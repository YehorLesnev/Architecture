using ApplicationCore.Models;

namespace HRMS.Controllers
{
	internal class RequestWithFileDto
	{
		public RequestModel Request { get; set; }
		public FileModel File { get; set; }
	}
}