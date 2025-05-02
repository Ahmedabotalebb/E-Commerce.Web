 using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceApstraction;
using Shared.DataTransfereObjects.IdentityDto;

namespace Service
{
    public class AuthenticationService (UserManager<ApplicationUser> _userManager,IConfiguration configuration , IMapper mapper): IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string Email)
        {
            var user= await _userManager.FindByEmailAsync(Email);
            if (user != null) 
                return true;
            else
                return false; 
        }

        public async Task<AddressDto> GetCurrentAddressAsync(string Email)
        {
            var user =await _userManager.Users.Include(U=>U.Address)
                .FirstOrDefaultAsync(u=>u.Email==Email) ?? throw new UserNotFoundException(Email);
            if(user.Address is not null)
                return mapper.Map<Address,AddressDto>(user.Address);
            else throw new AddressNotFoundException(user.UserName); 
        }

        public async Task<UserDto> GetCurrentUserAync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await CreateTokenAsync(user)
            };
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(U => U.Address)
               .FirstOrDefaultAsync(u => u.Email == Email) ?? throw new UserNotFoundException(Email);
            if(user is not null)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LasstName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.street = addressDto.Street;
            }
            else
            {
                user.Address = mapper.Map<AddressDto,Address>(addressDto);
            }
            _userManager.UpdateAsync(user);
            return mapper.Map<AddressDto>(addressDto);
        }
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
                    Token = await CreateTokenAsync(user)
                };

            }
            else
            {
                throw new UnauthorizedException();
            } 

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
                return new UserDto() { DisplayName = user.DisplayName, Email = user.Email, Token =await CreateTokenAsync(user) };
            }
            else

            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
                    
             }

            
        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),

            };
            var Roles = await _userManager.GetRolesAsync(user);

            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecretKey = configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: configuration.GetSection("JWTOptions")["Issuer"],
                audience: configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds);
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}