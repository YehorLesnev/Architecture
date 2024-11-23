using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Identity;

public class RequestUserDto
{
    public required string Email { get; set; }

    public required string UserName { get; set; }

    public required string FullName { get; set; }

    public required string Position { get; set; }

    public required bool IsManager { get; set; }

	public required IFormFile ProfilePicture { get; set; }

	public required string Password { get; set; }
}
