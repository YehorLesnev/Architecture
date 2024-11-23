using ApplicationCore.Identity;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static ApplicationCore.Constants.Constants;

namespace HRMS.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(Roles = UserRoleNames.Admin)]
public class RolesController(
RoleManager<IdentityRole<Guid>> roleManager,
UserManager<UserModel> userManager) : ControllerBase
{
	[HttpGet]
	public ActionResult<IEnumerable<IdentityRole<Guid>>> GetAllRoles()
	{
		return Ok(roleManager.Roles);
	}

	[HttpGet($"user/{{{nameof(userEmail)}}}")]
	[AllowAnonymous]
	public async Task<ActionResult<IEnumerable<string>>> GetUserRole([FromRoute] string userEmail)
	{
		var user = await userManager.FindByEmailAsync(userEmail);

		if (user is null) return NotFound();

		return Ok(await userManager.GetRolesAsync(user));
	}

	[HttpPost]
	public async Task<ActionResult> CreateRole([FromBody] string roleName)
	{
		await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));

		return Ok();
	}

	[HttpPost("assign-role/{userEmail}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult> AssignRoleToUser([FromRoute] string userEmail, [FromBody] string roleName)
	{
		var user = await userManager.FindByEmailAsync(userEmail);

		if (user is null) return NotFound("No users with such email found");

		try
		{
			await userManager.AddToRoleAsync(user, roleName);
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(ex.Message);
		}

		return Ok();
	}
}
