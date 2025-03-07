using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Tests.AutomaticDispatchApiTests
{
	[SetUpFixture]
	public class AutomaticDispatcherManager
	{
		private CancellationTokenSource dispatchThreadStop;

		[OneTimeSetUp]
		public void SteamInit()
		{
			dispatchThreadStop = SteamCallbackDispatch.Singleton.SpawnCallbackDispatchThread();
		}

		[OneTimeTearDown]
		public void SteamShutdown()
		{
			dispatchThreadStop.Cancel();
			dispatchThreadStop.Dispose();
		}

	}
}