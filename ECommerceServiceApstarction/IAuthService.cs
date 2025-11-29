using ECommerce.Shared.CommenResults;
using ECommerce.Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServiceApstarction
{
    public interface IAuthService
    {
     
        //login

        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);


        //register
        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);

         Task<bool> CheckEmailAsync(string email);

        Task<Result<UserDTO>> GetUserByEmailAsync(string email);
    }
}
