using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Services.Service;

namespace SWD.RecipeHaven.API.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
     

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
  
        }
        [HttpGet("/api/Categories/page/{pageNumber}")]
        public async Task<ActionResult<Paginate<CategoryDTO>>> GetCategories(int pageNumber)
        {
            try
            {
                var e = await _categoryService.GetAll(pageNumber);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

 
        [HttpGet("/api/Categories/{id}")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory(int id)
        {
            try
            {
                var e = await _categoryService.GetById(id);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("/api/Categories/{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryRequest e)
        {
            try
            {
                //if (!id.Equals(e.CategoryId))
                //{
                //    return BadRequest();
                //}

                await _categoryService.Update(id, e);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("/api/Categories")]
        public async Task<ActionResult<Recipe>> PostCategory([FromBody] CategoryRequest e)
        {
            try
            {
                await _categoryService.Create(e);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("/api/Categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.Delete(id);
           
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


 
    }
}
