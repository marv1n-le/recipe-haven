using Microsoft.EntityFrameworkCore;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.requestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Services.Service
{

    public interface IRecipeIngredientService
    {
        //Task<List<RecipeIngredient>> GetAll();
        //Task<RecipeIngredient> GetById(int id1, int id2);
        Task Create(RecipeIngredientRequest entity, int id);
        //Task CreateByRecipe(RecipeIngredient entity);
        Task Update(int id1, int id2, RecipeIngredientRequest entity);
        Task Delete(int id1, int id2);
        Task DeleteByRecipe(int id);
        Task DeleteByIngredient(int id);
        //Task<List<RecipeIngredient>> Search(string searchTerm);
    }
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly GenericRepository<RecipeIngredient> _genericRepository;

        public RecipeIngredientService() { _genericRepository ??= new GenericRepository<RecipeIngredient>(); }
        public async Task Create(RecipeIngredientRequest entity, int id)
        {
            try
            {
                var check = await _genericRepository.SingleOrDefaultAsync(
                            selector: x => x,
                            predicate: e => e.RecipeId == id && e.IngredientId == entity.IngredientId);
                if (check != null)
                {
                    var ingredient = check.Ingredient.Name;
                    throw new Exception(ingredient + " already exists in Recipe.");
                }

                var recipeIngredient = new RecipeIngredient();
                recipeIngredient.RecipeId = id;
                recipeIngredient.IngredientId = entity.IngredientId;
                recipeIngredient.Quantity = entity.Quantity;
                recipeIngredient.Unit = entity.Unit;


                // Create
                await _genericRepository.CreateAsync(recipeIngredient);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating : " + ex.Message, ex);
            }
        }

        public async Task Delete(int id1, int id2)
        {
            var check = await _genericRepository.SingleOrDefaultAsync(
                            selector: x => x,
                            predicate: e => e.RecipeId.Equals(id1) && e.IngredientId.Equals(id2));

            if (check == null)
            {
                throw new Exception("Recipe's Ingredient not found");
            }
            await _genericRepository.RemoveAsync(check);
        }

        public async Task DeleteByRecipe(int id)
        {
            var recipeIngredients = (List<RecipeIngredient>)await _genericRepository.GetListAsync(
                selector: x => x,
                               predicate: x => x.RecipeId == id
                );
            if (recipeIngredients != null)
            {
                foreach (var x in recipeIngredients)
                {
                    Delete(x.RecipeId, x.IngredientId);
                }
            }

        }    
        
        public async Task DeleteByIngredient(int id)
        {
            var recipeIngredients = (List<RecipeIngredient>)await _genericRepository.GetListAsync(
                selector: x => x,
                predicate: x => x.IngredientId == id
                );

            if (recipeIngredients != null)
            {
                foreach (var x in recipeIngredients)
                {
                    Delete(x.RecipeId, x.IngredientId);
                }
            }

        

        }


        //public async Task<List<RecipeIngredient>> GetAll()
        //{
        //    return (List<RecipeIngredient>)await _genericRepository.GetListAsync(
        //       selector: x => x,
        //       include: x => x.Include(x => x.Recipe)
        //                        .Include(x => x.Ingredient));
        //}

        //public async Task<RecipeIngredient> GetById(int id1, int id2)
        //{
        //    return await _genericRepository.SingleOrDefaultAsync(
        //                    selector: x => x,
        //                    predicate: e => e.RecipeId.Equals(id1) && e.IngredientId.Equals(id2),
        //                    include: x => x.Include(x => x.Recipe)
        //                                   .Include(x => x.Ingredient));
        //}

        public Task<List<RecipeIngredient>> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id1, int id2, RecipeIngredientRequest entity)
        {
            try
            {
                var tmpEntity = await _genericRepository.SingleOrDefaultAsync(
                            selector: x => x,
                            predicate: e => e.RecipeId.Equals(id1) && e.IngredientId.Equals(id2));
                if (tmpEntity == null)
                {
                    throw new Exception("Item not found");
                }

                tmpEntity.IngredientId = entity.IngredientId;
                tmpEntity.Quantity = entity.Quantity;
                tmpEntity.Unit = entity.Unit;   
             

                await _genericRepository.UpdateAsync(tmpEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while update : " + ex.Message, ex);
            }
        }
    }
}
