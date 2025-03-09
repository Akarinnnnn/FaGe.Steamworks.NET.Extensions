using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Ugc;
[Serializable]
public class SteamUgcQueryFailedException : Exception
{
	public EResult FailResult { get; }


	public SteamUgcQueryFailedException(string message, EResult m_eResult) : base(message)
	{
		FailResult = m_eResult;
	}

	public SteamUgcQueryFailedException(string? message, EResult m_eResult, Exception? innerException)
		: base(message, innerException)
	{
		FailResult = m_eResult;
	}
}