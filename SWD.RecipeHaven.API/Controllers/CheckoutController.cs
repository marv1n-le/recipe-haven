using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using SWD.RecipeHaven.Services.Service;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Data.Models;

namespace SWD.RecipeHaven.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly IPaymentService _paymentService;
        private readonly ISubscriptionService _subcriptionPlanService;

        public CheckoutController(PayOS payOS, IPaymentService paymentService, ISubscriptionService subcriptionPlanService)
        {
            _payOS = payOS;
            _paymentService = paymentService;
            _subcriptionPlanService = subcriptionPlanService;
        }

        [HttpPost("/create-payment-link")]
        public async Task<IActionResult> Checkout([FromBody] PaymentRequestDTO model)
        {
            try
            {

                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                Subscription plan = await _subcriptionPlanService.GetById(model.productId);
                ItemData item = new ItemData(plan.Name, 1, (int)plan.Price);
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData
                    (
                    orderCode,
                    (int)plan.Price,
                    "Thanh toan don hang",
                    items,
                    model.cancelUrl,
                    model.returnUrl
                    );
                CreatePaymentResult createPayment = await _paymentService.createPaymentLink(paymentData);

                return Ok(createPayment.checkoutUrl);
            }
            catch (Exception exception)
            {
                return Ok(model.returnUrl);
            }
        }
    }
}
