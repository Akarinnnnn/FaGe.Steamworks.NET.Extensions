using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;
public class AllUgcQueryBuilder : BaseUgcQueryBuilder
{
	internal AllUgcQueryBuilder(UGCQueryHandle_t handle, bool isGameServer) : base(handle, isGameServer)
	{
	}

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
	// ExcludeTag
	// MatchAnyTag
	// RankedByTrendDays
	// RequiredTag
	// RequiredTagGroup
	// ReturnOnlyIDs
	// ReturnTotalOnly
	// SearchText
	// -------------

	public AllUgcQueryBuilder ExcludeTag(string tag)
	{
		core.ExcludeTag(tag);
		return this;
	}

	public AllUgcQueryBuilder MatchAnyTag(bool isMatchAnyTag)
	{
		core.MatchAnyTag(isMatchAnyTag);
		return this;
	}

	public AllUgcQueryBuilder RankByTrendDays(uint days)
	{
		core.RankByTrendDays(days);
		return this;
	}

	public AllUgcQueryBuilder RequireTag(string tag)
	{
		core.RequireTag(tag);
		return this;
	}

	public AllUgcQueryBuilder RequireTagGroup(IList<string> tags)
	{
		core.RequireTagGroup(tags);
		return this;
	}

	public AllUgcQueryBuilder ReturnOnlyIDs(bool isReturn)
	{
		core.ReturnOnlyIDs(isReturn);
		return this;
	}

	public AllUgcQueryBuilder ReturnTotalOnly(bool isReturn)
	{
		core.ReturnTotalOnly(isReturn);
		return this;
	}

	public AllUgcQueryBuilder SearchText(string text)
	{
		core.SearchText(text);
		return this;
	}

	#region Shared
	public new AllUgcQueryBuilder AllowCachedResponse(uint maxCacheAgeSeconds)
	{
		base.AllowCachedResponse(maxCacheAgeSeconds);
		return this;
	}

	public new AllUgcQueryBuilder Language(string apiLanguageName)
	{
		base.Language(apiLanguageName);
		return this;
	}

	public new AllUgcQueryBuilder RequireKeyValueTag(string key, string value)
	{
		base.RequireKeyValueTag(key, value);
		return this;
	}

	public new AllUgcQueryBuilder ReturnAdditionalPreviews(bool isReturn)
	{
		base.ReturnAdditionalPreviews(isReturn);
		return this;
	}

	public new AllUgcQueryBuilder ReturnChildren(bool isReturn)
	{
		base.ReturnChildren(isReturn);
		return this;
	}

	public new AllUgcQueryBuilder ReturnKeyValueTags(bool isReturn)
	{
		base.ReturnKeyValueTags(isReturn);
		return this;
	}

	public new AllUgcQueryBuilder ReturnLongDescription(bool isReturn)
	{
		base.ReturnLongDescription(isReturn);
		return this;
	}

	public new AllUgcQueryBuilder ReturnMetadata(bool isReturn)
	{
		base.ReturnMetadata(isReturn);
		return this;
	}

	public new AllUgcQueryBuilder ReturnPlaytimeStats(uint days)
	{
		base.ReturnPlaytimeStats(days);
		return this;
	}
	#endregion
}
