using SWD.RecipeHaven.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int? PreparationTime { get; set; }

        public int? CookingTime { get; set; }

        public int? Servings { get; set; }

        public string Image { get; set; }
        public string? DifficultLevel { get; set; }

        public string? CookingMethod { get; set; }

        public string? Category { get; set; }

        public string? Author { get; set; }

        public string? Origin { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastUpdated { get; set; }

        //public RecipeNutritionDTO Nutrition { get; set; }

        public List<RecipeIngredientDTO> Ingredients { get; set; }

        public List<StepDTO> Steps { get; set; }
    }
}
