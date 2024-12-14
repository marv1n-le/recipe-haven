using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.RecipeHaven.Repo.responseDTOs;
using SWD.RecipeHaven.Services.Service;

namespace SWD.RecipeHaven.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly IUserSubscriptionService _userSubscriptionService;

        public UserSubscriptionController(IUserSubscriptionService userSubscriptionService)
        {
            _userSubscriptionService = userSubscriptionService;
        }

        [HttpGet("/api/UserSubscriptions/page/{pageNumber}")]
        public async Task<ActionResult<Paginate<UserSubscriptionDTO>>> GetUserSubscriptions(int pageNumber)
        {
            try
            {
                var result = await _userSubscriptionService.GetAll(pageNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/UserSubscriptions/{id}")]
        public async Task<ActionResult<UserSubscriptionDTO>> GetUserSubscription(int id)
        {
            try
            {
                var result = await _userSubscriptionService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/UserSubscriptions/{id}")]
        public async Task<IActionResult> UpdateUserSubscription(int id, [FromBody] UpdateUserSubscriptionDTO request)
        {
            try
            {
                await _userSubscriptionService.Update(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/api/UserSubscriptions")]
        public async Task<IActionResult> CreateUserSubscription([FromBody] CreateUserSubscriptionDTO request)
        {
            try
            {
                await _userSubscriptionService.Create(request);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/UserSubscriptions/{id}")]
        public async Task<IActionResult> DeleteUserSubscription(int id)
        {
            try
            {
                await _userSubscriptionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}