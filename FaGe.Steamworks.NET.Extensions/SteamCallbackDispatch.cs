using Steamworks;
using System.Diagnostics;

namespace FaGe.Steamworks.NET.Extensions;

public class SteamCallbackDispatch
{
	public static readonly SteamCallbackDispatch Singleton = new();
	private const long ExceptedDispatchFrequencyTicks = TimeSpan.TicksPerMillisecond * 100;

	private SteamCallbackDispatch()
	{
		StopDispatchSource = new CancellationTokenSource();
	}

	public CancellationTokenSource StopDispatchSource { get; private set; }

	private Thread? dispatchThread;
	private bool disposedValue;


	public CancellationTokenSource SpawnCallbackDispatchThread()
	{
		if (dispatchThread != null)
		{
			return StopDispatchSource;
		}

		dispatchThread = new Thread(DispatchThreadFunc)
		{
			IsBackground = true,
			Name = "FaGe S.NET Extensions Steam Event Dispatcher"
		};

		dispatchThread.Start();

		return StopDispatchSource;
	}

	private void DispatchThreadFunc()
	{
		Debug.Assert(StopDispatchSource != null);
		CancellationToken stop = StopDispatchSource.Token;
		while (!stop.IsCancellationRequested)
		{
			long startTick = Environment.TickCount64;
			long exceptedNextDispatchTick = startTick + ExceptedDispatchFrequencyTicks;

			SteamAPI.RunCallbacks();

			long dispatchEndTime;
			if ((dispatchEndTime = Environment.TickCount64) < exceptedNextDispatchTick)
			{
				Thread.Sleep(new TimeSpan(exceptedNextDispatchTick - dispatchEndTime));
			}
		}
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				StopDispatchSource.Cancel();
				StopDispatchSource.Dispose();
			}

			dispatchThread = null;
			disposedValue = true;
		}
	}

	public void Dispose()
	{
		// 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
