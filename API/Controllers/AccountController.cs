using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dto;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
  public class AccountController : BaseAPiController
  {
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    public AccountController(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
      _mapper = mapper;
      _tokenService = tokenService;
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
      var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)
                    .Value;

      var user = await _userManager.FindByEmailAsync(email);

      return new UserDto
      {
        Email = user.Email,
        Token = _tokenService.CreateToke(user),
        DisplayName = user.DisplayName,
      };

    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
      return await _userManager.FindByEmailAsync(email) != null;

    }



    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AdressDto>> GetUserAdress()
    {
      //httpcontext는 여기가 콘트롤러이기때문에 사용가능하지만 다른데선 안된다 
      var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)
                  .Value;
      var user = await _userManager.FIndByEmailWithAddressAsync(HttpContext.User);

      var AdressDto = _mapper.Map<Adress, AdressDto>(user.Adress);

      return AdressDto;

    }



    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
      var user = await _userManager.FindByEmailAsync(loginDto.Email);

      if (user == null)
        return Unauthorized(new ApiResponse(401));

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

      if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

      return new UserDto
      {
        Email = user.Email,
        Token = _tokenService.CreateToke(user),
        DisplayName = user.DisplayName,
      };

    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AdressDto>> UpdateUserAddress(AdressDto adressDto)
    {
      //접속해있는 유저 갖고와서 
      var user = await _userManager.FIndByEmailWithAddressAsync(HttpContext.User);
      //유저의 원래 어드레스를 업데이트 시켜주고 
      user.Adress = _mapper.Map<AdressDto, Adress>(adressDto);

      var result = await _userManager.UpdateAsync(user);

      if (result.Succeeded) return Ok(_mapper.Map<Adress, AdressDto>(user.Adress));

      return BadRequest("Problem  updatiing the user");

    }



    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
      {
        return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
      }

      var user = new AppUser
      {
        DisplayName = registerDto.DisplayName,
        Email = registerDto.Email,
        //usename이 필수 요소이기 떄문에 그냥 유니크한 이메일로 넣어주자 
        UserName = registerDto.Email
      };
      var result = await _userManager.CreateAsync(user, registerDto.Password);

      if (!result.Succeeded)
        return BadRequest(new ApiResponse(400));

      return new UserDto
      {
        Email = user.Email,
        DisplayName = user.DisplayName,
        Token = _tokenService.CreateToke(user)
      };

    }


  }
}