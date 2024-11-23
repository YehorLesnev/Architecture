namespace ApplicationCore.Models.Dto.User;

public class ResponseUserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public bool IsManager { get; set; }
    public string ProfilePicture { get; set; }
}
