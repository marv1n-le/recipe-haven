using Microsoft.AspNetCore.Mvc;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Services.Service;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Repo.responseDTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SWD.RecipeHaven.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCRUDService _userCrudService;

        public UserController(IUserCRUDService userCrudService)
        {
            _userCrudService = userCrudService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDTO userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = await _userCrudService.CreateUser(userRequest);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
        }  
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDTO userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userRequest.Role = "User";
            var newUser = await _userCrudService.CreateUser(userRequest);

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequestDTO userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedUser = await _userCrudService.UpdateUser(id, userRequest);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var isDeleted = await _userCrudService.DeleteUser(id);
                return isDeleted ? NoContent() : BadRequest(new { message = "Failed to delete user." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userCrudService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Users/page/{pageNumber}")]
        public async Task<ActionResult<Paginate<CategoryDTO>>> GetUsers(int pageNumber)
        {
            try
            {
                var e = await _userCrudService.GetAllUser(pageNumber);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}