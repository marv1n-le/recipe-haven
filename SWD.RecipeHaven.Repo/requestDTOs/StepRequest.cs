using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.requestDTOs
{
    public class StepRequest
    {
        public int StepNumber { get; set; }

        public string Description { get; set; }

        public string? Image { get; set; } = null;

        public int? Duration { get; set; }

        public string ToolsRequired { get; set; }
    }
}
