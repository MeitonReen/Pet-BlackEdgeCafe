namespace Cafe.Infrastructure.ApplicationSettings
{
	public class Constants
	{
		public CorsPolicies CorsPolicies { get; init; }
		public string UserId { get; init; }
		public string UserName { get; init; }
		public string AuthCookieName { get; init; }
		public string EFCoreMiniProfilerBasePath { get; init; }
		public string GetOpenAPIUrl { get; init; }
		public string ServerUrlOpenAPI { get; init; }
		public string AntiforgeryTokenResponseHeaderName { get; init; }
		public string AntiforgeryTokenRequestHeaderName { get; init; }
		public string AntiforgeryTokenCookieName { get; init; }
		public string[] ApiVersions { get; init; }
		public EnvironmentVariableNames EnvironmentVariableNames{ get; init; }
		public LoggingParams LoggingParams { get; init; }
		public string DefaultPage { get; init; }
		public string RootSpaStaticFilesPath { get; init; }
	}
}