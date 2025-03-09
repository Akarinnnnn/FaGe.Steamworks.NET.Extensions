namespace FaGe.Steamworks.NET.Extensions.Ugc;

public struct UgcTag(string v1, string v2, uint index)
{
	public string Tag { get; } = v1;
	public string DisplayName { get; } = v2;
	public uint Index { get; } = index;
}