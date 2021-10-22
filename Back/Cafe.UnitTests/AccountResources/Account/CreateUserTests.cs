using Cafe.Databases.Identity.Contexts.Implementations;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.AccountResources.Account;
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
	public class CreateUserTests : IDisposable
	{
		private readonly IdentityMssqlContext _identityContext = null;
		private readonly UserManager<User> _userManager = null;
		ChainRequest _chainRequest = null;

		public CreateUserTests()
		{
			DbContextOptions<IdentityMssqlContext> options =
				new DbContextOptionsBuilder<IdentityMssqlContext>()
					.UseInMemoryDatabase(new Guid().ToString(), new InMemoryDatabaseRoot())
					.Options;

			_identityContext = new(options, new PasswordHasher<User>(), TestService.GetAppSettings());

			UserStore<User, IdentityRole<Guid>,
				IdentityMssqlContext, Guid> userStore = new(_identityContext);

			_userManager = new(userStore, null, new PasswordHasher<User>(), null, null,
				new UpperInvariantLookupNormalizer(), null, null, null);

			_chainRequest = new ChainRequest();
		}
		public void Dispose()
		{
			_identityContext.Database.EnsureDeleted();
			_identityContext.Dispose();
			_userManager.Dispose();
		}
		[Fact]
		public void CreateUser_ValidLoginWithPassword_NotExceptionsAndChainRequestStatusSuccessAndFindInputUser()
		{
			//Arrange
			string inputLogin = "login";
			string inputPassword = "password";
			CreateUser createUser = new(inputLogin, inputPassword, _userManager);

			//Act
			Func<Task> act = () => createUser.HandleAsync(_chainRequest);

			//Assert
			act.Should().NotThrowAsync().Wait();

			_chainRequest.Status.Should().Be(ChainProcessingStatus.Success);

			Task<User> findUser = _userManager.FindByNameAsync(inputLogin);
			findUser.Wait();

			findUser
				.Result.Should().NotBeNull()
					.And.Subject.As<User>()
				.UserName.Should().Be(inputLogin);
		}
		[Fact]
		public void CreateUser_LoginAndInvalidPassword_ExceptionAndChainRequestStatusFailureExitAndDontFindInputUser()
		{
			//Arrange
			string inputLogin = "login";
			string inputPassword = null;
			CreateUser createUser = new(inputLogin, inputPassword, _userManager);

			//Act
			Func<Task> act = () => createUser.HandleAsync(_chainRequest);

			//Assert
			act.Should().ThrowAsync<Exception>().Wait();

			_chainRequest.Status.Should().Be(ChainProcessingStatus.Failure_exit);

			Task<User> findUser = _userManager.FindByNameAsync(inputLogin);
			findUser.Wait();
			
			findUser.Result.Should().BeNull();
		}
	}
}
