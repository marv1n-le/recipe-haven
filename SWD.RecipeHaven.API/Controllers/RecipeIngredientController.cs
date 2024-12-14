using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Services.Service;

namespace PE_PRN231_FA24_000979_SE173399_BE.Controllers
{
    public class RecipeIngredientController : ControllerBase
    {
        private readonly IRecipeIngredientService _recipeIngredientService;
     

        public RecipeIngredientController(IRecipeIngredientService recipeIngredientService)
        {
            _recipeIngredientService = recipeIngredientService;
  
        }
        //[HttpGet("/api/Origins")]
        //public async Task<ActionResult<IEnumerable<OriginDTO>>> GetOrigins()
        //{
        //    try
        //    {
        //        var e = await _stepService.GetAll();
        //        return Ok(e);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

 
        //[HttpGet("/api/Steps/{id}")]
        //public async Task<ActionResult<IEnumerable<StepDTO>>> GetRecipeIngredient(int id)
        //{
        //    try
        //    {
        //        var e = await _stepService.GetById(id);
        //        return Ok(e);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut("/api/RecipeIngredients/{id}")]
        public async Task<IActionResult> PutStep(int recipeId, int ingredienId, [FromBody] RecipeIngredientRequest e)
        {
            try
            {
                //if (!id1.Equals() || !id2.Equals(e.IngredientId))
                //{
                //    return BadRequest();
                //}

                await _recipeIngredientService.Update(recipeId, ingredienId, e);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("/api/RecipeIngredients")]
        public async Task<ActionResult<RecipeIngredient>> PostRecipeIngredient([FromBody] RecipeIngredientRequest e, int recipeId)
        {
            try
            {
                await _recipeIngredientService.Create(e, recipeId);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("/api/RecipeIngredients/{id}")]
        public async Task<IActionResult> DeleteRecipeIngredient(int id1, int id2)
        {
            try
            {
                await _recipeIngredientService.Delete(id1, id2);
           
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }    
    }
}
