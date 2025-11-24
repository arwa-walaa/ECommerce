using ECommerce.Shared.CommenResults;
using ECommerce.Shared.DTOS.IdentityDTOs;
using ECommerceDomain.Entities.IdentityModule;
using ECommerceServiceApstarction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
            return new UserDTO(user.Email, user.DisplayName, "Token");


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
                return new UserDTO(user.Email, user.DisplayName, "Token");

            }
            return result.Errors.Select(E => Error.Validation(E.Code, E.Description)).ToList();


        }
    }
}
