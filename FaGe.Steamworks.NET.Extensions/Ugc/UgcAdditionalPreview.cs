using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Ugc;

public class UgcAdditionalPreview
{
	public UgcAdditionalPreview(string urlOrVID, string originalFileName, EItemPreviewType previewType)
	{
		UrlOrVideoID = urlOrVID;
		OriginalFileName = originalFileName;
		PreviewType = previewType;
	}

	public string UrlOrVideoID { get; }
	public string OriginalFileName { get; }
	public EItemPreviewType PreviewType { get; }
}