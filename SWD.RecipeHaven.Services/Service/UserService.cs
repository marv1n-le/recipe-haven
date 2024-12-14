using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Repo.responseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Services.Service
{
    public interface IUserCRUDService
    {
        Task<UserResponseDTO> CreateUser(UserRequestDTO userRequest);
        Task<UserResponseDTO> UpdateUser(int id, UserRequestDTO userRequest);
        Task<bool> DeleteUser(int id);
        Task<UserResponseDTO> GetUserById(int id);
        Task<Paginate<UserResponseDTO>> GetAllUser(int page);
        Task<int> GetUserViewed(int? id);
    }

    public class UserService : IUserCRUDService
    {
        private readonly GenericRepository<User> _userRepository;

        public UserService()
        {
            _userRepository = new GenericRepository<User>();
        }

        public async Task<UserResponseDTO> CreateUser(UserRequestDTO userRequest)
        {
            var newUser = new User
            {
                Username = userRequest.Username,
                Password = userRequest.Password,
                Role = userRequest.Role,
                Fullname = userRequest.Fullname,
                Email = userRequest.Email,
                ActiveStatus = true,
                CreatedAt = DateTime.Now
            };

            await _userRepository.CreateAsync(newUser);
            return new UserResponseDTO
            {
                UserId = newUser.UserId,
                Username = newUser.Username,
                Role = newUser.Role,
                Fullname = newUser.Fullname,
                Email = newUser.Email
            };
        }

        public async Task<UserResponseDTO> UpdateUser(int id, UserRequestDTO userRequest)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found.");

            user.Username = userRequest.Username;
            user.Password = userRequest.Password;
            user.Role = userRequest.Role;
            user.Fullname = userRequest.Fullname;
            user.Email = userRequest.Email;

            await _userRepository.UpdateAsync(user);
            return new UserResponseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role,
                Fullname = user.Fullname,
                Email = user.Email
            };
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found.");

            user.ActiveStatus = false;

            return await _userRepository.RemoveAsync(user);
        }

        public async Task<UserResponseDTO> GetUserById(int id)
        {
            //var user = await _userRepository.GetByIdAsync(id);
            var user = await _userRepository.SingleOrDefaultAsync(
                selector: x => x,
                predicate: x => x.ActiveStatus == true && x.UserId == id
                );


            if (user == null) throw new Exception("User not found.");


            return new UserResponseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role,
                Fullname = user.Fullname,
                Email = user.Email,
                CreateAt = user.CreatedAt
              
                
            };
        } 
        public async Task<int> GetUserViewed(int? id)
        {
            //var user = await _userRepository.GetByIdAsync(id);
            //int amout = int.MaxValue;
              var amout = await _userRepository.SingleOrDefaultAsync(
                selector: x => x.ViewedRecipe,
                predicate: x =>  x.UserId == id
                );


            if (amout == null) return int.MaxValue;


            return amout.Value;
        } 
        public async Task<Paginate<UserResponseDTO>> GetAllUser(int page)
        {
            var result = await _userRepository.GetPagingListAsync(
                     selector: x => x,
                     page: page,
                     size: 10,
                     predicate: x => x.ActiveStatus == true);


            if (result == null)
            {
                throw new Exception("No Result");
            }

            var paginate = new Paginate<UserResponseDTO>();
            paginate.Items = MapObjectToDTO(result.Items);
            paginate.Size = result.Size;
            paginate.Page = result.Page;
            paginate.TotalPages = result.TotalPages;
            paginate.Total = result.Total;

            return paginate;


        }
       

        private List<UserResponseDTO> MapObjectToDTO(IEnumerable<User> users)
        {
            if (users == null || !users.Any())
            {
                return new List<UserResponseDTO>();
            }

            List<UserResponseDTO> result = new List<UserResponseDTO>();

            foreach (var user in users)
            {
                var dto = new UserResponseDTO
                {
                   Fullname = user.Fullname, 
                   Email = user.Email,    
                   Username = user.Username,
                   Role = user.Role,
                   CreateAt=user.CreatedAt,
                   UserId = user.UserId   
                 
                   
                };

                result.Add(dto);
            }
            return result;
        }


    }
}
