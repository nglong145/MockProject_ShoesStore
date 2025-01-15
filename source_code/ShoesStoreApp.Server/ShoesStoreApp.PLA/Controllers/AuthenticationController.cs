using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.AuthenticationService;
using ShoesStoreApp.BLL.ViewModels.Auth;
using System.Security.Claims;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVm registerVm)
        {
            try
            {
                var result = await _authenticationService.RegisterUserAsync(registerVm);
                return Created(nameof(Register), result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVm loginVm)
        {
            try
            {
                var result = await _authenticationService.LoginUserAsync(loginVm);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                var result = await _authenticationService.RefreshTokenAsync(refreshToken);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "User is not authenticated." });
                }

                var userInfo = await _authenticationService.GetUserInfoAsync(Guid.Parse(userId));
                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPut("update-user-info")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserVm updateUserVm)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "User is not authenticated." });
                }

                var isUpdated = await _authenticationService.UpdateUserInfoAsync(Guid.Parse(userId), updateUserVm);
                if (!isUpdated)
                {
                    return BadRequest(new { Message = "Failed to update user information." });
                }

                return Ok(new { Message = "User information updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
