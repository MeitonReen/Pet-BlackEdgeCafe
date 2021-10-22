using Cafe.Databases.Identity.Contexts.Implementations;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.AccountResources.Account.Verificators;
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
	public class IfPasswordIsCorrectTests
	{
		private readonly PasswordHasher<User> _passwordHasher = null;
		private readonly ChainRequest _chainRequest = null;
		private User user = null;
		private string _password = null;

		public IfPasswordIsCorrectTests()
		{
			_passwordHasher = new();
			_chainRequest = new ChainRequest();
			_passwordHasher = new PasswordHasher<User>();

			user = new();
			_chainRequest.Context.Add(nameof(user), user);
			_password = "password";
		}

		[Fact]
		public void IfPasswordIsCorrect_PasswordIsCorrect_ChainRequestStatusSuccess()
		{
			//Arrange
			user.PasswordHash = _passwordHasher.HashPassword(null, _password);

			IfPasswordIsCorrect ifPasswordIsCorrect = new(_password, _passwordHasher);
			//Act
			ifPasswordIsCorrect.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Success);
		}
		[Fact]
		public void IfPasswordIsCorrect_PasswordIsNotCorrect_ChainRequestStatusFailureExit()
		{
			//Arrange
			user.PasswordHash = _password;

			IfPasswordIsCorrect ifPasswordIsCorrect = new(_password, _passwordHasher);
			//Act
			ifPasswordIsCorrect.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Failure_exit);
		}
	}
}
