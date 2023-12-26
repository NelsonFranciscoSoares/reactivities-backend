using System.Security.Claims;
using API.DTOs.User;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, TokenService tokenService)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginUserDto loginUserDto)
        {
            var user = await this._userManager.FindByEmailAsync(loginUserDto.Email);

            if (user == null) return Unauthorized();

            var result = await this._userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (result == false) return Unauthorized();

            return new UserDto {
                AccessToken = this._tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                Image = null,
                Username = user.UserName
            };
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerUserDto)
        {
            if (await this._userManager.Users.AnyAsync(user => user.UserName == registerUserDto.Username))
            {
                ModelState.AddModelError("username", "Username is already taken");
                return ValidationProblem();
            }

            if (await this._userManager.Users.AnyAsync(user => user.Email == registerUserDto.Email))
            {
                ModelState.AddModelError("email", "Email is already taken");
                return ValidationProblem();
            }

            var appUser = new AppUser {
                DisplayName = registerUserDto.DisplayName,
                Email = registerUserDto.Email,
                UserName = registerUserDto.Username
            };

            var result = await this._userManager.CreateAsync(appUser, registerUserDto.Password);

            if(result.Succeeded) {
                return new UserDto {
                    DisplayName = appUser.DisplayName,
                    Image = null,
                    Username = appUser.UserName,
                    AccessToken = this._tokenService.CreateToken(appUser)
                };
            }

            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var appUser = await this._userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            return CreateUserObject(appUser);
        }

        private UserDto CreateUserObject(AppUser appUser) 
        {
            return new UserDto {
                DisplayName = appUser.DisplayName,
                Image = null,
                Username = appUser.UserName,
                AccessToken = this._tokenService.CreateToken(appUser)
            };
        }
    }
}