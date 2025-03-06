#if NET8_0_OR_GREATER
using Steamworks;
using System.Diagnostics;

namespace FaGe.Steamworks.NET.Extensions.Tests.GameServerApiTests;

public class ServerValueTaskDispatchTest
{
	private ulong modFAGE_id = 1579583001;

	[Test]
	public void TestAwait()
	{
		var queryHandle = SteamGameServerUGC.CreateQueryUGCDetailsRequest([new(modFAGE_id)], 1);
		Assert.ThatAsync(async () =>
		{
			var queryResult = await SteamGameServerUGC.SendQueryUGCRequest(queryHandle).ToValueTask<SteamUGCQueryCompleted_t>();


			Assert.Pass();

		}, Throws.Nothing.Or.InstanceOf<SteamAPICallException>());
		// completionHolder[i].Set(queryApiCallHandle);
	}

	[Test]
	public void TestCancel()
	{
		var queryHandle = SteamGameServerUGC.CreateQueryUGCDetailsRequest([new(modFAGE_id)], 1);
		Assert.ThatAsync(async () =>
		{
			var queryResultTask = SteamGameServerUGC.SendQueryUGCRequest(queryHandle)
					.ToValueTaskWithCancellation<SteamUGCQueryCompleted_t>(out var cts);
			cts.Cancel();
			await queryResultTask;

			Assert.Fail("Not cancelled successfully");

		}, Throws.InstanceOf<OperationCanceledException>());
	}
}
#endif