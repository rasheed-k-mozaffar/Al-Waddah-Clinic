using Microsoft.AspNetCore.Mvc;
using AlWaddahClinic.Server.Extensions;
using AlWaddahClinic.Server.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AlWaddahClinic.Server.Options;

namespace AlWaddahClinic.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthRepository _authRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly IdentityOptions _options;

        public AuthController(ILogger<AuthController> logger, IAuthRepository authRepository, IClinicRepository clinicRepository, ClinicDbContext context, IdentityOptions options) : base(context)
        {
            _logger = logger;
            _authRepository = authRepository;
            _clinicRepository = clinicRepository;
            _options = options;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDto model)
        {

            if (ModelState.IsValid)
            {
                //The model passes the validations
                var result = await _authRepository.LoginUserAsync(model);

                if (result.HasSucceeded)
                {
                    //Valid login credentials
                    return Ok(new ApiResponse<LoginResult> { Message = "Access token retrieved successfully", Value = new LoginResult { Token = result.Message, HasSucceeded = true }, IsSuccess = true }); // Return 200 and the access token.
                }
                else
                {
                    //Invalid login credentials
                    return BadRequest(new ApiErrorResponse { Message = "Invalid credentials" });
                }
            }
            else
            {
                //The model doesn't pass the validations
                return BadRequest(ModelState);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterClinicDto model)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var clinic = model.ToClinicRegister();

                    try
                    {
                        var clinicRegisterationResult = await _clinicRepository.CreateClinicAsync(clinic);

                        try
                        {
                            //This will call the register service and create a new user in the database with their respective clinic ID
                            var result = await _authRepository.RegisterUserAsync(model.RegisterUser, clinic.Id);

                            if (result.HasSucceeded)
                            {
                                transaction.Commit();
                                return Ok(new ApiResponse
                                {
                                    Message = "Clinic and user were registered successfully",
                                    IsSuccess = true
                                });
                            }
                        }
                        catch (UserCreationFailed ex)
                        {
                            transaction.Rollback();
                            return BadRequest(new ApiErrorResponse
                            {
                                Message = ex.Message
                            });
                        }
                    }
                    catch (ClinicRegisterationFailedException ex)
                    {
                        transaction.Rollback();
                        return BadRequest(new ApiErrorResponse
                        {
                            Message = ex.Message
                        });
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ConfirmUserEmail()
        {

            var result = await _authRepository.AuthorizeUserAsync();

            if (result.HasSucceeded)
            {
                return Ok(new ApiResponse
                {
                    Message = "User email was verified successfully",
                    IsSuccess = true
                });
            }
            else
            {
                return BadRequest(new ApiErrorResponse
                {
                    Message = "User email couldn't be verified, please try again"
                });
            }
        }
    }
}


