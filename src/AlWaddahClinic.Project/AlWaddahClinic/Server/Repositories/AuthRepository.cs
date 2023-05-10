﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AlWaddahClinic.Server.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public AuthRepository(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                //Check if the user's password is correct and matches the given email.
                var result = await _userManager.CheckPasswordAsync(user, model.Password);

                if (result)
                {
                    //Password is correct
                    string token = await GenerateJwtTokenForUserAsync(user);

                    return new UserManagerResponse
                    {
                        Message = token,
                        HasSucceeded = true
                    };
                }
                else
                {
                    //Password is incorrect
                    return new UserManagerResponse
                    {
                        Message = "Invalid email or password",
                        HasSucceeded = false
                    };
                }
            }

            return new UserManagerResponse
            {
                Message = "Email doesn't exist",
                HasSucceeded = false
            };

        }

        public Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto model)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenerateJwtTokenForUserAsync(AppUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));
            var sigingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
            };

            //Check if the user logging in is an admin user
            var result = await _userManager.IsInRoleAsync(user, "Admin");

            if (result)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            //Creating the token with the claims
            var token = new JwtSecurityToken
                (
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: sigingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
