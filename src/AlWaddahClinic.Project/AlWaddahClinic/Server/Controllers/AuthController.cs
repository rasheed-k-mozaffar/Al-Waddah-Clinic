using Microsoft.AspNetCore.Mvc;



namespace AlWaddahClinic.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthRepository _authRepository;

        public AuthController(ILogger<AuthController> logger, IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDto model)
        {
            
            if(ModelState.IsValid)
            {
                //The model passes the validations
                var result = await _authRepository.LoginUserAsync(model);

                if (result.HasSucceeded)
                {
                    //Valid login credentials
                    return Ok(new { Token = result.Message }); // Return 200 and the access token.
                }
                else
                {
                    //Invalid login credentials
                    return BadRequest(new ApiErrorResponse { Message = "Invalid credentials"});
                }
            }
            else
            {
                //The model doesn't pass the validations
                return BadRequest(ModelState);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto model)
        {
            //TODO Implement the registeration
            return Ok("You are registered... I promise :)");
        }
    }
}

