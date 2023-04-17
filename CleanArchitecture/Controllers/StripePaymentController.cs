using Azure.Core;
using CleanArchitecture.Contract;
using CleanArchitecture.PaymentData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Stripe;
using Stripe.Checkout;
using Stripe.Issuing;

namespace CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripePaymentController : ControllerBase
    {
        private readonly IStripeAppService _stripeService;

        public StripePaymentController(IStripeAppService stripeAppService)
        {
            _stripeService = stripeAppService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment()
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = 1000,
                Currency = "inr",
                PaymentMethodTypes = new List<string>
        {
            "card",
        },
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return Ok(new { clientSecret = paymentIntent });
        }

        [HttpPost("customer/add")]
        public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromBody] AddStripeCustomer customer, CancellationToken ct)
        {
            try
            {
                //CancellationToken ct = CancellationToken.None;
                StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(
                    customer, ct);

                return StatusCode(StatusCodes.Status200OK, createdCustomer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpPost("create/html")]
        //public ActionResult Create(AddStripePayment payment)
        //{
        //    StripeConfiguration.ApiKey = "sk_test_51Mfyf6SFLF5bIQTbwIs4Wki5FNwjcdlqFnxU5Sm75ake9KpzU6LnY3YAFg6IzJKsF9hSfRKfpUCxjvRoW3hu0mk800nGq5cyJK";

        //    var options = new PaymentIntentCreateOptions
        //    {
        //        Customer = payment.CustomerId,
        //        PaymentMethod = "pm_card_visa",
        //        Amount = payment.Amount,
        //        Currency = "inr",
        //        PaymentMethodTypes = new List<string> { "card" },
        //        ReceiptEmail = payment.ReceiptEmail
        //    };
        //    var service = new PaymentIntentService();

        //    service.Create(options);

        //    service.Confirm(
        //      "pi_3Mg5jMSFLF5bIQTb1ovgFlYI" ,options
        //      );

        //    return Ok(options);
        //}
    }
}