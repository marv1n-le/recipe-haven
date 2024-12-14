using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.DTOs
{
    public class RecipeIngredientDTO
    {
        public int RecipeId { get; set; }

        public int IngredientId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public double? Quantity { get; set; }

        public string Unit { get; set; }
    }
}
