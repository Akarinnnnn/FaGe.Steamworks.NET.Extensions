using Steamworks;
using System.Threading.Tasks.Sources;

namespace FaGe.Steamworks.NET.Extensions;

/// <summary>
/// ValueTask adapter for CallResult<typeparamref name="T"/>
/// </summary>
/// <remarks>
/// This object is reusable, but <see cref="CallResultExtensions.ToValueTask{T}(SteamAPICall_t)"/> series method new one every time.
/// <para>Due to the massive amount of result types, pool instances on every types may instantize generic too many times.</para>
/// <para>Do pooling yourself on frequently used result types might helpful to performance.</para>
/// </remarks>
/// <typeparam name="T">Call Result return type</typeparam>
public class CallResultValueTaskSource<T> : IValueTaskSource<T> where T : struct
{
	private ManualResetValueTaskSourceCore<T> core = new();

	private SteamAPICall_t callHandle;
	private CancellationToken cancellationToken;

	public CallResult<T>? NativeCallback { get; private set; }

	public T GetResult(short token)
	{
		return core.GetResult(token);
	}

	public ValueTaskSourceStatus GetStatus(short token)
	{
		return core.GetStatus(token);
	}

	public void OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
	{
		if (callHandle == SteamAPICall_t.Invalid)
		{
			throw new InvalidOperationException("Steam API CallResult handle is not set, before await ValueTask, call Setup() first.");
		}

		core.OnCompleted(continuation, state, token, flags);
	}

	private void OnCallResultCompleted(T value, bool isFailed)
	{
		if (isFailed)
		{
			ESteamAPICallFailure failReason;
			try
			{
				failReason = SteamUtils.GetAPICallFailureReason(callHandle);
			}
			catch (InvalidOperationException)
			{
				failReason = ESteamAPICallFailure.k_ESteamAPICallFailureNone;
			}
			core.SetException(new SteamAPICallException("Steam API call failed", failReason));
		}

		core.SetResult(value);
	}

	private void TriggerCancel()
	{
		try
		{
			cancellationToken.ThrowIfCancellationRequested();
		}
		catch (OperationCanceledException e)
		{
			core.SetException(e);
		}
	}

	public ValueTask<T> Setup(SteamAPICall_t handle, CallResult<T> result, CancellationToken cancellationToken = default)
	{
		callHandle = handle;
		NativeCallback = result;
		if (cancellationToken.CanBeCanceled)
		{
			cancellationToken.Register(TriggerCancel);
			this.cancellationToken = cancellationToken;
		}
		core.Reset();
		result.Set(handle, OnCallResultCompleted);
		return new(this, core.Version);
	}
}
