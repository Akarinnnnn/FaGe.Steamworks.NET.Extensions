using FaGe.Steamworks.NET.Extensions.Ugc;
using FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;
using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Tests.AutomaticDispatchApiTests.Ugc.QueryBuilder;

public class DetailsBuilderTests
{
	private PublishedFileId_t modValid;

	[SetUp]
	public void Setup()
	{
		modValid = new(280762427);
	}

	[Test]
	public void TestDetailsQueryClient()
	{
		DetailsUgcQueryBuilder builder = UgcQueryBuilders.Details([modValid], false);
		using (var block = Assert.EnterMultipleScope())
		{
			TestAllBaseMethods(builder);
		}
		TestAllSpecificMethods(builder);
		Assert.That(builder.AbandonQuery, Throws.Nothing);
	}

	[Test]
	public void TestDetailsQueryGameServer()
	{
		DetailsUgcQueryBuilder builder = UgcQueryBuilders.Details([modValid], true);
		using (var block = Assert.EnterMultipleScope())
		{
			TestAllBaseMethods(builder);
		}
		TestAllSpecificMethods(builder);
		Assert.That(builder.AbandonQuery, Throws.Nothing);
	}

	private static void TestAllBaseMethods(DetailsUgcQueryBuilder builder)
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

	private static void TestAllSpecificMethods(DetailsUgcQueryBuilder builder)
	{
		
	}
}
