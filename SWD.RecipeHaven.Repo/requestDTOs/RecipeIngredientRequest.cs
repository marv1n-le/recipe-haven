using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.requestDTOs
{
    public class RecipeIngredientRequest
    {

        //public int RecipeId { get; set; }

        public int IngredientId { get; set; }

        public double? Quantity { get; set; }

        public string Unit { get; set; }
    }
}
