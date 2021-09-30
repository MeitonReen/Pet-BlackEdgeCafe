using Cafe.Model.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Controllers.Error
{
	[ApiController]
	[Route(CafeAPIRoutes.V1.Error.This)]
	[ApiExplorerSettings(IgnoreApi = true)]
	public partial class ErrorController : ControllerBase
	{
	}
}
