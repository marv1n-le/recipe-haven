using Microsoft.EntityFrameworkCore;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Services.Service
{

    public interface IRecipeService
    {
        Task<Paginate<RecipeDTO>> GetAll(int page);
        Task<RecipeDTO> GetById(int id, int? userId);
        Task Create(RecipeRequest entity);
        Task Update(int id, RecipeUpdateRequest entity);
        Task Delete(int id);
        //Task<List<RecipeDTO>> Search(string searchTerm);
        Task<Paginate<RecipeDTO>> Search(int page, string? title, string? description, string? method, string? user, string? origin, string? difficul, string? category, List<int> ingredientReq);
        //Task<List<RecipeIngredient>> GetIngrement(int id);
        int GetCurrentId();
    }
    public class RecipeService : IRecipeService
    {
        private readonly GenericRepository<Recipe> _genericRepository;
        private readonly GenericRepository<RecipeIngredient> _recipeIngredientRepo;
        private readonly GenericRepository<Step> _stepIngrementRepo;
        private readonly GenericRepository<User> _userRepo;

        private int tmpId = 0;
        public RecipeService()
        {
            _genericRepository ??= new GenericRepository<Recipe>();
            _recipeIngredientRepo ??= new GenericRepository<RecipeIngredient>();
            _stepIngrementRepo ??= new GenericRepository<Step>();
            _userRepo ??= new GenericRepository<User>();
        }  
     

        public async Task Create(RecipeRequest entity)
        {


            try
            {
                //var check = await _genericRepository.GetByIdAsync();
                //if (check != null)
                //{
                //    throw new Exception("Recipe already exists.");
                //}



                var recipe = new Recipe();
                //recipe.RecipeId = IdGenerator();
                recipe.Title = entity.Title;
                recipe.Description = entity.Description;
                recipe.Servings = entity.Servings;
                recipe.OriginId = entity.Origin;
                recipe.PreparationTime = entity.PreparationTime;
                recipe.CookingMethod = entity.CookingMethod;
                recipe.CookingTime = entity.CookingTime;
                recipe.DifficultyLevel = entity.Image;
                recipe.CategoryId = entity.Category;
                recipe.UserId = entity.Author;
                recipe.CreatedDate = recipe.LastUpdated = DateTime.Now;
                recipe.ActiveStatus = true;
                // Create

                IdGenerator();
                await _genericRepository.CreateAsync(recipe);

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
                    throw new Exception("Recipe not found");
                }

                check.ActiveStatus = false;

                await _genericRepository.UpdateAsync(check);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while delete : " + ex.Message, ex);
            }
        }

        public async Task<Paginate<RecipeDTO>> GetAll(int page)
        {
            var recipeList = await _genericRepository.GetPagingListAsync(
               selector: x => x,
               predicate: x => x.ActiveStatus == true,
               page: page,
               size: 10,
               include: x => x.Include(x => x.Category)
                            .Include(x => x.User)
                            .Include(x => x.Origin)
                                          );
            if (recipeList == null)
            {
                throw new Exception("No Result");

            }
            return MapPaging(recipeList, MapRecipeToDTO(recipeList.Items));

        }

        public async Task<RecipeDTO> GetById(int id, int? userId)
        {
            var recipe =  await _genericRepository.SingleOrDefaultAsync(
                            selector: x => x,
                            predicate: x => x.RecipeId.Equals(id) && x.ActiveStatus == true,
                            include: x => x.Include(x => x.Category)
                                           .Include(x => x.User)
                                           .Include(x => x.Origin)
                                );

            if(userId != null)
            {
                UserViewedRecipe(userId);
            }

            return MapRecipeToDTO(recipe);
        }

        //public async Task<List<RecipeDTO>> Search(string searchTerm)
        //{
        //    var recipeList =  (List<Recipe>)await _genericRepository.GetListAsync(
        //        selector: x => x,
        //        predicate: x =>
        //                        ((x.Title != null && x.Title.Contains(searchTerm)) ||
        //                        (x.Description != null && x.Description.Contains(searchTerm)) ||
        //                        (x.CookingMethod != null && x.CookingMethod.Contains(searchTerm)) ||
        //                        (x.User.Username != null && x.User.Username.Contains(searchTerm)) ||
        //                        (x.Origin.Country != null && x.Origin.Country.Contains(searchTerm)) ||
        //                        (x.DifficultyLevel != null && x.DifficultyLevel.Contains(searchTerm)) ||
        //                        (x.Category.Name != null && x.Category.Name.Contains(searchTerm)) ||
        //                        (x.Title != null && x.Title.Contains(searchTerm)) ||
        //                            x.ActiveStatus == true),
        //        include: x => x.Include(x => x.Category)
        //                        .Include(x => x.User)
        //                        .Include(x => x.Origin)
        //                       //.Include(x => x.RecipeIngredients).ThenInclude(c => c.Ingredient)

        //        );
        //    return MapRecipeToDTO(recipeList);
        //}

        public async Task<Paginate<RecipeDTO>> Search(int page, string title, string description, string method, string user,string origin,string difficul,string category, List<int> ingredientReq)
        {
            var recipeList = await _genericRepository.GetPagingListAsync(
                selector: x => x,
                predicate: x =>
                    (                        
                            (x.Title != null && x.Title.Contains(title) || (title == null)) &&
                            (x.Description != null && x.Description.Contains(description) || (description == null)) &&
                            (x.CookingMethod != null && x.CookingMethod.Contains(method) || (method == null)) &&
                            (x.User.Username != null && x.User.Username.Contains(user) || (user == null)) &&
                            (x.Origin.Country != null && x.Origin.Country.Contains(origin) || (origin == null)) &&
                            (x.DifficultyLevel != null && x.DifficultyLevel.Contains(difficul) || (difficul== null)) &&
                            (x.Category.Name != null && x.Category.Name.Contains(category) || (category == null)) &&
                            x.ActiveStatus == true
                        
                    ),
                page : page,
                size : 10,
                include: x => x.Include(x => x.Category)
                               .Include(x => x.User)
                               .Include(x => x.Origin)

            );

            var result = SearchByIngredient(recipeList.Items, ingredientReq);

            if (result == null)
            {
                throw new Exception("No Result");

            }
            return MapPaging(recipeList, MapRecipeToDTO(result));
        }


        public async Task Update(int id, RecipeUpdateRequest entity)
        {
            try
            {
                var tmpEntity = await _genericRepository.GetByIdAsync(id);
                if (tmpEntity == null)
                {
                    throw new Exception("Item not found");
                }

                //Map data
                tmpEntity.Title = entity.Title;
                tmpEntity.Description = entity.Description;
                tmpEntity.PreparationTime = entity.PreparationTime;
                tmpEntity.CookingTime = entity.CookingTime;
                tmpEntity.Servings = entity.Servings;
                tmpEntity.DifficultyLevel = entity.Image;
                tmpEntity.CookingMethod = entity.CookingMethod;
                tmpEntity.CategoryId = entity.Category;
                //tmpEntity.UserId = entity.Author;
                tmpEntity.OriginId = entity.Origin;
                tmpEntity.LastUpdated = DateTime.Now;

                await _genericRepository.UpdateAsync(tmpEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while update : " + ex.Message, ex);
            }
        }

        private List<RecipeDTO> MapRecipeToDTO(IEnumerable<Recipe> recipes)
        {
            if (recipes == null || !recipes.Any())
            {
                return new List<RecipeDTO>();
            }

            List<RecipeDTO> result = new List<RecipeDTO>();

            foreach (var recipe in recipes)
            {
                var recipeDTO = new RecipeDTO
                {
                    Id = recipe.RecipeId,
                    Title = recipe.Title,
                    Description = recipe.Description,
                    PreparationTime = recipe.PreparationTime,
                    CookingTime = recipe.CookingTime,
                    Servings = recipe.Servings,
                    Image = recipe.DifficultyLevel,
                    CookingMethod = recipe.CookingMethod,
                    Category = recipe.Category?.Name,
                    Author = recipe.User?.Username,
                    Origin = recipe.Origin?.Region,
                    CreatedDate = recipe.CreatedDate,
                    LastUpdated = DateTime.Now,
                    Ingredients = GetRecipeIngredient(recipe.RecipeId).Result,
                    //Nutrition = null, //GetRecipeNutrition(recipe.RecipeId).Result,
                    Steps = GetStepInRecipe(recipe.RecipeId).Result,
                };

                recipeDTO.DifficultLevel = CalDifficultLevel(recipeDTO.Steps);

                result.Add(recipeDTO);
            }
            return result;
        }

        // Overload for single recipe
        private RecipeDTO MapRecipeToDTO(Recipe recipe)
        {
            if (recipe == null)
            {
                return null;
            }

            return MapRecipeToDTO(new List<Recipe> { recipe }).FirstOrDefault();
        }

        public Paginate<RecipeDTO> MapPaging(Paginate<Recipe> list, List<RecipeDTO> items)
        {
            var result = new Paginate<RecipeDTO>();


            result.Items = items;
            result.Size = list.Size;
            result.Page = list.Page;
            result.TotalPages = list.TotalPages;
            result.Total = list.Total;

            return result;
        }


        private string CalDifficultLevel(List<StepDTO> steps)
        {
            if (!steps.Any() || steps == null) return "N/A";

            int count = 0;
            for (int i = 0; i < steps.Count; i++) 
            {
                count = count + 1;
            }

            if (count <= 3) return "Easy";
            else if (count >= 4 && count <= 6) return "Medium";
            else return "Difficult";
        }

        //private async Task<RecipeNutritionDTO> GetRecipeNutrition(int id)
        //{
        //    RecipeNutritionDTO recipeNutrition = new RecipeNutritionDTO();
        //    var l =(List<RecipeIngredient>)await _recipeIngrementRepo.GetListAsync(
        //                    selector: x => x,
        //                    predicate: x => x.RecipeId.Equals(id),
        //                    include: x => x.Include(x => x.Recipe)
        //                                   .Include(x => x.Ingredient)
        //                        );

        //    foreach (var ri in l){
        //        NutritionInfo ingredientNutrition = ri.Ingredient.NutritionInfo;
        //        recipeNutrition.Calories = recipeNutrition.Calories + ingredientNutrition.Calories;
        //        recipeNutrition.Protein = recipeNutrition.Protein + ingredientNutrition.Protein;
        //        recipeNutrition.Fat = recipeNutrition.Fat + ingredientNutrition.Fat;
        //        recipeNutrition.Carbs = recipeNutrition.Carbs + ingredientNutrition.Carbs;
        //        recipeNutrition.Fiber = recipeNutrition.Fiber + ingredientNutrition.Fiber;
        //        recipeNutrition.Sugar = recipeNutrition.Sugar + ingredientNutrition.Sugar;
        //    }

        //    return recipeNutrition;
        //}


        private async Task<List<int>> GetIngredientList(int id)
        {
            return (List<int>)await _recipeIngredientRepo.GetListAsync(
                            selector: x => x.Ingredient.IngredientId,
                            predicate: x => x.RecipeId.Equals(id),
                            include: x => x.Include(x => x.Recipe)
                                           .Include(x => x.Ingredient)
                                );

        }
        private async Task<List<RecipeIngredientDTO>> GetRecipeIngredient(int id)
        {
            var list = (List<RecipeIngredient>)await _recipeIngredientRepo.GetListAsync(
                            selector: x => x,
                            predicate: x => x.RecipeId.Equals(id),
                            include: x => x.Include(x => x.Recipe)
                                           .Include(x => x.Ingredient)
                                );

            var result = new List<RecipeIngredientDTO>();

            foreach (var entity in list)
            {
                var dto = new RecipeIngredientDTO
                {


                    RecipeId = entity.RecipeId,
                    IngredientId = entity.IngredientId,
                    Name =  entity.Ingredient.Name,
                    Type =  entity.Ingredient.Type,
                    Quantity = entity.Quantity,
                    Unit = entity.Unit
                };

                result.Add(dto);
            }

            return result;

        }



        private async Task<List<StepDTO>> GetStepInRecipe(int id)
        {

            var steps = (List<Step>)await _stepIngrementRepo.GetListAsync(
                 selector: x => x,
                 predicate: x => x.RecipeId.Equals(id),
                 orderBy: x => x.OrderBy(x => x.StepNumber)
                 );

            return steps.Select(step => new StepDTO
            {
              id = step.StepId,
              StepNumber = step.StepNumber,
              Description = step.Description,
              //Image = step.Image,
              Duration = step.Duration,
              ToolsRequired = step.ToolsRequired
            }).ToList();
        }

        private List<Recipe> SearchByIngredient(IEnumerable<Recipe> recipeList, List<int> requests)
        {
            var result = new List<Recipe>();

            foreach (var recipe in recipeList)
                {
                    List<int> ingredientsInRecipe = GetIngredientList(recipe.RecipeId).Result;

                    bool isSubset = requests.All(e => ingredientsInRecipe.Contains(e));
                    if (isSubset)
                    {
                        result.Add(recipe);
                    }
                }
            
            return result;
        }

        private int IdGenerator()
        {
            var maxId = (int) _genericRepository.SingleOrDefaultAsync(
                            selector: x => x.RecipeId,
                            //predicate: x => x.RecipeId.Equals(id),
                            orderBy: x => x.OrderByDescending(x => x.RecipeId)
                                ).Result;

            tmpId = maxId + 1;
            return maxId + 1;
        }

        public int GetCurrentId()
        {
            int currentId = tmpId;
            tmpId = 0;

            return currentId;
        }

        private async void UserViewedRecipe(int? id)
        {
            var result = (User)await _userRepo.SingleOrDefaultAsync(
                 selector: x => x,
                 predicate: x => x.UserId.Equals(id)                    
                );
            result.ViewedRecipe++;

            await _userRepo.UpdateAsync(result);
        }

    }

}
