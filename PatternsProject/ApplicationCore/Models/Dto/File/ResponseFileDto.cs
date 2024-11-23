using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Dto.File;

public class ResponseFileDto
{
	public Guid FileId { get; set; }
	public byte[] FileContent { get; set; }
	public Guid RequestId { get; set; }
}
