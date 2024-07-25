using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.Core.Entities;
using Talabat.Core.Services.Interfaces;
using Talabat_.APIS.Errors;

namespace Talabat_.APIS.Controllers
{
    [Authorize]
    public class PaymentController : BaseAPIController
    {
        private readonly IPaymentService _paymentService;
        const string endpointSecret = "whsec_a5e875a887d550e0237e6013ec1f7a1ba78a6af4d887c6b4373c1c00b85b788c";

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        #region CreateOrUpdatePaymentIntent
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket is null) return BadRequest(new ApiResponse(400, "there is a problem with your basket"));

            return Ok(basket);
        }
        #endregion


        #region StripeWebhook

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    await _paymentService.UpdatePaymentIntentToSuccessedOrFailed(paymentIntent.Id, false);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    await _paymentService.UpdatePaymentIntentToSuccessedOrFailed(paymentIntent.Id, true);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

        #endregion

    }
}
