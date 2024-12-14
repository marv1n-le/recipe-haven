using SWD.RecipeHaven.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.DTOs
{
    public class StepDTO
    {

        public int id { get; set; }
        public int StepNumber { get; set; }

        public string Description { get; set; }

        //public string Image { get; set; }

        public int? Duration { get; set; }

        public string ToolsRequired { get; set; }

    }
}
