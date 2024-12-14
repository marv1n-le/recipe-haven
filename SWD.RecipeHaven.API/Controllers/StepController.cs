using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Services.Service;


    public class StepController : ControllerBase
    {
        private readonly IStepService _stepService;
    private readonly ICloudinaryService _cloudinaryService;


    public StepController(IStepService stepService, ICloudinaryService cloudinaryService)
        {
            _stepService = stepService;
        _cloudinaryService = cloudinaryService;
             
        }
 

        [HttpPut("/api/Steps/{id}")]
        public async Task<IActionResult> PutStep(int id, [FromBody] StepRequest e, IFormFile file)
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

                e.Image = imageUrl;
            }  

            //------------------------------------------------------------------------------------------------

            await _stepService.Update(id, e);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    [HttpPost("/api/Steps")]
    public async Task<ActionResult<Step>> PostStep([FromBody] StepRequest entity, int ricipeId, IFormFile file)
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

            entity.Image = imageUrl;
        }

        //------------------------------------------------------------------------------------------------

        try
        {
            await _stepService.Create(entity, ricipeId);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("/api/Steps/{id}")]
        public async Task<IActionResult> DeleteStep(int id)
        {
            try
            {
                await _stepService.Delete(id);
           
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }    
    }

