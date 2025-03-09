using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;
public class BaseUgcQueryBuilder
{
	// Allowed
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
	private protected QueryBuilderCore core;

	internal BaseUgcQueryBuilder(UGCQueryHandle_t handle, bool isGameServer)
	{
		core = new(handle, isGameServer);
	}

	public UgcQuery Build() => core.Build();

	public (UGCQueryHandle_t Handle, bool IsGameServer) BuildPartial() => core.BuildPartial();

	public void AbandonQuery() => core.Abandon();

	public BaseUgcQueryBuilder AllowCachedResponse(uint maxCacheAgeSeconds)
	{
		core.AllowCachedResponse(maxCacheAgeSeconds);
		return this;
	}

	public BaseUgcQueryBuilder Language(string apiLanguageName)
	{
		core.Language(apiLanguageName);
		return this;
	}

	public BaseUgcQueryBuilder RequireKeyValueTag(string key, string value)
	{
		core.RequireKeyValueTag(key, value);
		return this;
	}

	public BaseUgcQueryBuilder ReturnAdditionalPreviews(bool isReturn)
	{
		core.ReturnAdditionalPreviews(isReturn);
		return this;
	}

	public BaseUgcQueryBuilder ReturnChildren(bool isReturn)
	{
		core.ReturnChildren(isReturn);
		return this;
	}

	public BaseUgcQueryBuilder ReturnKeyValueTags(bool isReturn)
	{
		core.ReturnKeyValueTags(isReturn);
		return this;
	}

	public BaseUgcQueryBuilder ReturnLongDescription(bool isReturn)
	{
		core.ReturnLongDescription(isReturn);
		return this;
	}
	
	public BaseUgcQueryBuilder ReturnMetadata(bool isReturn)
	{
		core.ReturnMetadata(isReturn);
		return this;
	}

	public BaseUgcQueryBuilder ReturnPlaytimeStats(uint days)
	{
		core.ReturnPlaytimeStats(days);
		return this;
	}
}
