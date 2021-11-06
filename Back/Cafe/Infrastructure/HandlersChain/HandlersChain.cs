using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.HandlersChain
{
	public class HandlersChain : IHandlersChain
	{
		private readonly ChainProcessingStatus[] _exitFromChain = new[]
		{
			ChainProcessingStatus.Failure_exit,
			ChainProcessingStatus.Success_exit
		};
		private ChainRequest _chainRequest = new(null);
		//private List<Type> _handlers = new();
		private readonly List<Func<IHandler>> _handlerCreators = new();
		public HandlersChain()
		{ }
		public HandlersChain(params KeyValuePair<string, object>[] context)
		{
			_chainRequest = new ChainRequest(new Dictionary<string, object>(context));
		}
		public HandlersChain(Dictionary<string, object> context)
		{
			_chainRequest = new ChainRequest(context);
		}
		public IHandlersChain AddChainLink(Func<IHandler> handlerCreator)
		{
			_handlerCreators.Add(handlerCreator);
			return this;
		}
		/*public IHandlersChain AddChainLink<T>() where T : IHandler, new()
		{
			_handlers.Add(typeof(T));
			return this;
		}*/
		public IHandlersChain AddToContext(string name, object value)
		{
			_chainRequest ??= new ChainRequest(new Dictionary<string, object>());

			_chainRequest.Context.Add(name, value);
			return this;
		}
		public async Task<IActionResult> RunChainAsync()
		{
			if (_chainRequest == null)
			{
				throw new InvalidOperationException(
					"ChainRequest in handlers chain is not found");
			}
			return await RunChainBaseAsync();
		}
		public async Task<IActionResult> RunChainAsync(Dictionary<string, object> context)
		{
			_chainRequest = new ChainRequest(context);
			return await RunChainBaseAsync();
		}
		public async Task<IActionResult> RunChainAsync(params
			KeyValuePair<string, object>[] context)
		{
			_chainRequest = new ChainRequest(new Dictionary<string, object>(context));
			return await RunChainBaseAsync();
		}
		/*public async Task<IActionResult> RunChainBaseAsync()
		{
			for (int i = 0;
				(i != _handlers.Count) && !_exitFromChain.Contains(_chainRequest.Status);
				i++)
			{
				await (Activator.CreateInstance(_handlers[i]) as IHandler)
					.HandleAsync(_chainRequest);
			}
			;
			return _chainRequest.Result;
		}*/
		public async Task<IActionResult> RunChainBaseAsync()
		{
			int i = 0;
			do
			{
				await (_handlerCreators[i].Invoke()).HandleAsync(_chainRequest);
				i++;
			}
			while ((i != _handlerCreators.Count) && !_exitFromChain.Contains(_chainRequest.Status));
			
			return _chainRequest.Result;
		}
	}
}