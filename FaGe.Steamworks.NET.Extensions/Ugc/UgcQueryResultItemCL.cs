using Steamworks;
using System.Diagnostics;

namespace FaGe.Steamworks.NET.Extensions.Ugc;

internal class UgcQueryResultItemCL : UgcQueryResultItem
{
	public UgcQueryResultItemCL(UGCQueryHandle_t handle, uint index) : base(handle, index)
	{
	}

	public override UgcAdditionalPreview GetAdditionalPreview(uint previewIndex)
	{
		if (SteamUGC.GetQueryUGCAdditionalPreview(QueryHandle, QueryResultIndex, previewIndex, out var urlOrVID, 600,
			out var originalFileName, 260,
			out EItemPreviewType previewType))
		{
			return new(urlOrVID, originalFileName, previewType);
		}
		else
		{
			throw new SteamAPICallException("Failed to get additional preview");
		}
	}

	public override Memory<EUGCContentDescriptorID> GetContentDescriptors(int maxCount)
	{
		if (maxCount < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(maxCount), "Max possible descriptor count must be positive.");
		}

		var buffHolder = new EUGCContentDescriptorID[maxCount];
		uint count = SteamUGC.GetQueryUGCContentDescriptors(QueryHandle, QueryResultIndex, buffHolder, (uint)maxCount);
		Debug.Assert(count <= int.MaxValue);
		return new(buffHolder, 0, (int)count);
	}

	public override SteamUGCDetails_t GetDetails()
	{
		if (CachedDetails is not null)
		{
			return CachedDetails.Value;
		}

		if (SteamUGC.GetQueryUGCResult(QueryHandle, QueryResultIndex, out var ret))
		{
			CachedDetails = ret;
			return ret;
		}
		throw new SteamAPICallException("Failed to get UGC detail.");
	}

	public override string GetMainPreviewUrl(int maxUrlLength)
	{
		if (maxUrlLength < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(maxUrlLength), "Max string length must be positive.");
		}

		if (SteamUGC.GetQueryUGCPreviewURL(QueryHandle, QueryResultIndex, out string result, (uint)maxUrlLength))
		{
			return result;
		}

		throw new SteamAPICallException("Failed to retrieve main preview url.");
	}

	public override PublishedFileId_t[] GetQueryUGCChildren()
	{
		var detail = GetDetails();
		PublishedFileId_t[] result = new PublishedFileId_t[detail.m_unNumChildren];

		if (SteamUGC.GetQueryUGCChildren(QueryHandle, QueryResultIndex, result, (uint)result.Length))
		{
			return result;
		}

		throw new SteamAPICallException("Failed to retrieve children UGC item IDs.");
	}

	public override bool TryGetMetadata(uint maxUrlLength, out string metadata)
	{
		return SteamUGC.GetQueryUGCMetadata(QueryHandle, QueryResultIndex, out metadata, maxUrlLength);
	}

	public override bool TryGetStatistic(EItemStatistic type, out ulong statValue)
	{
		return SteamUGC.GetQueryUGCStatistic(QueryHandle, QueryResultIndex, type, out statValue);
	}

	public override bool TryGetSupportedGameVersionData(uint versionIndex, out string minGameBranch, out string maxGameBranch, uint maxVersionStringLength)
	{
		return SteamUGC.GetSupportedGameVersionData(QueryHandle, QueryResultIndex, versionIndex,
			out minGameBranch, out maxGameBranch, maxVersionStringLength);
	}

	protected override void FillNumberProperties()
	{
		AdditionalPreviewCount = SteamUGC.GetQueryUGCNumTags(QueryHandle, QueryResultIndex); ;
		KeyValueTagCount = SteamUGC.GetQueryUGCNumKeyValueTags(QueryHandle, QueryResultIndex);
		TagCount = SteamUGC.GetQueryUGCNumTags(QueryHandle, QueryResultIndex);
		SupportedGameVersionsCount = SteamUGC.GetNumSupportedGameVersions(QueryHandle, QueryResultIndex);
	}

	public override string GetTagDisplayName(uint index, int stringMaxLength)
	{
		if (stringMaxLength < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(stringMaxLength), "Max string length must be positive.");
		}

		if (SteamUGC.GetQueryUGCTagDisplayName(QueryHandle, QueryResultIndex, index, out string result, (uint)stringMaxLength))
		{
			return result;
		}

		throw new SteamAPICallException("Failed to retrieve tag display name.");
	}

	public override string GetTagString(uint index, int stringMaxLength)
	{
		if (stringMaxLength < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(stringMaxLength), "Max string length must be positive.");
		}

		if (SteamUGC.GetQueryUGCTag(QueryHandle, QueryResultIndex, index, out string result, (uint)stringMaxLength))
		{
			return result;
		}

		throw new SteamAPICallException("Failed to retrieve tag display name.");
	}

	public override KeyValuePair<string, string> GetKeyValueTagAt(uint tagIndex, int stringMaxLength)
	{
		if (stringMaxLength < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(stringMaxLength), "Max string length must be positive.");
		}

		if (SteamUGC.GetQueryUGCKeyValueTag(QueryHandle, QueryResultIndex, tagIndex, out string k, (uint)stringMaxLength, out string v, (uint)stringMaxLength))
		{
			return new(k, v);
		}

		throw new SteamAPICallException($"Failed to retrieve key-value tag at index {tagIndex}.");
	}

	public override string GetKeyedTagValue(string key, int stringMaxLength)
	{
		if (stringMaxLength < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(stringMaxLength), "Max string length must be positive.");
		}

		if (SteamUGC.GetQueryUGCKeyValueTag(QueryHandle, QueryResultIndex, key, out string value, (uint)stringMaxLength))
		{
			return value;
		}

		throw new SteamAPICallException($"Failed to retrieve key-value tag by key {key}.");
	}
}

