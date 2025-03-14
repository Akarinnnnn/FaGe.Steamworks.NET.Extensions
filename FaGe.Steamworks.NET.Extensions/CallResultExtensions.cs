﻿using Steamworks;

namespace FaGe.Steamworks.NET.Extensions;

public static class CallResultExtensions
{
	public static ValueTask<T> ToValueTaskWithCancellation<T>(this SteamAPICall_t handle, out CancellationTokenSource cancel)
		where T : struct
	{
		CancellationTokenSource cts = new();
		CallResult<T> native = new();
		cts.Token.Register(native.Cancel);

		cancel = cts;

		// pass cts.Token here is for awaiting place know task cancelled, then throw a OCE as excepted
		return new CallResultValueTaskSource<T>().Setup(handle, native, cts.Token);
	}

	public static ValueTask<T> ToValueTask<T>(this SteamAPICall_t handle)
		where T : struct
	{
		CallResult<T> native = new();

		return new CallResultValueTaskSource<T>().Setup(handle, native);
	}
}
