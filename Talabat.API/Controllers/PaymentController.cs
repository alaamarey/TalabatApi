using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly string? _webhookSecret;

        public PaymentController(IPaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _webhookSecret = configuration["Stripe:WebhookSecret"];
        }




        [Authorize]

        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket?>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null) return BadRequest(new ApiResponse(400, "No Basket"));
            return Ok(basket);
        }



        [HttpPost("webhook")]
        public async Task<ActionResult<Order>> StripeWebhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _webhookSecret);

                var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;

                var order = new Order();


                //  Success الي Order  بتاع ال  Status  كدا يعني اشطا العمليه نجحت واغير ال  // 
                if (stripeEvent.Type == "payment_intent.succeeded")
                {


                     order = await _paymentService.UpdatePaymentIntentToSucceededOrFailedAsync(paymentIntent.Id, true);

                }

                //  Failed الي Order  بتاع ال  Status  كدا يعني  العمليه فشلت واغير ال  // 

                else if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                     order = await _paymentService.UpdatePaymentIntentToSucceededOrFailedAsync(paymentIntent.Id, false);

                }



                return Ok(order);
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe Webhook Error: {ex.Message}");
                return BadRequest();
            }
        }







    }
}
