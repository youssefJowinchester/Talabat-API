using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Services.Interfaces;
using Talabat.Core.Specifications.OrderSpecs;
using Talabat.Repository.Repositories;

namespace Talabat.Seevice
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository
            , IUnitOfWork unitOfWork
            , IPaymentService paymentService
            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }


        public async Task<Order?> createOrderAsync(string BuyerEmail, string basketId, int DeliveryMethodId, Address ShippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            var OrderItem = new List<OrderItem>();
            if (basket?.Items.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var productItemOrder = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrder, item.Price, item.Quantity);

                    OrderItem.Add(orderItem);
                }
            }

            var SubTotal = OrderItem.Sum(OI => OI.Price * OI.Quantity);

            var delivery = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(DeliveryMethodId);

            //check if payment intent id exsits from another order
            var spec = new OrderWithPaymentIntentSpec(basket.PaymentIntentId);
            var exsitsOrder = await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);
            if (exsitsOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(exsitsOrder);
                var NewBasket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }

            var Order = new Order(BuyerEmail, ShippingAddress, delivery, OrderItem, SubTotal, basket.PaymentIntentId);

            await _unitOfWork.Repository<Order>().AddAsync(Order);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return Order;
        }



        public async Task<IReadOnlyList<Order>?> GetOrdersForSpecUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpec(BuyerEmail);

            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return orders;
        }




        public async Task<Order?> GetOrderByIdForSpecUSerAsync(string BuyerEmail, int OrderId)
        {
            var spec = new OrderSpec(BuyerEmail, OrderId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);

            if (order is null) return null;

            return order;
        }
    }
}
