using api.Dtos.Auth;
using api.Interfaces;
using api.Mapping;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly ITokenService _tokenSer;

        public AuthController(
            UserManager<AppUser> userManager,
            ITokenService tokenSer,
            SignInManager<AppUser> signInManager
        )
        {
            _singInManager = signInManager;
            _tokenSer = tokenSer;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = registerDto.ToAppUser();
            try
            {
                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roles = await _userManager.AddToRoleAsync(user, "user");
                    if (!roles.Succeeded)
                    {
                        return StatusCode(500, roles.Errors);
                    }
                    else
                    {
                        return Ok(
                            new RegisterResponceDto
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = _tokenSer.CreateToken(user)
                            }
                        );
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user =>
                user.UserName == loginDto.UserName
            );
            if (user is null)
            {
                return Unauthorized("User name and/or password is Incorrect");
            }

            var result = await _singInManager.CheckPasswordSignInAsync(
                user:user,
                password: loginDto.Password,
                lockoutOnFailure: false
            );

            if (!result.Succeeded)
            {
                return Unauthorized("User name and/or password is Incorrect");
            }
            return Ok(
                new RegisterResponceDto
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = _tokenSer.CreateToken(user)
                }
            );
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetValues()
        {
            return Ok(new string[] { "value1", "value2" });
        }
    }
}
