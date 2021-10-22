using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cafe.Infrastructure.HandlersChain
{
	public class ChainRequest
	{
		public ChainProcessingStatus Status { get; set; } = ChainProcessingStatus.Initial;
		public IActionResult Result { get; set; } = null;
		public Dictionary<string, object> Context { get; set; } = null;

		public ChainRequest(Dictionary<string, object> context)
		{
			if (context == null)
			{
				Context = new Dictionary<string, object>();
				return;
			}

			Context = context;
			return;
		}
		public ChainRequest()
		{
			Context = new Dictionary<string, object>();
			return;
		}
	}
}