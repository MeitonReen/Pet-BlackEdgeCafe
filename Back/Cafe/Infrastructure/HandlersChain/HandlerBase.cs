using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.HandlersChain
{
	public abstract class HandlerBase : IHandler
	{
		protected readonly ControllerBaseResultGenerator _resultGenerator = new();
		protected virtual void GetParamsFromChainRequest(ChainRequest request)
		{ }
		protected virtual void SetParamsToChainRequest(ChainRequest request)
		{ }
		protected toT GetParamFromChainRequest<toT>(string className,
			ChainRequest request, string paramName)
		{
			if (request.Context.GetValueOrDefault(paramName) is not toT _paramName)
			{
				throw new ArgumentException($"In {nameof(className)} = " +
					$"\"{className}\" {nameof(paramName)} = \"{paramName}\"" +
					$" is null or not found");
			}
			return _paramName;
		}
		protected abstract Task ExecuteAsync(ChainRequest chainRequest);
		public async Task HandleAsync(ChainRequest chainRequest)
		{
			GetParamsFromChainRequest(chainRequest);

			await ExecuteAsync(chainRequest);

			SetParamsToChainRequest(chainRequest);
			return;
		}
	}
}