﻿using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.DIServices
{
	public class OpenAPIGenerator
	{
		private readonly AppSettings _appSettings = null;
		public OpenAPIGenerator(AppSettings appSettings)
		{
			_appSettings = appSettings;
		}
		public async Task GenerateDefaultAsync()
		{
			await GenerateFromUriAsync(_appSettings.Constants.GetOpenAPIUrl);
			return;
		}
		public async Task GenerateFromUriAsync(string uri)
		{
			await PowerShell.Create()
				.AddScript($"invoke-webrequest -uri '{uri}/swagger/v1/swagger.json' -outfile '{Environment.CurrentDirectory}/swagger.json'")
				.InvokeAsync();
			return;
		}
	}
}
