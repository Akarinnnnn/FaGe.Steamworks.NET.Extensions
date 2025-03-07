#if NET8_0_OR_GREATER
using Steamworks;
using System.Diagnostics;

namespace FaGe.Steamworks.NET.Extensions.Tests.ManualDispatchApiTests;

[TestFixture]
public class ValueTaskDispatcherTests
{
	private ulong modFAGE_id = 1579583001;

	[Test]
	public void TestAwait()
	{
		var queryHandle = SteamUGC.CreateQueryUGCDetailsRequest([new(modFAGE_id)], 1);
		Assert.ThatAsync(async () =>
		{
			var queryResult = await SteamUGC.SendQueryUGCRequest(queryHandle).ToValueTask<SteamUGCQueryCompleted_t>();


			Assert.That(queryResult.m_eResult, Is.EqualTo(EResult.k_EResultOK));

		}, Throws.Nothing.Or.InstanceOf<SteamAPICallException>());
		// completionHolder[i].Set(queryApiCallHandle);
	}

	[Test]
	public void TestCancel()
	{
		var queryHandle = SteamUGC.CreateQueryUGCDetailsRequest([new(modFAGE_id)], 1);
		Assert.ThatAsync(async () =>
		{
			var queryResultTask = SteamUGC.SendQueryUGCRequest(queryHandle)
					.ToValueTaskWithCancellation<SteamUGCQueryCompleted_t>(out var cts);
			cts.Cancel();
			await queryResultTask;

			Assert.Fail("Not cancelled successfully");

		}, Throws.InstanceOf<OperationCanceledException>());
	}
}
#endif