using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;

public class UserUgcQueryBuilder : BaseUgcQueryBuilder
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
	// ExcludeTag
	// RequiredTag
	// RequiredTagGroup
	// ReturnOnlyIDs
	// ReturnTotalOnly
	// -------------
	internal UserUgcQueryBuilder(UGCQueryHandle_t handle, bool isGameServer) : base(handle, isGameServer)
	{
		
	}

	public UserUgcQueryBuilder ExcludeTag(string tag)
	{
		core.ExcludeTag(tag);
		return this;
	}

	public UserUgcQueryBuilder RequireTag(string tag)
	{
		core.RequireTag(tag);
		return this;
	}

	public UserUgcQueryBuilder RequireTagGroup(IList<string> tags)
	{
		core.RequireTagGroup(tags);
		return this;
	}

	public UserUgcQueryBuilder ReturnOnlyIDs(bool isReturn)
	{
		core.ReturnOnlyIDs(isReturn);
		return this;
	}

	public UserUgcQueryBuilder ReturnTotalOnly(bool isReturn)
	{
		core.ReturnTotalOnly(isReturn);
		return this;
	}

	#region Shared
	public new UserUgcQueryBuilder AllowCachedResponse(uint maxCacheAgeSeconds)
	{
		base.AllowCachedResponse(maxCacheAgeSeconds);
		return this;
	}

	public new UserUgcQueryBuilder Language(string apiLanguageName)
	{
		base.Language(apiLanguageName);
		return this;
	}

	public new UserUgcQueryBuilder RequireKeyValueTag(string key, string value)
	{
		base.RequireKeyValueTag(key, value);
		return this;
	}

	public new UserUgcQueryBuilder ReturnAdditionalPreviews(bool isReturn)
	{
		base.ReturnAdditionalPreviews(isReturn);
		return this;
	}

	public new UserUgcQueryBuilder ReturnChildren(bool isReturn)
	{
		base.ReturnChildren(isReturn);
		return this;
	}

	public new UserUgcQueryBuilder ReturnKeyValueTags(bool isReturn)
	{
		base.ReturnKeyValueTags(isReturn);
		return this;
	}

	public new UserUgcQueryBuilder ReturnLongDescription(bool isReturn)
	{
		base.ReturnLongDescription(isReturn);
		return this;
	}

	public new UserUgcQueryBuilder ReturnMetadata(bool isReturn)
	{
		base.ReturnMetadata(isReturn);
		return this;
	}

	public new UserUgcQueryBuilder ReturnPlaytimeStats(uint days)
	{
		base.ReturnPlaytimeStats(days);
		return this;
	}
	#endregion

}