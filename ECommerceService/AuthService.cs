using ECommerce.Shared.CommenResults;
using ECommerce.Shared.DTOS.IdentityDTOs;
using ECommerceDomain.Entities.IdentityModule;
using ECommerceServiceApstarction;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
           _configuration = configuration;
        }

        public   async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;

        }

        public async Task<Result<UserDTO>> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Error.NotFound("User not found.");
            }
            return new UserDTO(user.Email!, user.DisplayName!, await CreateJwtTokenAsync( user));
        }

        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return Error.InvalidCredentials("Invalid email or password.");
            }
            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!IsPasswordValid)
            {
                return Error.InvalidCredentials("Invalid password.");

            }
            var token = await CreateJwtTokenAsync(user);
            return new UserDTO(user.Email!, user.DisplayName!, token );


        }
        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                DisplayName = registerDTO.DisplayName,
                PhoneNumber = registerDTO.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                var token = await CreateJwtTokenAsync(user);
                return new UserDTO(user.Email, user.DisplayName, token);

            }
            return result.Errors.Select(E => Error.Validation(E.Code, E.Description)).ToList();


        }

        //method to craete jwt token
        private async Task<string> CreateJwtTokenAsync(ApplicationUser user)
        {
            // Implementation for JWT token creation goes here
            //claims, Roles, SecretKey
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName!),
             
            };
            var roles =await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }
            //secret key
            var secretKey =  _configuration["JWTOptions:SecretKey"];

            //endode
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //create token
            var token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);



        }

    }
}
