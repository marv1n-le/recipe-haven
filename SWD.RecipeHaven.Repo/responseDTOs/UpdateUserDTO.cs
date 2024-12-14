using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.DTOs
{
    public class UpdateUserDTO
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
