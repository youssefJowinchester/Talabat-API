using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Services.Interfaces;
using Talabat.Core.Specifications.OrderSpecs;
using Talabat.Repository.Repositories;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Seevice
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork
            , IConfiguration configuration)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        #region CreateOrUpdatePaymentIntent
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            //1. Get basket
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) return null;

            //2. Get total price
            if (basket.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }

            //SubTotal
            var SubTotal = basket.Items.Sum(I => I.Price * I.Quantity);

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
                shippingPrice = DeliveryMethod.Cost;
            }

            // call stripe

            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // create new PaymentIntentId

                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(SubTotal * 100 + shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await service.CreateAsync(Options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;


            }
            else
            {
                //Update PaymentIntentId

                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(SubTotal * 100 + shippingPrice * 100),
                };

                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }


            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }

        #endregion


        #region UpdatePaymentIntentToSuccessedOrFailed
        public async Task<Order> UpdatePaymentIntentToSuccessedOrFailed(string PaymentIntentId, bool flag)
        {
            var spec = new OrderWithPaymentIntentSpec(PaymentIntentId);

            var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);

            if (flag)
            {
                order.Status = OrderStatus.PaymentReceived;
            }
            else
            {
                order.Status = OrderStatus.PaymentFailed;
            }

            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.CompleteAsync();

            return order;
        }
        #endregion

    }
}
