using Microsoft.EntityFrameworkCore;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Services.Service
{

    public interface IStepService
    {
        //Task<List<StepDTO>> GetAll();
        Task<StepDTO> GetById(int id);
        Task<List<StepDTO>> GetStepByRecipe(int id);
        Task Create(StepRequest entity, int id);
        Task Update(int id, StepRequest entity);
        Task Delete(int id);
        Task DeleteByRecipe(int id);
        Task<List<StepDTO>> Search(string searchTerm);
    }
    public class StepService : IStepService
    {
        private readonly GenericRepository<Step> _genericRepository;
        public StepService() {
            _genericRepository ??= new GenericRepository<Step>();
        }
        public async Task Create(StepRequest entity, int id)
        {
            try
            {
                var checkStepNumber = await _genericRepository.SingleOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.RecipeId == id && x.StepNumber == entity.StepNumber
                    );
                if (checkStepNumber != null)
                {
                    int stepNum = checkStepNumber.StepNumber;
                    throw new Exception("Step " + stepNum + " already exists for this Recipe");
                }

                var step = new Step();
                step.RecipeId = id;
                step.StepNumber = entity.StepNumber;
                step.Description = entity.Description;
                step.Image = entity.Image;
                step.Duration = entity.Duration;
                step.ToolsRequired = entity.ToolsRequired;

                // Create
                await _genericRepository.CreateAsync(step);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating : " + ex.Message, ex);
            }
        }

        public async Task Delete(int id)
        {
            var check = await _genericRepository.GetByIdAsync(id);

            if (check == null)
            {
                throw new Exception("Step not found");
            }
            await _genericRepository.RemoveAsync(check);
        }

        public async Task DeleteByRecipe(int id)
        {
            var steps = (List<Step>)await _genericRepository.GetListAsync(
                selector: x => x,
                predicate: x => x.RecipeId == id
                );

            if (steps.Any() && steps != null)
            {
                //throw new Exception("Step not Exist");
                foreach (var step in steps)
                {
                   Delete(step.StepId);
                }

            }
            

        }

        public async Task<Paginate<StepDTO>> GetAll(int page)
        {
            var result = await _genericRepository.GetPagingListAsync(
               selector: x => x,
               page: page,
               size: 10,
               include: x => x.Include(x => x.Recipe));


            if (result == null)
            {
                throw new Exception("No Result");
            }

            var paginate = new Paginate<StepDTO>();
            paginate.Items = MapObjectToDTO(result.Items);
            paginate.Size = result.Size;
            paginate.Page = result.Page;
            paginate.TotalPages = result.TotalPages;
            paginate.Total = result.Total;

            return paginate;
        }

        public async Task<StepDTO> GetById(int id)
        {
            var result = await _genericRepository.SingleOrDefaultAsync(
                            selector: x => x,
                            include: x => x.Include(x => x.Recipe));
            return MapObjectToDTO(result);

        }

        public Task<List<StepDTO>> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, StepRequest entity)
        {
            try
            {
                var tmpEntity = await _genericRepository.GetByIdAsync(id);
                if (tmpEntity == null)
                {
                    throw new Exception("Item not found");
                }

                tmpEntity.StepNumber = entity.StepNumber;
                tmpEntity.Description = entity.Description;
                tmpEntity.Duration = entity.Duration;
                tmpEntity.ToolsRequired = entity.ToolsRequired;

                await _genericRepository.UpdateAsync(tmpEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while update : " + ex.Message, ex);
            }
        }
        public async Task<List<StepDTO>> GetStepByRecipe(int id)
        {

            var steps = (List<Step>)await _genericRepository.GetListAsync(
                 selector: x => x,
                 predicate: x => x.RecipeId.Equals(id)
                 );

            return steps.Select(step => new StepDTO
            {
                id = id,
                StepNumber = step.StepNumber,
                Description = step.Description,
                //Image = step.Image,
                Duration = step.Duration,
                ToolsRequired = step.ToolsRequired
            }).ToList();
        }

        //-------------------
        private List<StepDTO> MapObjectToDTO(IEnumerable<Step> entityList)
        {
            if (entityList == null || !entityList.Any())
            {
                return new List<StepDTO>();
            }

            var result = new List<StepDTO>();

            foreach (var entity in entityList)
            {
                var dto = new StepDTO
                {
                    //id = entity.StepId,
                    StepNumber = entity.StepNumber,
                    Description = entity.Description,
                    Duration = entity.Duration,
                    ToolsRequired = entity.ToolsRequired
                };

                result.Add(dto);
            }
            return result;
        }


        // Overload for single entity
        private StepDTO MapObjectToDTO(Step entity)
        {
            if (entity == null)
            {
                return null;
            }

            return MapObjectToDTO(new List<Step> { entity }).FirstOrDefault();
        }
    }
}
