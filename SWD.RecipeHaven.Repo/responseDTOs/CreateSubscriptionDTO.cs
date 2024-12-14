using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.responseDTOs
{
    public class CreateSubscriptionDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public bool? ActiveStatus { get; set; }
    }
}
