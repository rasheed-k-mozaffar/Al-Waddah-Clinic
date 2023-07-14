using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using AlWaddahClinic.Server.Options;
using Microsoft.IdentityModel.Tokens;

namespace AlWaddahClinic.Server.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly Options.IdentityOptions _options;

        public AuthRepository(IConfiguration configuration, UserManager<AppUser> userManager, Options.IdentityOptions options)
        {
            _configuration = configuration;
            _userManager = userManager;
            _options = options;
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

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto model, Guid clinicId)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                throw new UserCreationFailed("A user already exists with the same email address");
            }

            AppUser user = new AppUser
            {
                UserName = model.Email,
                ClinicId = clinicId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    HasSucceeded = true
                };
            }
            else
            {
                throw new UserCreationFailed("Something has gone wrong while registering the user");
            }
        }

        public async Task<UserManagerResponse> AuthorizeUserAsync(string userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);

            var result = CheckIfEmailIsVerifiedAsync(user!);

            if (result)
            {
                return new UserManagerResponse
                {
                    Message = "Email is already verified",
                    HasSucceeded = false
                };
            }
            else
            {
                user.EmailConfirmed = true;
                await _userManager.AddToRoleAsync(user!, "Admin");
                await _userManager.UpdateAsync(user);

                var token = await GenerateJwtTokenForUserAsync(user);
                return new UserManagerResponse
                {
                    Message = token,
                    HasSucceeded = true
                };
            }
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
                new Claim("ClinicId", user.ClinicId.ToString())
            };

            //// Email Confirmation Check.
            //if(user.EmailConfirmed)
            // {
            //     claims.Add(new Claim("email_verified", "true"));
            //     claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            // }
            // else
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, "User"));
            // }

            if(await _userManager.IsInRoleAsync(user, "Admin"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
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

        private bool CheckIfEmailIsVerifiedAsync(AppUser user)
        {
            if (user.EmailConfirmed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

