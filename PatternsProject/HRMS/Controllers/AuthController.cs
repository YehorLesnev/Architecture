using ApplicationCore.Identity.JwtConfig;
using ApplicationCore.Models;
using ApplicationCore.Models.Dto.Auth;
using ApplicationCore.Models.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.CQRS.Commands.Balance;

namespace HRMS.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(
IMediator mediator,
IMapper mapper,
IUserService userService,
UserManager<UserModel> userManager,
IJwtTokenConfig jwtConfig) : BaseController(mediator)
{
	[HttpPost("Register")]
	[AllowAnonymous]
	public async Task<ActionResult<ResponseUserDto>> RegisterAsync(
		[FromForm] RequestUserDto registerDto)
	{
		if (!ModelState.IsValid) return BadRequest("Wrong input");

		if (await userManager.FindByEmailAsync(registerDto.Email) is not null)
			return BadRequest("UserModel with specified email is already exists");

		var UserModel = mapper.Map<UserModel>(registerDto);

		try
		{
			await userService.CreateAsync(UserModel, registerDto.Password);
		}
		catch (InvalidOperationException)
		{
			return BadRequest("The password must contain capital letters, numbers and special symbol");
		}

		var createdUser = await userService.GetAsync(r => r.Email == UserModel.Email);

		if (createdUser is null)
			return BadRequest("Couldn't create UserModel");

		await Mediator.SendAsync(new CreateUserBalanceCommand{ UserId = createdUser.Id });

		return Created(nameof(RegisterAsync), mapper.Map<ResponseUserDto>(createdUser));
	}

	[HttpPost("Login")]
	[AllowAnonymous]
	public async Task<ActionResult<ResponseLoginDto>> LoginAsync(
		[FromBody] RequestLoginDto loginDto)
	{
		if (!ModelState.IsValid) return BadRequest("Wrong input");

		var UserModel = await userManager.FindByEmailAsync(loginDto.Email);

		if (UserModel is null)
			return NotFound("No users with specified email found");

		if (await userManager.CheckPasswordAsync(UserModel, loginDto.Password) == false)
			return BadRequest("Wrong password or email");

		var token = await GetJwtSecurityTokenAsync(UserModel);
		var tokenExpirationDate = DateTime.Now.AddDays(jwtConfig.TokenLifetimeInDays);

		CookieOptions cookieOptions = new()
		{
			HttpOnly = true,
			SameSite = SameSiteMode.Strict,
			Secure = true,
			Expires = tokenExpirationDate
		};

		Response.Cookies.Append(
			"AuthorizationToken",
			new JwtSecurityTokenHandler().WriteToken(token),
			cookieOptions
			);

		return Ok(new ResponseLoginDto
		{
			UserId = UserModel.Id,
			Roles = await userManager.GetRolesAsync(UserModel),
			Token = new JwtSecurityTokenHandler().WriteToken(token),
			Expires = tokenExpirationDate
		});
	}

	private async Task<JwtSecurityToken?> GetJwtSecurityTokenAsync(UserModel UserModel)
	{
		var userRoles = await userManager.GetRolesAsync(UserModel);

		var authClaims = new List<Claim>
		{
			new(ClaimTypes.Name, UserModel.UserName ?? string.Empty),
			new(ClaimTypes.Email, UserModel.Email ?? string.Empty),
			new("IsManager", UserModel.IsManager.ToString()),
			new(ClaimTypes.NameIdentifier, UserModel.Id.ToString()),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		foreach (var role in userRoles)
			authClaims.Add(new(ClaimTypes.Role, role));

		var tokenExpirationDate = DateTime.UtcNow.AddDays(jwtConfig.TokenLifetimeInDays);

		return new JwtSecurityToken(
			issuer: jwtConfig.JwtIssuer,
			audience: jwtConfig.JwtAudience,
			claims: authClaims,
			notBefore: DateTime.Now,
			expires: tokenExpirationDate,
			signingCredentials: new SigningCredentials(
				new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(jwtConfig.JwtKey)),
				SecurityAlgorithms.HmacSha256)
			);
	}
}
