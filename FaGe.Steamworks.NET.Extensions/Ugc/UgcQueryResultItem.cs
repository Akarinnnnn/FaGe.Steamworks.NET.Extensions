using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Ugc;

/// <summary>
/// Single item returned from UGC Query.
/// </summary>
/// <remarks>
///  See ISteamUGC.GetQueryUGC* functions for more detailed document.
/// </remarks>
public abstract class UgcQueryResultItem
{
	protected UgcQueryResultItem(UGCQueryHandle_t handle, uint index)
	{
		QueryHandle = handle;
		QueryResultIndex = index;
	}

	public UGCQueryHandle_t QueryHandle { get; }

	public uint QueryResultIndex { get; }

	public uint TagCount { get; protected set; }
	public uint KeyValueTagCount { get; protected set; }
	public uint AdditionalPreviewCount { get; protected set; }
	public uint SupportedGameVersionsCount { get; protected set; }
	public SteamUGCDetails_t? CachedDetails { get; protected set; }


	private string? previewUrl;
	public string PreviewUrl
	{
		get
		{
			if (previewUrl != null)
			{
				return previewUrl;
			}

			return previewUrl = GetMainPreviewUrl(260);
		}
	}


	internal UgcQueryResultItem Initialize()
	{
		FillNumberProperties();
		return this;
	}
	protected abstract void FillNumberProperties();

	public abstract string GetTagString(uint index, int stringMaxLength);
	public abstract string GetTagDisplayName(uint index, int stringMaxLength);

	public UgcTag[] GetTags(int maxStringLength = 64)
	{
		UgcTag[] result = new UgcTag[TagCount];
		for (uint i = 0; i < result.Length; i++)
		{
			result[i] = GetTagAt(i, maxStringLength);
		}

		return result;
	}

	public UgcTag GetTagAt(uint index, int maxStringLength = 64)
	{
		return new(GetTagString(index, maxStringLength), GetTagDisplayName(index, maxStringLength), index);
	}

	public abstract KeyValuePair<string, string> GetKeyValueTagAt(uint tagIndex, int maxStringLength);

	public KeyValuePair<string, string>[] GetKeyValueTags(int maxStringLength)
	{
		KeyValuePair<string, string>[] result = new KeyValuePair<string, string>[KeyValueTagCount];
		for (uint i = 0; i < result.Length; i++)
		{
			result[i] = GetKeyValueTagAt(i, maxStringLength);
		}

		return result;
	}

	public abstract string GetKeyedTagValue(string key, int stringMaxLength);

	public abstract UgcAdditionalPreview GetAdditionalPreview(uint previewIndex);

	public abstract PublishedFileId_t[] GetQueryUGCChildren();

	public abstract Memory<EUGCContentDescriptorID> GetContentDescriptors(int maxCount);

	public abstract SteamUGCDetails_t GetDetails();

	public abstract bool TryGetMetadata(uint maxUrlLength, out string metadata);

	
	public abstract string GetMainPreviewUrl(int maxUrlLength);

	public abstract bool TryGetStatistic(EItemStatistic type, out ulong statValue);

	public abstract bool TryGetSupportedGameVersionData(uint versionIndex, out string minGameBranch, out string maxGameBranch, uint maxVersionStringLength);
}

