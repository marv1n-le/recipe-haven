using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.RecipeHaven.Repo.responseDTOs;
using SWD.RecipeHaven.Services.Service;

namespace SWD.RecipeHaven.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("/api/Subscriptions/page/{pageNumber}")]
        public async Task<ActionResult<Paginate<SubscriptionDTO>>> GetSubscriptions(int pageNumber)
        {
            try
            {
                var result = await _subscriptionService.GetAll(pageNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/Subscriptions/{id}")]
        public async Task<ActionResult<SubscriptionDTO>> GetSubscription(int id)
        {
            try
            {
                var result = await _subscriptionService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/Subscriptions/{id}")]
        public async Task<IActionResult> UpdateSubscription(int id, [FromBody] UpdateSubscriptionDTO request)
        {
            try
            {
                await _subscriptionService.Update(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/api/Subscriptions")]
        public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionDTO request)
        {
            try
            {
                await _subscriptionService.Create(request);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/Subscriptions/{id}")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            try
            {
                await _subscriptionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
