using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.requestDTOs
{
    public class PaymentRequestDTO
    {
        public int productId { get; set; }
        //public string productName { get; set; }
        //public string description { get; set; }
        public int price { get; set; }
        public string returnUrl { get; set; }
        public string cancelUrl { get; set; }
    }
}
