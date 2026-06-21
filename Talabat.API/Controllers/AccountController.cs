using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper, IAuthService authService , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authService = authService;
           _signInManager = signInManager;
        }



        [ProducesResponseType(typeof(UserToReturnDto) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status400BadRequest)]
        [HttpPost("Register")]
        public async Task<ActionResult<UserToReturnDto>> Register(RegisterDto registerDto)
        {
            // Validation Error  لان انا بالفعل مظبطه ال  ModelState انا هنا مش محتاجه ان اعمل 
            var appUser = _mapper.Map<ApplicationUser>(registerDto);

            var result = await _userManager.CreateAsync(appUser , registerDto.Password);
            if (result.Succeeded)
            {
                // Create a token 
                var token = await _authService.CreateTokenAsync(appUser, _userManager);
                var userToReturnDto = new UserToReturnDto()
                {
                    DisplayName = appUser.DisplayName,
                    Email = appUser.Email,
                    Token = token
                };

                return Ok(userToReturnDto);
            }
            else
                return BadRequest(new ApiResponse(400, result.Errors.First().Description));
        }




        [ProducesResponseType(typeof(UserToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [HttpPost("Login")]
        public async Task<ActionResult<UserToReturnDto>> Login(LoginDto loginDto)
        {
            var foundUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if (foundUser is not null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(foundUser, loginDto.Password, false);
                if (result.Succeeded)
                {
                    string token = await _authService.CreateTokenAsync(foundUser, _userManager);
                    return Ok(new UserToReturnDto()
                    {
                        DisplayName = foundUser.DisplayName,
                        Email = foundUser.Email,
                        Token = token
                    });
                }
            }
            return Unauthorized(new ApiResponse(401));

        }













    }
}
