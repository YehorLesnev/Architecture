using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.CQRS.Queries.Request
{
	internal class GetRequestByIdQuery : IRequest<RequestModel>
	{
		public Guid Id { get; set; }
	}
}
