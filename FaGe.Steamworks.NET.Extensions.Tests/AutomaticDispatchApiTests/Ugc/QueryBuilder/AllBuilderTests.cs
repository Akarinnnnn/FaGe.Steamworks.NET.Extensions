using FaGe.Steamworks.NET.Extensions.Ugc;
using FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;
using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Tests.AutomaticDispatchApiTests.Ugc.QueryBuilder;

public class AllBuilderTests
{
	private AppId_t appIdSpaceWar = new(480);

	[Test]
	public void TestAllCursorQueryBaseGameServer()
	{
		var builder = UgcQueryBuilders.All(
			EUGCQuery.k_EUGCQuery_RankedByVote,
			EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
			appIdSpaceWar,
			appIdSpaceWar,
			false,
			"cursor"
		);
		TestAllBaseMethods(builder);
		TestAllSpecificMethods(builder);
		Assert.That(builder.AbandonQuery, Throws.Nothing);
	}

	[Test]
	public void TestAllPageQueryBaseGameServer()
	{
		var builder = UgcQueryBuilders.All(
			EUGCQuery.k_EUGCQuery_RankedByVote,
			EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
			appIdSpaceWar,
			appIdSpaceWar,
			true,
			1
		);
		TestAllBaseMethods(builder);
		TestAllSpecificMethods(builder);
		Assert.That(builder.AbandonQuery, Throws.Nothing);
	}

	private static void TestAllBaseMethods(AllUgcQueryBuilder builder)
	{
		Assert.Multiple(() =>
		{
			Assert.That(() => builder.AllowCachedResponse(120), Throws.Nothing);
			Assert.That(() => builder.Language("schinese"), Throws.Nothing);
			Assert.That(() => builder.RequireKeyValueTag("k", "v"), Throws.Nothing);
			Assert.That(() => builder.ReturnAdditionalPreviews(true), Throws.Nothing);
			Assert.That(() => builder.ReturnAdditionalPreviews(true), Throws.Nothing);
			Assert.That(() => builder.ReturnChildren(true), Throws.Nothing);
			Assert.That(() => builder.ReturnKeyValueTags(true), Throws.Nothing);
			Assert.That(() => builder.ReturnLongDescription(true), Throws.Nothing);
			Assert.That(() => builder.ReturnMetadata(true), Throws.Nothing);
			Assert.That(() => builder.ReturnPlaytimeStats(120), Throws.Nothing);
		});
	}

	private static void TestAllSpecificMethods(AllUgcQueryBuilder builder)
	{
		Assert.Multiple(() =>
		{
			Assert.DoesNotThrow(() => builder.ExcludeTag("tag"));
			Assert.DoesNotThrow(() => builder.RequireTag("tag"));
			Assert.DoesNotThrow(() => builder.RequireTagGroup(["tag"]));
			Assert.DoesNotThrow(() => builder.ReturnOnlyIDs(true));
			Assert.DoesNotThrow(() => builder.ReturnTotalOnly(true));
		});
	}
}
