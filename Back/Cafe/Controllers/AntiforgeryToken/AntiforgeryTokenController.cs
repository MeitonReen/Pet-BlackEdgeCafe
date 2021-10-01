using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Cafe.Model.Shared;

namespace Cafe.Controllers.AntiforgeryToken
{
	[ApiController]
	[Route(CafeAPIRoutes.V1.This)]
	[Authorize]
	public partial class AntiforgeryTokenController : ControllerBase
	{
		private readonly AppSettings _appSettings = null;
		private readonly IAntiforgery _antiforgery = null;

		public AntiforgeryTokenController(AppSettings appSettings, IAntiforgery antiforgery)
		{
			_appSettings = appSettings;
			_antiforgery = antiforgery;
		}
	}
}
