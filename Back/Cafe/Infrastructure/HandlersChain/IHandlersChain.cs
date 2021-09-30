using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.HandlersChain
{
	public interface IHandlersChain
	{
		IHandlersChain AddToContext(string name, object value);
		IHandlersChain AddChainLink(Func<IHandler> handlerCreator);
		Task<IActionResult> RunChainAsync();
		Task<IActionResult> RunChainAsync(Dictionary<string, object> context);
		Task<IActionResult> RunChainAsync(params KeyValuePair<string, object>[] context);
	}
}