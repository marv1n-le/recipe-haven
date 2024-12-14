using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.DTOs
{
    public class OriginDTO
    {
        public int Id { get; set; }
        public string Country { get; set; }

        public string Region { get; set; }

        public string CultureDescription { get; set; }
    }
}
