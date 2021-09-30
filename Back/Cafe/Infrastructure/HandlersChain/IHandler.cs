using System.Threading.Tasks;

namespace Cafe.Infrastructure.HandlersChain
{
	public interface IHandler
	{
		Task HandleAsync(ChainRequest chainRequest);
	}
}