using Microsoft.AspNetCore.Authorization;

namespace Cafe.Model.Shared.AuthorizationPolicies.Default.CustomRequirements
{
	public class UserIsExists : IAuthorizationRequirement
	{
	}
}