using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Repo.responseDTOs
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime? CreateAt { get; set; }

    }
}
