# FaGe's Steamworks.NET Extension for .NET 8+

As you can see, this project is aimed to extend modern features to `Steamworks.NET` on regular .NET projects that targeting to up to date framework.

## Extended features list

- Allow get Steam API CallResult by `await`, achived by `ToValueTask<T>()` extension method series on `SteamAPICall_t`.
	These methods build on top of original `CallResult<T>` and can be accessed by using namespace `FaGe.Steamworks.NET.Extensions`. In current version, there are 2 variants.
	- Method `ToValueTask<T>()` is suitable for those call-results that don't considering cancellation.
		Example:
		```csharp
		var queryResult = await SteamUGC.SendQueryUGCRequest(queryHandle).ToValueTask<SteamUGCQueryCompleted_t>();
		```
	- Method `ToValueTaskWithCancellation<T>()` returns a `ValueTask<T>` and carry out a `CancellationTokenSource` by the way,
		you can later cancel this operation by that `CancellationTokenSource`. Example:
		```csharp
		// do async operation
		var result = SteamUGC.SendQueryUGCRequest(queryHandle).ToValueTaskWithCancellation<SteamUGCQueryCompleted_t>(out var ctsInSomeField);

		// somewhere else that want to cancel
		ctsInSomeField.Cancel()
		```