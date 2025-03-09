using FaGe.Steamworks.NET.Extensions.Ugc;
using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Tests.AutomaticDispatchApiTests.Ugc.Result;

public class ResultTests
{
	private AppId_t appIdSpaceWar = new(480);


	[Test]
	public async Task TestSendQuery()
	{
		var builder = UgcQueryBuilders.All(
			EUGCQuery.k_EUGCQuery_RankedByTextSearch,
			EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
			appIdSpaceWar,
			appIdSpaceWar,
			false,
			1
		);
		builder.ReturnMetadata(true)
			.SearchText("a");

		UgcQuery query = builder.Build();
		query.CancelQuery.CancelAfter(14000);
		query.CancelQuery.Token.Register(Assert.Inconclusive);
		
		using var result = await query.Send();

		Assert.That(result, Has.Count.GreaterThanOrEqualTo(1));
	}

	[Test]
	public void TestCancelQueryBeforeSend()
	{
		var builder = UgcQueryBuilders.All(
			EUGCQuery.k_EUGCQuery_RankedByTextSearch,
			EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
			appIdSpaceWar,
			appIdSpaceWar,
			false,
			1
		);
		builder.ReturnMetadata(true)
			.SearchText("a");

		Assert.ThatAsync(async () =>
		{
			UgcQuery query = builder.Build();
			query.CancelQuery.Cancel();
			await query.Send();
		}, Throws.TypeOf<OperationCanceledException>());
	}

	[Test]
	public void TestCancelQueryAfterSend()
	{
		var builder = UgcQueryBuilders.All(
			EUGCQuery.k_EUGCQuery_RankedByTextSearch,
			EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
			appIdSpaceWar,
			appIdSpaceWar,
			false,
			1
		);
		builder.ReturnMetadata(true)
			.SearchText("a");

		UgcQuery query = builder.Build();

		Assert.ThatAsync(async () =>
		{
			var task = query.Send();
			query.CancelQuery.Cancel();

			await task;
		}, Throws.TypeOf<OperationCanceledException>());
	}
}
