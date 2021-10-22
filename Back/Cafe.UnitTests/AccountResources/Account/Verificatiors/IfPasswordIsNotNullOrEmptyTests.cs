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
	public class IfPasswordIsNotNullOrEmptyTests
	{
		private readonly ChainRequest _chainRequest = null;

		public IfPasswordIsNotNullOrEmptyTests()
		{
			_chainRequest = new ChainRequest();
		}
		[Fact]
		public void IfPasswordIsNotNullOrEmptyTests_PasswordIsNull_ChainRequestStatusFailureExit()
		{
			//Arrange
			string password = null;

			IfPasswordIsNotNullOrEmpty ifPasswordIsNotNullOrEmpty = new(password);
			//Act
			ifPasswordIsNotNullOrEmpty.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Failure_exit);
		}
		[Fact]
		public void IfPasswordIsNotNullOrEmpty_PasswordIsEmpty_ChainRequestStatusFailureExit()
		{
			//Arrange
			string password = string.Empty;

			IfPasswordIsNotNullOrEmpty ifPasswordIsNotNullOrEmpty = new(password);
			//Act
			ifPasswordIsNotNullOrEmpty.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Failure_exit);
		}
		[Fact]
		public void IfPasswordIsNotNullOrEmpty_PasswordIsValid_ChainRequestStatusFailureExit()
		{
			//Arrange
			string password = "password";

			IfPasswordIsNotNullOrEmpty ifPasswordIsNotNullOrEmpty = new(password);
			//Act
			ifPasswordIsNotNullOrEmpty.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Success);
		}
	}
}
