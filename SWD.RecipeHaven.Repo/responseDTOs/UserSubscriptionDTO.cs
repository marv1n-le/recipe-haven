using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.responseDTOs
{
    public class UserSubscriptionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? ActiveStatus { get; set; }
    }
}
