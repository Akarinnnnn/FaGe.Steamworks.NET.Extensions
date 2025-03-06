
using Steamworks;

namespace FaGe.Steamworks.NET.Extensions
{
	[Serializable]
	public class SteamAPICallException : Exception
	{
		public SteamAPICallException(string? message, ESteamAPICallFailure reason) : base(message)
		{
			Reason = reason;
		}

		public SteamAPICallException(string? message, ESteamAPICallFailure reason, Exception? innerException) : base(message, innerException)
		{
			Reason = reason;
		}

		public ESteamAPICallFailure Reason { get; }
	}
}