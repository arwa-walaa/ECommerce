using ECommerce.Shared.DTOS.IdentityDTOs;
using ECommerceServiceApstarction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePresentation.Controllers
{
    public class AuthController : ApiBaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);
            return HandelResult(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login( LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);
            return HandelResult(result);
        }

    }
}
