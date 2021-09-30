using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Controllers.Error
{
	public partial class ErrorController : ControllerBase
	{
		public IActionResult Error() => StatusCode(StatusCodes.Status500InternalServerError);
	}
}
