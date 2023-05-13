using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UserManagement.Core.DTO;
using UserManagement.Core.IdentityEntities;
using UserManagement.Core.ServiceInterfaces;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// Account controller for user management. including authenticaiton, authorization and user profile data.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AccountController> _logger;
        /// <summary>
        /// controller constructor along with injected depenedencies
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="signInManager"></param>
        /// <param name="jwtService"></param>
        /// <param name="logger"></param>

        public AccountController(
            IAccountService accountService,
            SignInManager<AppUser> signInManager,
            IJwtService jwtService,
            ILogger<AccountController> logger
            )
        {
            _accountService = accountService;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _logger = logger;
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponse>> Register(RegisterDto registerDto)
        {
            try
            {
                //validation
                if (await _accountService.IsUserNameExist(registerDto.UserName)) return Problem("Username is already exist", statusCode: 400);

                if (!ModelState.IsValid)
                {
                    string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return Problem(errorMessage, statusCode: 400);
                }

                AppUser user = _accountService.CreateIdentityUser(registerDto);
                IdentityResult result = await _accountService.InsertIntoDbAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);
                    var authenticationResponse = _jwtService.CreateJwtToken(user);

                    _logger.LogInformation("A new user has been registred with username: {username}", authenticationResponse.Username);
                    return Ok(authenticationResponse);
                }

                else
                {
                    string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                    return Problem(errorMessage, statusCode: 400);
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Problem(errorMessage, statusCode: 400);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, true, false);

            if (result.Succeeded)
            {
                var user = await _accountService.FindUserByUserNameAsync(loginDto.UserName);
                if (user == null) return NoContent();
                var authenticationResponse = _jwtService.CreateJwtToken(user);

                _logger.LogInformation("the user {name} has been logged in", authenticationResponse.Username);
                return Ok(authenticationResponse);
            }

            return Problem("Invalid email/password", statusCode: 400);
        }

        /// <summary>
        /// User Logout
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<AppUser>> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        /// <summary>
        /// Reset User Password
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader["Bearer ".Length..];
                var tokenHandler = new JwtSecurityTokenHandler();

                var jwt = tokenHandler.ReadJwtToken(token);


                var userId = jwt.Subject;

                var user = await _accountService.FindUserByIdAsync(userId);
                if (user == null) return NotFound("User not found");

                var result = await _accountService.ResetUserPassword(user, resetPasswordDto);
                if (result.Succeeded)
                {
                    _logger.LogInformation("The user: {name} has changed their password", user.UserName);
                    return Accepted("Password has changed successfullu");
                }

                else
                {
                    string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                    return Problem(errorMessage, statusCode: 400);
                }
            }

            return Problem("Invalid token");
        }

        /// <summary>
        /// Get User Profile (Not Implemented Yet)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Profile()
        {
            return Problem("Not implemented yet");
        }

    }
}
