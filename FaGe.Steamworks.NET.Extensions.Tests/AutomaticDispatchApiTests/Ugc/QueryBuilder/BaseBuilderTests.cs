using FaGe.Steamworks.NET.Extensions.Ugc;
using FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;
using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Tests.AutomaticDispatchApiTests.Ugc.QueryBuilder
{
	public class BaseBuilderTests
	{
		private AccountID_t targetUser;
		private AppId_t appIdSpaceWar = new AppId_t(480);

		[SetUp]
		public void Setup()
		{
			targetUser = SteamUser.GetSteamID().GetAccountID();
		}

		[Test]
		public void TestUserQueryBaseClient()
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
			Assert.That(builder.AbandonQuery, Throws.Nothing);
		}

		[Test]
		public void TestUserQueryBaseGameServer()
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
			Assert.That(builder.AbandonQuery, Throws.Nothing);
		}

		[Test]
		public void TestAllCursorQueryBaseGameServer()
		{
			TestAllBaseMethods(UgcQueryBuilders.All(
				EUGCQuery.k_EUGCQuery_RankedByVote,
				EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
				appIdSpaceWar,
				appIdSpaceWar,
				true
			));
		}
	
		[Test]
		public void TestAllPageQueryBaseGameServer()
		{
			TestAllBaseMethods(UgcQueryBuilders.All(
				EUGCQuery.k_EUGCQuery_RankedByVote,
				EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
				appIdSpaceWar,
				appIdSpaceWar,
				true,
				1
			));
		}

		[Test]
		public void TestAllCursorQueryBaseClient()
		{
			TestAllBaseMethods(UgcQueryBuilders.All(
				EUGCQuery.k_EUGCQuery_RankedByVote,
				EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
				appIdSpaceWar,
				appIdSpaceWar,
				false
			));
		}
	
		[Test]
		public void TestAllPageQueryBaseClient()
		{
			TestAllBaseMethods(UgcQueryBuilders.All(
				EUGCQuery.k_EUGCQuery_RankedByVote,
				EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,
				appIdSpaceWar,
				appIdSpaceWar,
				false,
				1
			));
		}

		[Test]
		public void TestDetailsQueryBaseClient()
		{
			TestAllBaseMethods(UgcQueryBuilders.Details(
				[ new (1) ],
				false
			));
		}
		
		[Test]
		public void TestDetailsQueryBaseGameServer()
		{
			TestAllBaseMethods(UgcQueryBuilders.Details(
				[ new (1) ],
				true
			));
		}

		private static void TestAllBaseMethods(BaseUgcQueryBuilder builder)
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
				Assert.That(builder.AbandonQuery, Throws.Nothing);
			});
		}
	}
}
