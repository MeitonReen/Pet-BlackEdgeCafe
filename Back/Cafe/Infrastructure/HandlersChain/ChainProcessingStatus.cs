namespace Cafe.Infrastructure.HandlersChain
{
	public enum ChainProcessingStatus
	{
		Success,
		Success_exit,//exit from chain
		Failure,
		Failure_exit,//exit from chain
		Initial//exit from chain
	}
}