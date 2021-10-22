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
	public class IfPasswordAndConfPasswordMatchedTests
	{
		private readonly ChainRequest _chainRequest = null;
		public IfPasswordAndConfPasswordMatchedTests()
		{
			_chainRequest = new ChainRequest();
		}
		[Fact]
		public void IfPasswordAndConfPasswordMatched_PasswordsMatched_ChainRequestStatusSuccess()
		{
			//Arrange
			string password = "password";
			string confPassword = "password";

			IfPasswordAndConfPasswordMatched ifPasswordAndConfPasswordMatched =
				new(password, confPassword);
			//Act
			ifPasswordAndConfPasswordMatched.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Success);
		}
		[Fact]
		public void IfPasswordAndConfPasswordMatched_PasswordsNotMatched_ChainRequestStatusFailureExit()
		{
			//Arrange
			string password = "password";
			string confPassword = "password2";

			IfPasswordAndConfPasswordMatched ifPasswordAndConfPasswordMatched =
				new(password, confPassword);
			//Act
			ifPasswordAndConfPasswordMatched.HandleAsync(_chainRequest).Wait();

			//Assert
			_chainRequest.Status.Should().Be(ChainProcessingStatus.Failure_exit);
		}
	}
}
