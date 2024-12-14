using SWD.RecipeHaven.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.DTOs
{
    public class IngredientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }
    }
}
