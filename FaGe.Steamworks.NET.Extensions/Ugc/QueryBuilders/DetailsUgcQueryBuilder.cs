using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;

public class DetailsUgcQueryBuilder : BaseUgcQueryBuilder
{
	// Shared
	// -------------
	// AllowCachedResponse
	// Language
	// RequiredKeyValueTag
	// ReturnAdditionalPreviews
	// ReturnChildren
	// ReturnKeyValueTags
	// ReturnLongDescription
	// ReturnMetadata
	// ReturnPlaytimeStats
	// -------------
	// Allowed
	// -------------
	// CloudFileNameFilter
	// -------------
	internal DetailsUgcQueryBuilder(UGCQueryHandle_t handle, bool isGameServer) : base(handle, isGameServer)
	{
	}

	public DetailsUgcQueryBuilder FilterCloudFileName(string matchCloudFileName)
	{
		core.FilterCloudFileName(matchCloudFileName);
		return this;
	}

	#region Shared
	public new DetailsUgcQueryBuilder AllowCachedResponse(uint maxCacheAgeSeconds)
	{
		base.AllowCachedResponse(maxCacheAgeSeconds);
		return this;
	}

	public new DetailsUgcQueryBuilder Language(string apiLanguageName)
	{
		base.Language(apiLanguageName);
		return this;
	}

	public new DetailsUgcQueryBuilder RequireKeyValueTag(string key, string value)
	{
		base.RequireKeyValueTag(key, value);
		return this;
	}

	public new DetailsUgcQueryBuilder ReturnAdditionalPreviews(bool isReturn)
	{
		base.ReturnAdditionalPreviews(isReturn);
		return this;
	}

	public new DetailsUgcQueryBuilder ReturnChildren(bool isReturn)
	{
		base.ReturnChildren(isReturn);
		return this;
	}

	public new DetailsUgcQueryBuilder ReturnKeyValueTags(bool isReturn)
	{
		base.ReturnKeyValueTags(isReturn);
		return this;
	}

	public new DetailsUgcQueryBuilder ReturnLongDescription(bool isReturn)
	{
		base.ReturnLongDescription(isReturn);
		return this;
	}

	public new DetailsUgcQueryBuilder ReturnMetadata(bool isReturn)
	{
		base.ReturnMetadata(isReturn);
		return this;
	}

	public new DetailsUgcQueryBuilder ReturnPlaytimeStats(uint days)
	{
		base.ReturnPlaytimeStats(days);
		return this;
	}
	#endregion
}
