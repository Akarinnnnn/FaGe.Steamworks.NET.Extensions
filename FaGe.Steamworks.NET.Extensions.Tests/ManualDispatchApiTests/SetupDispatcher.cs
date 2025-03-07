using Steamworks;

namespace FaGe.Steamworks.NET.Extensions.Tests.ManualDispatchApiTests
{
	[SetUpFixture]
	public class SetupDispatcher
	{
		private CancellationTokenSource dispatchThreadStop;
		private Thread dispatchThread;

		[OneTimeSetUp]
		public void SteamInit()
		{
			dispatchThreadStop = new();
			dispatchThread = new(() =>
			{
				while (!dispatchThreadStop.IsCancellationRequested)
				{
					SteamAPI.RunCallbacks();
					Thread.Sleep(100); // valve recommended frequency
				}
			})
			{
				Name = "Steamworks.NET dispatcher thread",
				IsBackground = true
			};
			dispatchThread.Start();
		}

		[OneTimeTearDown]
		public void SteamShutdown()
		{
			dispatchThreadStop.Cancel();
			dispatchThreadStop.Dispose();
		}

	}
}