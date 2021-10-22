using Cafe.Databases.Identity.Contexts.Implementations;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.AccountResources.Account;
using Cafe.Model.AccountResources.Account.Verificators;
using Cafe.UnitTests.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cafe.UnitTests
{
	public class LogInTests
	{
		private readonly User user = new();
		private readonly ChainRequest _chainRequest = null;
		private readonly DefaultHttpContext _httpContext = null;
		public LogInTests()
		{
			IServiceProvider services = Host.CreateDefaultBuilder()
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				}).ConfigureAppConfiguration(conf =>
				conf.AddUserSecrets<Program>()).Build().Services;

			_httpContext = new();
			_httpContext.RequestServices = services;

			_chainRequest = new ChainRequest();
		}
		[Fact]
		public void LogIn_ValidUser_NotExceptionAndChainRequestStatusSuccess()
		{
			//Arrange
			user.UserName = "userName";
			user.Id = new Guid();
			_chainRequest.Context.Add(nameof(user), user);

			LogIn logIn = new(TestService.GetAppSettings(), _httpContext);

			//Act
			Func<Task> act = () => logIn.HandleAsync(_chainRequest);

			//Assert
			act.Should().NotThrowAsync().Wait();
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Success);
		}
		[Fact]
		public void LogIn_InvalidUserUser_ExceptionAndChainRequestStatusFailureExit()
		{
			//Arrange
			_chainRequest.Context.Add(nameof(user), null);

			LogIn logIn = new(TestService.GetAppSettings(), _httpContext);

			//Act
			Func<Task> act = () => logIn.HandleAsync(_chainRequest);

			//Assert
			act.Should().ThrowAsync<Exception>().Wait();

			_chainRequest.Status.Should().Be(ChainProcessingStatus.Failure_exit);
		}
	}
}
