using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Services.Service;

namespace PE_PRN231_FA24_000979_SE173399_BE.Controllers
{
    public class OriginController : ControllerBase
    {
        private readonly IOriginService _originService;
     

        public OriginController(IOriginService originService)
        {
            _originService = originService;
  
        }

        //[Authorize(Policy = "Lv2")]

        [HttpGet("/api/Origins/page/{pageNumber}")]
        public async Task<ActionResult<Paginate<OriginDTO>>> GetOrigins(int pageNumber)
        {
            try
            {
                var e = await _originService.GetAll(pageNumber);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

 
        [HttpGet("/api/Origins/{id}")]
        public async Task<ActionResult<IEnumerable<OriginDTO>>> GetOrigin(int id)
        {
            try
            {
                var e = await _originService.GetById(id);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/Origins/{id}")]
        public async Task<IActionResult> PutOrigin(int id, [FromBody] OriginRequest e)
        {
            try
            {
                //if (!id.Equals(e.OriginId))
                //{
                //    return BadRequest();
                //}

                await _originService.Update(id, e);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("/api/Origins")]
        public async Task<ActionResult<Origin>> PostOrigin([FromBody] OriginRequest e)
        {
            try
            {
                await _originService.Create(e);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("/api/Origins/{id}")]
        public async Task<IActionResult> DeleteOrigin(int id)
        {
            try
            {
                await _originService.Delete(id);
           
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


      
    }
}
