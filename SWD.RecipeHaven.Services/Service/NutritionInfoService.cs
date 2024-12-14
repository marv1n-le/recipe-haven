using Microsoft.EntityFrameworkCore;
using SWD.RecipeHaven.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Services.Service
{

    public interface INutritionInfoService
    {
        Task<List<NutritionInfo>> GetAll();
        Task<NutritionInfo> GetById(string id);
        Task Create(NutritionInfo entity);
        Task Update(string id, NutritionInfo entity);
        Task Delete(string id);
        Task<List<NutritionInfo>> Search(string searchTerm);
    }
    public class NutritionInfoService : INutritionInfoService
    {
        private readonly GenericRepository<NutritionInfo> _genericRepository;

        public NutritionInfoService()
        {
            _genericRepository ??= new GenericRepository<NutritionInfo>();
        }
        public async Task Create(NutritionInfo entity)
        {
            try
            {
                var check = await _genericRepository.GetByIdAsync(entity.NutritionInfoId);
                if (check != null)
                {
                    throw new Exception("Nutrition Info already exists.");
                }

                // Create
                await _genericRepository.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating : " + ex.Message, ex);
            }
        }

        public async Task Delete(string id)
        {
            var check = await _genericRepository.GetByIdAsync(id);

            if (check == null)
            {
                throw new Exception("Item not found");
            }
            await _genericRepository.RemoveAsync(check);
        }

        public async Task<List<NutritionInfo>> GetAll()
        {
            return (List<NutritionInfo>)await _genericRepository.GetListAsync(
               selector: x => x);
        }

        public async Task<NutritionInfo> GetById(string id)
        {
            return await _genericRepository.SingleOrDefaultAsync(
                            selector: x => x,
                            predicate: x => x.NutritionInfoId.Equals(id));
        }

        public Task<List<NutritionInfo>> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task Update(string id, NutritionInfo entity)
        {
            try
            {
                var tmpEntity = await _genericRepository.GetByIdAsync(entity.NutritionInfoId);
                if (tmpEntity == null)
                {
                    throw new Exception("Item not found");
                }

                //tmpEntity.ExpirationDate = cosmetic.ExpirationDate;
                //tmpEntity.CosmeticName = cosmetic.CosmeticName;
                //tmpEntity.SkinType = cosmetic.SkinType;
                //tmpEntity.DollarPrice = cosmetic.DollarPrice;
                //tmpEntity.CosmeticSize = cosmetic.CosmeticSize;
                //tmpEntity.CategoryId = cosmetic.CategoryId;

                await _genericRepository.UpdateAsync(tmpEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while update : " + ex.Message, ex);
            }
        }
    }
}
