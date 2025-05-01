using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransfereObjects.IdentityDto;

namespace ServiceApstraction
{
    public interface IAuthenticationService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync( RegisterDto registerDto);
    }
}
