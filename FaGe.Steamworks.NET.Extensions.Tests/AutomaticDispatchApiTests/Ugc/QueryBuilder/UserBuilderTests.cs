using FaGe.Steamworks.NET.Extensions.Ugc;
using FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;
using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Tests.AutomaticDispatchApiTests.Ugc.QueryBuilder;

public class UserBuilderTests
{
	private AccountID_t targetUser;
	private AppId_t appIdSpaceWar = new(480);

	[SetUp]
	public void Setup()
	{
		// FaGe's 32bit account id
		targetUser = new AccountID_t(365958699);
	}

	[Test]
	public void TestUserQueryClient()
	{
		Assert.Multiple(() =>
		{
			UserUgcQueryBuilder builder = UgcQueryBuilders.User(
				targetUser,
				EUserUGCList.k_EUserUGCList_Published,
				EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
				EUserUGCListSortOrder.k_EUserUGCListSortOrder_TitleAsc,
				appIdSpaceWar,
				appIdSpaceWar,
				1,
				false
			);
			TestAllBaseMethods(builder);
			TestAllSpecificMethods(builder);
			Assert.That(builder.AbandonQuery, Throws.Nothing);
		});
	}

	[Test]
	public void TestUserQueryGameServer()
	{
		Assert.Multiple(() =>
		{
			UserUgcQueryBuilder builder = UgcQueryBuilders.User(
				targetUser,
				EUserUGCList.k_EUserUGCList_Published,
				EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
				EUserUGCListSortOrder.k_EUserUGCListSortOrder_TitleAsc,
				appIdSpaceWar,
				appIdSpaceWar,
				1,
				true
			);
			TestAllBaseMethods(builder);
			TestAllSpecificMethods(builder);
			Assert.That(builder.AbandonQuery, Throws.Nothing);
		});
	}

	private static void TestAllBaseMethods(UserUgcQueryBuilder builder)
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

	private static void TestAllSpecificMethods(UserUgcQueryBuilder builder)
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
