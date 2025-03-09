
using Steamworks;

namespace FaGe.Steamworks.NET.Extensions;

[Serializable]
public class SteamAPICallException : Exception
{
	public SteamAPICallException(string? message) : base(message)
	{
		
	}
	
	public SteamAPICallException(string? message, Exception? innerException) : base(message, innerException)
	{
		
	}

	public SteamAPICallException(string? message, ESteamAPICallFailure reason) : base(message + $"\nFailure reason: {reason}")
	{
		Reason = reason;
	}

	public SteamAPICallException(string? message, ESteamAPICallFailure reason, Exception? innerException) : base(message + $"\nFailure reason: {reason}", innerException)
	{
		Reason = reason;
	}

	public ESteamAPICallFailure Reason { get; }
}