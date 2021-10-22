using Cafe.Databases.Identity.Contexts.Implementations;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.AccountResources.Account.Verificators;
using Cafe.UnitTests.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cafe.UnitTests
{
	public class IfLoginIsExistsTests : IDisposable
	{
		private readonly IdentityMssqlContext _identityContext = null;
		private readonly User _rightUser = null;
		private readonly ChainRequest _chainRequest = null;
		public IfLoginIsExistsTests()
		{
			DbContextOptions<IdentityMssqlContext> options =
				new DbContextOptionsBuilder<IdentityMssqlContext>()
					.UseInMemoryDatabase(new Guid().ToString(), new InMemoryDatabaseRoot())
					.Options;

			_identityContext = new(options, new PasswordHasher<User>(),
				TestService.GetAppSettings());

			_rightUser = new() { UserName = "rightUser" };

			_chainRequest = new ChainRequest();
		}
		public void Dispose()
		{
			_identityContext.Database.EnsureDeleted();
			_identityContext.Dispose();
		}

		[Fact]
		public void IfLoginIsExists_LoginIsExist_ChainRequestStatusSuccess()
		{
			//Arrange
			_identityContext.Users.Add(_rightUser);
			_identityContext.SaveChanges();

			IfLoginIsExists ifLoginIsExists = new(_identityContext, _rightUser.UserName);

			//Act
			ifLoginIsExists.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Success);
		}
		[Fact]
		public void IfLoginIsExists_LoginIsNotExist_ChainRequestStatusFailureExit()
		{
			//Arrange
			IfLoginIsExists ifLoginIsExists = new(_identityContext, _rightUser.UserName);

			//Act
			ifLoginIsExists.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Failure_exit);
		}
	}
}
