using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApstraction;
using Shared.DataTransfereObjects.IdentityDto;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager serviceManager):ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user= await serviceManager.authenticationService.LoginAsync(loginDto);
            return Ok(user);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user= await serviceManager.authenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }


        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = await serviceManager.authenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public  async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var appuser=await serviceManager.authenticationService.GetCurrentUserAync(email!);
            return Ok(appuser);
        }


        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await serviceManager.authenticationService.GetCurrentUserAddressAsync(email);
            return Ok(Address);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddressAsync(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress= await serviceManager.authenticationService.UpdateCurrentUserAddressAsync(email, address);
            return Ok(UpdatedAddress);
        }
    }
}
