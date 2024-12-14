using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Services.Service;

namespace SWD.RecipeHaven.API.Controllers
{
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IStepService _stepService;
        private readonly IRecipeIngredientService _recipeIngredientService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IUserCRUDService _userCRUDService;
        private readonly IUserSubscriptionService _userSubscriptionService;


        public RecipeController(IRecipeService recipeService, IStepService stepService, IRecipeIngredientService recipeIngredientService, ICloudinaryService cloudinaryService, IUserCRUDService userCRUDService, IUserSubscriptionService userSubscriptionService)
        {
            _recipeService = recipeService;
            _stepService = stepService;
            _recipeIngredientService = recipeIngredientService;
            _cloudinaryService = cloudinaryService;
            _userCRUDService = userCRUDService;
            _userSubscriptionService = userSubscriptionService; 
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Recipes/page/{pageNumber}")]
        public async Task<ActionResult<Paginate<RecipeDTO>>> GetRecipes(int pageNumber)
        {
            try
            {
                var recipes = await _recipeService.GetAll(pageNumber);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Recipes/{id}")]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipe(int id)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");


                int amout = _userSubscriptionService.GetAmoutView(userId).Result;
                int viewed = _userCRUDService.GetUserViewed(userId).Result;

                if(amout == 0 && userId != null) return BadRequest("Please upgrade your subscription plan to view this recipe");

                if (viewed <= amout && userId != null) return BadRequest("You have used up all your views. Please upgrade your subscription plan to view this recipe");


                var recipe = await _recipeService.GetById(id, userId);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("/api/Recipes/{id}")]
        public async Task<IActionResult> PutRecipe(int id, [FromBody] RecipeUpdateRequest recipe, IFormFile? file)
        {
            try
            {
                //----------------------------- Cloundinary------------------------------------------------------
                if (file != null)
                {
                    var tempFilePath = Path.GetTempFileName();
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var imageUrl = _cloudinaryService.UploadImage(tempFilePath);

                    System.IO.File.Delete(tempFilePath);

                    recipe.Image = imageUrl;
                }

                //------------------------------------------------------------------------------------------------

                if (!id.Equals(id))
                {
                    return BadRequest();
                }

                await _recipeService.Update(id, recipe);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/api/ImageTest")]
        public async Task<ActionResult<String>> PostImage( IFormFile file) //    
        {
            if (file == null)
            {
                return BadRequest("Invalid File");
            }

            var tempFilePath = Path.GetTempFileName();
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var imageUrl = _cloudinaryService.UploadImage(tempFilePath);

            System.IO.File.Delete(tempFilePath);

            return imageUrl;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/api/Recipes")]
        public async Task<ActionResult<Recipe>> PostRecipe([FromBody] RecipeRequest recipe, IFormFile file) //    , [FromForm] IFormFile file, [FromForm] string name
        {
            // [FromBody] RecipeRequest recipe, 
            try
            {

                //----------------------------- Cloundinary------------------------------------------------------
                if (file != null)
                {
                    var tempFilePath = Path.GetTempFileName();
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var imageUrl = _cloudinaryService.UploadImage(tempFilePath);

                    System.IO.File.Delete(tempFilePath);

                    recipe.Image = imageUrl;
                }

                //------------------------------------------------------------------------------------------------

                var author = HttpContext.Session.GetInt32("UserId");
                recipe.Author = author;

                await _recipeService.Create(recipe);
                //List<AddRecipeIngredientDTO> recipeIngredients = recipe.Ingrediens;

                int recipeId = _recipeService.GetCurrentId();

                foreach (var item in recipe.Ingrediens)
                {
                    await _recipeIngredientService.Create(item, recipeId);
                    
                }               
                foreach (var item in recipe.Steps)
                {
                    await _stepService.Create(item, recipeId);
                    
                }

              
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("/api/Recipes/{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            try
            {
                await _recipeService.Delete(id);
                await _stepService.DeleteByRecipe(id);
                await _recipeIngredientService.DeleteByRecipe(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Policy = "Lv2")]

        [Authorize(Roles = "Admin")]
        [HttpGet("/api/Search/page/{pageNumber}")]
        public async Task<ActionResult<Paginate<RecipeDTO>>> SearchRecipe(
                             int pageNumber,
                             [FromQuery] string title = null,
                             [FromQuery] string description = null,
                             [FromQuery] string method = null,
                             [FromQuery] string user = null,
                             [FromQuery] string origin = null,
                             [FromQuery] string difficul = null,
                             [FromQuery] string category = null,
                             [FromQuery] List<int> ingredientsId = null)
        {
            try
            {
                var recipes = await _recipeService.Search(pageNumber, title, description, method, user, origin, difficul, category, ingredientsId);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}