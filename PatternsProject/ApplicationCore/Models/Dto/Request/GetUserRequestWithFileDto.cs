using ApplicationCore.Models.Dto.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Dto.Request
{
	public  class GetUserRequestWithFileDto
	{
		public ResponseFileDto File { get; set; }
		public ResponseRequestDto Request { get; set; }
	}
}
