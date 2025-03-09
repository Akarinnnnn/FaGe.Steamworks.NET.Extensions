using Steamworks;
using System.Diagnostics;

namespace FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;

#pragma warning disable IDE0251

/// <summary>
/// Query operators collection, cannot be reused once query built.
/// </summary>
internal struct QueryBuilderCore
{
	private UGCQueryHandle_t handle;
	internal readonly bool IsGameServer;
	internal QueryBuilderCore(UGCQueryHandle_t handle, bool isGameServer)
	{
		if (handle == UGCQueryHandle_t.Invalid)
		{
			throw new ArgumentException("Attempt to build UGC query on invalid handle.", nameof(handle));
		}

		this.handle = handle;
		IsGameServer = isGameServer;
	}

	private readonly void CheckValid()
	{
		if (handle == UGCQueryHandle_t.Invalid)
		{
			throw new InvalidOperationException("UGC query is already built or abandoned.");
		}
	}

	public UgcQuery Build()
	{
		CheckValid();

		var result = handle;
		handle = UGCQueryHandle_t.Invalid;
		return new(result, IsGameServer);
	}

	/// <summary>
	/// Obtain a partially built query handle.
	/// </summary>
	/// <remarks>
	/// <para>This method is mainly served for using updated Steamworks.NET UGC query operators that isn't encapsuled here.</para>
	/// <para>Once your query is fully built, you can use returned IsGameServer to create <see cref="UgcQuery"/> instance.</para>
	/// </remarks>
	/// <returns></returns>
	public (UGCQueryHandle_t Handle, bool IsGameServer) BuildPartial()
	{
		CheckValid();

		var result = handle;
		handle = UGCQueryHandle_t.Invalid;
		return new(result, IsGameServer);
	}

	public void ExcludeTag(string tag)
	{
		CheckValid();
		ArgumentNullException.ThrowIfNull(tag);

		bool succeed;
		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.AddExcludedTag(handle, tag);
		}
		else
		{
			succeed = SteamUGC.AddExcludedTag(handle, tag);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($".");
		}
	}

	public void RequireKeyValueTag(string key, string value)
	{
		CheckValid();
		ArgumentNullException.ThrowIfNull(key);
		ArgumentNullException.ThrowIfNull(value);

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.AddRequiredKeyValueTag(handle, key, value);
		}
		else
		{
			succeed = SteamUGC.AddRequiredKeyValueTag(handle, key, value);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($"Cannot add required key-value paired tag on this query({handle}).");
		}
	}

	public void RequireTag(string tag)
	{
		CheckValid();
		ArgumentNullException.ThrowIfNull(tag);

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.AddRequiredTag(handle, tag);
		}
		else
		{
			succeed = SteamUGC.AddRequiredTag(handle, tag);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($"Cannot add required tag on this query({handle}).");
		}
	}

	public void RequireTagGroup(IList<string> tags)
	{
		CheckValid();
		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.AddRequiredTagGroup(handle, tags);
		}
		else
		{
			succeed = SteamUGC.AddRequiredTagGroup(handle, tags);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($"Cannot add required tag group on this query({handle}).");
		}
	}
	
	public void AllowCachedResponse(uint maxCacheAgeSeconds)
	{
		CheckValid();
		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetAllowCachedResponse(handle, maxCacheAgeSeconds);
		}
		else
		{
			succeed = SteamUGC.SetAllowCachedResponse(handle, maxCacheAgeSeconds);
		}

		Debug.Assert(succeed, "Steamworks API internal error?");
		if (!succeed)
		{
			throw new UgcQueryBuildException($"Steamworks API internal error?");
		}
	}
	
	public void FilterCloudFileName(string matchCloudFileName)
	{
		CheckValid();
		ArgumentNullException.ThrowIfNull(matchCloudFileName);
		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetCloudFileNameFilter(handle, matchCloudFileName);
		}
		else
		{
			succeed = SteamUGC.SetCloudFileNameFilter(handle, matchCloudFileName);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($"UGC query handle is not from CreateQueryUGCDetailsRequest() or Steamworks internal error happened.");
		}
	}
	
	public void Language(string apiLanguageName)
	{
		CheckValid();
		ArgumentNullException.ThrowIfNull(apiLanguageName);

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetLanguage(handle, apiLanguageName);
		}
		else
		{
			succeed = SteamUGC.SetLanguage(handle, apiLanguageName);
		}

		Debug.Assert(succeed, "Steamworks API internal error?");
		if (!succeed)
		{
			throw new UgcQueryBuildException($"Steamworks API internal error?");
		}
	}

	public void MatchAnyTag(bool isMatchAnyTag)
	{
		CheckValid();
		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetMatchAnyTag(handle, isMatchAnyTag);
		}
		else
		{
			succeed = SteamUGC.SetMatchAnyTag(handle, isMatchAnyTag);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($"UGC query handle is not from CreateQueryAllUGCRequest().");
		}
	}

	public void RankByTrendDays(uint days)
	{
		CheckValid();
		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetRankedByTrendDays(handle, days);
		}
		else
		{
			succeed = SteamUGC.SetRankedByTrendDays(handle, days);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($"Failed to set rank order by trend days, see help link for details.")
			{
				HelpLink = "https://partner.steamgames.com/doc/api/ISteamUGC#SetRankedByTrendDays"
			};
		}
	}

	public void ReturnAdditionalPreviews(bool isReturn)
	{
		CheckValid();

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetReturnAdditionalPreviews(handle, isReturn);
		}
		else
		{
			succeed = SteamUGC.SetReturnAdditionalPreviews(handle, isReturn);
		}

		Debug.Assert(succeed, "Steamworks API internal error?");
		if (!succeed)
		{
			throw new UgcQueryBuildException($"Steamworks API internal error?");
		}
	}

	public void ReturnChildren(bool isReturn)
	{
		CheckValid();

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetReturnChildren(handle, isReturn);
		}
		else
		{
			succeed = SteamUGC.SetReturnChildren(handle, isReturn);
		}

		Debug.Assert(succeed, "Steamworks API internal error?");
		if (!succeed)
		{
			throw new UgcQueryBuildException($"Steamworks API internal error?");
		}
	}

	public void ReturnKeyValueTags(bool isReturn)
	{
		CheckValid();

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetReturnKeyValueTags(handle, isReturn);
		}
		else
		{
			succeed = SteamUGC.SetReturnKeyValueTags(handle, isReturn);
		}

		Debug.Assert(succeed, "Steamworks API internal error?");
		if (!succeed)
		{
			throw new UgcQueryBuildException($"Steamworks API internal error?");
		}
	}

	public void ReturnLongDescription(bool isReturn)
	{
		CheckValid();

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetReturnLongDescription(handle, isReturn);
		}
		else
		{
			succeed = SteamUGC.SetReturnLongDescription(handle, isReturn);
		}

		Debug.Assert(succeed, "Steamworks API internal error?");
		if (!succeed)
		{
			throw new UgcQueryBuildException($"Steamworks API internal error?");
		}
	}
	
	public void ReturnMetadata(bool isReturn)
	{
		CheckValid();

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetReturnMetadata(handle, isReturn);
		}
		else
		{
			succeed = SteamUGC.SetReturnMetadata(handle, isReturn);
		}

		Debug.Assert(succeed, "Steamworks API internal error?");
		if (!succeed)
		{
			throw new UgcQueryBuildException($"Steamworks API internal error?");
		}
	}
	
	public void ReturnOnlyIDs(bool isReturn)
	{
		CheckValid();

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetReturnOnlyIDs(handle, isReturn);
		}
		else
		{
			succeed = SteamUGC.SetReturnOnlyIDs(handle, isReturn);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($"UGC query handle can't from CreateQueryUGCDetailsRequest().");
		}
	}

	public void ReturnPlaytimeStats(uint days)
	{
		CheckValid();

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetReturnPlaytimeStats(handle, days);
		}
		else
		{
			succeed = SteamUGC.SetReturnPlaytimeStats(handle, days);
		}

		Debug.Assert(succeed, "Steamworks API internal error?");
		if (!succeed)
		{
			throw new UgcQueryBuildException($"Steamworks API internal error?");
		}
	}

	public void ReturnTotalOnly(bool isReturn)
	{
		CheckValid();

		bool succeed;

		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.SetReturnTotalOnly(handle, isReturn);
		}
		else
		{
			succeed = SteamUGC.SetReturnTotalOnly(handle, isReturn);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($"UGC query handle can't from CreateQueryUGCDetailsRequest().");
		}
	}

	public void SearchText(string text)
	{
		CheckValid();
		ArgumentNullException.ThrowIfNull(text);

		bool succeed;
		if (IsGameServer)
		{
			succeed = SteamGameServerUGC.AddExcludedTag(handle, text);
		}
		else
		{
			succeed = SteamUGC.AddExcludedTag(handle, text);
		}

		if (!succeed)
		{
			throw new UgcQueryBuildException($"UGC query handle must from CreateQueryAllUGCRequest().");
		}
	}

	public void Abandon()
	{
		var handle = this.handle;
		if (handle != UGCQueryHandle_t.Invalid)
		{
			this.handle = UGCQueryHandle_t.Invalid;
			if (IsGameServer)
			{
				SteamGameServerUGC.ReleaseQueryUGCRequest(handle);
			}
			else
			{
				SteamUGC.ReleaseQueryUGCRequest(handle);
			}
		}
	}
}