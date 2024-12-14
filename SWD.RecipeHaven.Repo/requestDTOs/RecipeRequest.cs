using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.requestDTOs
{
    public class RecipeRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int? PreparationTime { get; set; }

        public int? CookingTime { get; set; }

        public int? Servings { get; set; }

        public string? Image { get; set; }

        public string? CookingMethod { get; set; }

        public int? Category { get; set; }

        public int? Author { get; set; }

        public int? Origin { get; set; }

        public List<RecipeIngredientRequest> Ingrediens { get; set; }

        public List<StepRequest> Steps { get; set; }

    }
}
