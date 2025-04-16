using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Commands.File;

public class CreateFileCommand : IRequest
{
    public byte[] FileContent { get; set; }

    public Guid RequestId { get; set; }
}
