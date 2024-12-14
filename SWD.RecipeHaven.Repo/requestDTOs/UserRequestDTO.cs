using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.requestDTOs
{
    public class UserRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}
