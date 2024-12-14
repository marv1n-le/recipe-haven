using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.requestDTOs
{
    public class OriginRequest
    {
        public string Country { get; set; }

        public string Region { get; set; }

        public string CultureDescription { get; set; }
    }
}
