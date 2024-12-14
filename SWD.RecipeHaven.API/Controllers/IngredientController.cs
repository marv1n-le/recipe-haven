using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Services.Service;

namespace PE_PRN231_FA24_000979_SE173399_BE.Controllers
{
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _igredientService;
        private readonly IRecipeIngredientService _recipeIgredientService;
     

        public IngredientController(IIngredientService ingredientService, IRecipeIngredientService recipeIgredientService)
        {
            _igredientService = ingredientService;
            _recipeIgredientService = recipeIgredientService;
        }
        [HttpGet("/api/Ingredients/page/{pageNumber}")]
        public async Task<ActionResult<Paginate<IngredientDTO>>> GetIngredients(int pageNumber)
        {
            try
            {
                var e = await _igredientService.GetAll(pageNumber);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

 
        [HttpGet("/api/Ingredients/{id}")]
        public async Task<ActionResult<IEnumerable<IngredientDTO>>> GetIngredient(int id)
        {
            try
            {
                var e = await _igredientService.GetById(id);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/Ingredients/{id}")]
        public async Task<IActionResult> PutIngredient(int id, [FromBody] IngredientRequest e)
        {
            try
            {
                //if (!id.Equals(e.IngredientId))
                //{
                //    return BadRequest();
                //}

                await _igredientService.Update(id, e);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("/api/Ingredients")]
        public async Task<ActionResult<Ingredient>> PostIngredient([FromBody] IngredientRequest e)
        {
            try
            {
                await _igredientService.Create(e);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("/api/Ingredients/{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            try
            {
                await _igredientService.Delete(id);
                await _recipeIgredientService.DeleteByIngredient(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
