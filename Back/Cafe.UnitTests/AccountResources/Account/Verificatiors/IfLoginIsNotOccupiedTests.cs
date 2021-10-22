using Cafe.Databases.Identity.Contexts.Implementations;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.AccountResources.Account.Verificators;
using Cafe.UnitTests.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
	public class IfLoginIsNotOccupiedTests : IDisposable
	{
		private readonly IdentityMssqlContext _identityContext = null;
		private readonly UserManager<User> _userManager = null;
		private readonly User _storedUser = null;
		ChainRequest _chainRequest = null;

		public IfLoginIsNotOccupiedTests()
		{
			DbContextOptions<IdentityMssqlContext> options =
				new DbContextOptionsBuilder<IdentityMssqlContext>()
					.UseInMemoryDatabase(new Guid().ToString(), new InMemoryDatabaseRoot())
					.Options;

			_storedUser = new() { UserName = "storedUser" };

			_identityContext = new(options, new PasswordHasher<User>(), TestService.GetAppSettings());

			UserStore<User, IdentityRole<Guid>,
				IdentityMssqlContext, Guid> userStore = new(_identityContext);

			_userManager = new(userStore, null, null, null, null,
				new UpperInvariantLookupNormalizer(), null, null, null);
			_userManager.CreateAsync(_storedUser);

			_chainRequest = new ChainRequest();
		}
		public void Dispose()
		{
			_identityContext.Database.EnsureDeleted();
			_identityContext.Dispose();
			_userManager.Dispose();
		}
		[Fact]
		public void IfLoginIsNotOccupied_LoginIsOccupied_ChainRequestStatusFailureExit()
		{
			//Arrange
			User inputUser = new() { UserName = _storedUser.UserName };

			IfLoginIsNotOccupied ifLoginIsNotOccupied = new(inputUser.UserName,
				_userManager);

			//Act
			ifLoginIsNotOccupied.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Failure_exit);
		}
		[Fact]
		public void IfLoginIsNotOccupied_LoginIsNotOccupied_ChainRequestStatusSuccess()
		{
			//Arrange
			User inputUser = new() { UserName = "inputUser" };

			IfLoginIsNotOccupied ifLoginIsNotOccupied = new(inputUser.UserName,
				_userManager);

			//Act
			ifLoginIsNotOccupied.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Success);
		}
		
	}
}
