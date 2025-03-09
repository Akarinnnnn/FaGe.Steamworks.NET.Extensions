using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Ugc;
public class UgcQuery
{
	private UGCQueryHandle_t handle;
	private CancellationTokenSource cancelQuery;
	private bool isGameServer;
	private bool handleTransferred;

	public CancellationTokenSource CancelQuery { get => cancelQuery; }

	public CancellationToken QueryCancelledToken => cancelQuery.Token;

	public UgcQuery(UGCQueryHandle_t handle, bool isGameServer)
	{
		this.handle = handle;
		this.isGameServer = isGameServer;

		cancelQuery = new();
		cancelQuery.Token.Register(CancelBeforeReceived);
	}

	private void CancelBeforeReceived()
	{
		if (handleTransferred)
		{
			// query completed, handle is transferred to UgcQueryResult
			return;
		}

		if (isGameServer)
		{
			SteamGameServerUGC.ReleaseQueryUGCRequest(handle);
		}
		else
		{
			SteamUGC.ReleaseQueryUGCRequest(handle);
		}
	}

	private async Task<UgcQueryResult> SendQueryRequest()
	{
		var task = SteamUGC.SendQueryUGCRequest(handle)
			.ToValueTaskWithCancellation<SteamUGCQueryCompleted_t>(out CancellationTokenSource cancelTask);

		cancelQuery.Token.Register(cancelTask.Cancel);

		var completedResult = await task;
		handleTransferred = true;

		return new(completedResult, false);
	}

	private async Task<UgcQueryResult> SendQueryRequestGameServer()
	{
		var task = SteamGameServerUGC.SendQueryUGCRequest(handle)
			.ToValueTaskWithCancellation<SteamUGCQueryCompleted_t>(out CancellationTokenSource cancelTask);

		cancelQuery.Token.Register(cancelTask.Cancel);

		var completedResult = await task;
		handleTransferred = true;

		return new(completedResult, true);
	}

	public Task<UgcQueryResult> Send()
	{
		if (isGameServer)
		{
			return SendQueryRequestGameServer();
		}
		else
		{
			return SendQueryRequest();
		}
	}
}
