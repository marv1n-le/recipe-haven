using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using SWD.RecipeHaven.Repo.responseDTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Services.Service
{
    public interface IUserService
    {
        Task<UserResponseDTO> Login(LoginRequestDTO loginRequest);
    }

    public class LoginService : IUserService
    {
        private readonly GenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IConfiguration configuration)
        {
            _userRepository = new GenericRepository<User>();
            _configuration = configuration;
        }

        public async Task<UserResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            var user = await _userRepository.SingleOrDefaultAsync(
                x => x,
                predicate: u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var token = GenerateJwtToken(user);
           

            return new UserResponseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role,
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim("role", user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:ExpireDays"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        } 
    }
}




//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using SWD.RecipeHaven.Data.Models;
//using SWD.RecipeHaven.Repo.DTOs;
//using SWD.RecipeHaven.Repo.requestDTOs;
//using SWD.RecipeHaven.Repo.responseDTOs;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace SWD.RecipeHaven.Services.Service
//{
//    public interface IUserService
//    {
//        Task<UserResponseDTO> Login(LoginRequestDTO loginRequest);
//    }

//    public class UserService : IUserService
//    {
//        private readonly GenericRepository<User> _userRepository;
//        private readonly IConfiguration _configuration;

//        public UserService(IConfiguration configuration)
//        {
//            _userRepository = new GenericRepository<User>();
//            _configuration = configuration;
//        }

//        public async Task<UserResponseDTO> Login(LoginRequestDTO loginRequest)
//        {
//            var user = await _userRepository.SingleOrDefaultAsync(
//                x => x,
//                predicate: u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);

//            if (user == null)
//                throw new UnauthorizedAccessException("Invalid username or password.");

//            var token = GenerateJwtToken(user);

//            return new UserResponseDTO
//            {
//                UserId = user.UserId,
//                Username = user.Username,
//                Role = user.Role,
//                Token = token
//            };
//        }

//        private string GenerateJwtToken(User user)
//        {
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
//            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
//            new Claim("role", user.Role),
//            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//        };

//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:ExpireDays"])),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using SWD.RecipeHaven.Data.Models;
//using SWD.RecipeHaven.Repo.DTOs;
//using SWD.RecipeHaven.Repo.requestDTOs;
//using SWD.RecipeHaven.Repo.responseDTOs;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace SWD.RecipeHaven.Services.Service
//{
//    public interface IUserService
//    {
//        Task<UserResponseDTO> Login(LoginRequestDTO loginRequest);
//    }

//    public class UserService : IUserService
//    {
//        private readonly GenericRepository<User> _userRepository;
//        private readonly IConfiguration _configuration;

//        public UserService(IConfiguration configuration)
//        {
//            _userRepository = new GenericRepository<User>();
//            _configuration = configuration;
//        }

//        public async Task<UserResponseDTO> Login(LoginRequestDTO loginRequest)
//        {
//            var user = await _userRepository.SingleOrDefaultAsync(
//                x => x,
//                predicate: u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);

//            if (user == null)
//                throw new UnauthorizedAccessException("Invalid username or password.");

//            var token = GenerateJwtToken(user);

//            return new UserResponseDTO
//            {
//                UserId = user.UserId,
//                Username = user.Username,
//                Role = user.Role,
//                Token = token
//            };
//        }

//        private string GenerateJwtToken(User user)
//        {
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
//            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
//            new Claim("role", user.Role),
//            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//        };

//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:ExpireDays"])),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using SWD.RecipeHaven.Data.Models;
//using SWD.RecipeHaven.Repo.DTOs;
//using SWD.RecipeHaven.Repo.requestDTOs;
//using SWD.RecipeHaven.Repo.responseDTOs;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace SWD.RecipeHaven.Services.Service
//{
//    public interface IUserService
//    {
//        Task<UserResponseDTO> Login(LoginRequestDTO loginRequest);
//    }

//    public class UserService : IUserService
//    {
//        private readonly GenericRepository<User> _userRepository;
//        private readonly IConfiguration _configuration;

//        public UserService(IConfiguration configuration)
//        {
//            _userRepository = new GenericRepository<User>();
//            _configuration = configuration;
//        }

//        public async Task<UserResponseDTO> Login(LoginRequestDTO loginRequest)
//        {
//            var user = await _userRepository.SingleOrDefaultAsync(
//                x => x,
//                predicate: u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);

//            if (user == null)
//                throw new UnauthorizedAccessException("Invalid username or password.");

//            var token = GenerateJwtToken(user);

//            return new UserResponseDTO
//            {
//                UserId = user.UserId,
//                Username = user.Username,
//                Role = user.Role,
//                Token = token
//            };
//        }

//        private string GenerateJwtToken(User user)
//        {
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
//            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
//            new Claim("role", user.Role),
//            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//        };

//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:ExpireDays"])),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}

