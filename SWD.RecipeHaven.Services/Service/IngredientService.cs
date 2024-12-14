using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
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

    public interface IIngredientService
    {
        Task<Paginate<IngredientDTO>> GetAll(int page);
        Task<IngredientDTO> GetById(int id);
        Task Create(IngredientRequest entity);
        Task Update(int id, IngredientRequest entity);
        Task Delete(int id);
        Task<List<IngredientDTO>> Search(string searchTerm);
    }
    public class IngredientService : IIngredientService
    {
        private readonly GenericRepository<Ingredient> _genericRepository;

        public IngredientService()
        {
            _genericRepository ??= new GenericRepository<Ingredient>();
        }
        public async Task Create(IngredientRequest entity)
        {
            try
            {
                //var check = await _genericRepository.GetByIdAsync(entity.IngredientId);
                //if (check != null)
                //{
                //    throw new Exception("Ingredient already exists.");
                //}

                var result = new Ingredient();
                result.Name = entity.Name;
                result.Type = entity.Type;
                result.ActiveStatus = true;
                result.NutritionInfoId = null;


                // Create
                await _genericRepository.CreateAsync(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating : " + ex.Message, ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var check = await _genericRepository.GetByIdAsync(id);
                if (check == null)
                {
                    throw new Exception("Ingredient not found");
                }

                check.ActiveStatus = false;

                await _genericRepository.UpdateAsync(check);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while delete : " + ex.Message, ex);
            }
        }

        public async Task<Paginate<IngredientDTO>> GetAll(int page)
        {
            var result = await _genericRepository.GetPagingListAsync(
               selector: x => x,
               predicate: x => x.ActiveStatus == true,
               page: page,
               size: 10
               );
            if (result == null)
            {
                throw new Exception("No Result");
            }

            var paginate = new Paginate<IngredientDTO>();
            paginate.Items = MapObjectToDTO(result.Items);
            paginate.Size = result.Size;
            paginate.Page = result.Page;
            paginate.TotalPages = result.TotalPages;
            paginate.Total = result.Total;

            return paginate;
        }

        public async Task<IngredientDTO> GetById(int id)
        {
            var result = await _genericRepository.SingleOrDefaultAsync(
                            selector: x => x,
                            predicate: x => x.IngredientId.Equals(id) && x.ActiveStatus == true
                            //include: x => x.Include(x => x.NutritionInfo)
                            );
            return MapObjectToDTO(result);
        }

        public Task<List<IngredientDTO>> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, IngredientRequest entity)
        {
            try
            {
                var tmpEntity = await _genericRepository.GetByIdAsync(id);
                if (tmpEntity == null)
                {
                    throw new Exception("Ingredient not found");
                }

                tmpEntity.Name = entity.Name;
                tmpEntity.Type = entity.Type;
                //tmpEntity.ActiveStatus = entity.ActiveStatus;
   
                await _genericRepository.UpdateAsync(tmpEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while update : " + ex.Message, ex);
            }
        }

        private List<IngredientDTO> MapObjectToDTO(IEnumerable<Ingredient> entityList)
        {
            if (entityList == null || !entityList.Any())
            {
                return new List<IngredientDTO>();
            }

            var result = new List<IngredientDTO>();

            foreach (var entity in entityList)
            {
                var dto = new IngredientDTO
                {
                    Id = entity.IngredientId,
                    Name = entity.Name,
                    Type = entity.Type,
                };

                result.Add(dto);
            }
            return result;
        }

        // Overload for single entity
        private IngredientDTO MapObjectToDTO(Ingredient entity)
        {
            if (entity == null)
            {
                return null;
            }

            return MapObjectToDTO(new List<Ingredient> { entity }).FirstOrDefault();
        }
    }
}
