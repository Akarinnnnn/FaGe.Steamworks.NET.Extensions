using FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaGe.Steamworks.NET.Extensions.Ugc;

/// <summary>
/// Entry of all UGC query builders
/// </summary>
public static class UgcQueryBuilders
{
	public static UserUgcQueryBuilder User(
		AccountID_t accountID,
		EUserUGCList listType,
		EUGCMatchingUGCType matchingUGCType,
		EUserUGCListSortOrder sortOrder,
		AppId_t creatorAppID,
		AppId_t consumerAppID,
		uint page,
		bool isGameServer)
	{
		UGCQueryHandle_t handle;
		
		if (isGameServer)
		{
			handle = SteamGameServerUGC.CreateQueryUserUGCRequest(accountID, listType, matchingUGCType, sortOrder, creatorAppID, consumerAppID, page);
		}
		else
		{
			handle = SteamUGC.CreateQueryUserUGCRequest(accountID, listType, matchingUGCType, sortOrder, creatorAppID, consumerAppID, page);
		}


		return new UserUgcQueryBuilder(handle, isGameServer);
	}

	public static AllUgcQueryBuilder All(
		EUGCQuery eQueryType,
		EUGCMatchingUGCType matchingFiletype,
		AppId_t creatorAppid,
		AppId_t consumerAppid,
		bool isGameServer,
		string? cursor = null)
	{
		UGCQueryHandle_t handle;

		if (isGameServer)
		{
			handle = SteamGameServerUGC.CreateQueryAllUGCRequest(eQueryType, matchingFiletype, creatorAppid, consumerAppid, cursor);
		}
		else
		{
			handle = SteamUGC.CreateQueryAllUGCRequest(eQueryType, matchingFiletype, creatorAppid, consumerAppid, cursor);
		}

		return new AllUgcQueryBuilder(handle, isGameServer);
	}

	public static AllUgcQueryBuilder All(
		EUGCQuery eQueryType,
		EUGCMatchingUGCType matchingFiletype,
		AppId_t creatorAppId,
		AppId_t consumerAppId,
		bool isGameServer,
		uint page)
	{
		UGCQueryHandle_t handle;

		if (isGameServer)
		{
			handle = SteamGameServerUGC.CreateQueryAllUGCRequest(eQueryType, matchingFiletype, creatorAppId, consumerAppId, page);
		}
		else
		{
			handle = SteamUGC.CreateQueryAllUGCRequest(eQueryType, matchingFiletype, creatorAppId, consumerAppId, page);
		}

		return new AllUgcQueryBuilder(handle, isGameServer);
	}

	public static DetailsUgcQueryBuilder Details(PublishedFileId_t[] publishedFileIds, bool isGameServer)
	{
		if (publishedFileIds.Length == 0)
		{
			throw new ArgumentException("Query details must specify at least 1 item id.", nameof(publishedFileIds));
		}

		UGCQueryHandle_t handle;

		if (isGameServer)
		{
			handle = SteamGameServerUGC.CreateQueryUGCDetailsRequest(publishedFileIds, (uint)publishedFileIds.Length);
		}
		else
		{
			handle = SteamUGC.CreateQueryUGCDetailsRequest(publishedFileIds, (uint)publishedFileIds.Length);
		}

		return new DetailsUgcQueryBuilder(handle, isGameServer);
	}
}
