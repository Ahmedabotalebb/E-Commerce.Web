using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceApstraction;
using Shared.DataTransfereObjects.IdentityDto;

namespace Service
{
    class AuthenticationService (UserManager<ApplicationUser> _userManager): IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UserNotFoundException(loginDto.Email);

            bool IsPasswordVallid = await _userManager.CheckPasswordAsync(user,loginDto.Password);
            if (IsPasswordVallid)
            {
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = CreateTokenAsync(user)
                };

            }
            else
            {
                throw new UnauthorizedException();
            } 

        }


        private static string CreateTokenAsync(ApplicationUser user)
        {
            return "Token - TODO";
        }
        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto() { DisplayName = user.DisplayName, Email = user.Email, Token = CreateTokenAsync(user) };
            }
            else

            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
                    
             }

            
        }

    }
}