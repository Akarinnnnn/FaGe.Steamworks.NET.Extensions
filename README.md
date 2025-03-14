



# FaGe's Steamworks.NET Extension for .NET 8+ [![NuGet Version](https://img.shields.io/nuget/v/FaGe.Steamworks.NET.Extensions)](https://www.nuget.org/packages/fage.steamworks.net.extensions) ![GitHub License](https://img.shields.io/github/license/Akarinnnnn/FaGe.Steamworks.NET.Extensions)

As you can see, this project is aimed to extend modern features to `Steamworks.NET` on regular .NET projects that targeting to up to date framework.

## Extended features list

- Allow get Steam API CallResult by `await`, achived by `ToValueTask<T>()` extension method series on `SteamAPICall_t`.
	These methods build on top of original `CallResult<T>` and can be accessed by using namespace `FaGe.Steamworks.NET.Extensions`. In current version, there are 2 variants.
	- Method `ToValueTask<T>()` is suitable for those call-results that don't considering cancellation.
		Example:
		```csharp
		var queryResult = await SteamUGC.SendQueryUGCRequest(queryHandle).ToValueTask<SteamUGCQueryCompleted_t>();
		```
	- Method `ToValueTaskWithCancellation<T>()` returns a `ValueTask<T>` and carry out a `CancellationTokenSource` for cancellation,
		you can later cancel this operation by that `CancellationTokenSource`. Example:
		```csharp
		// do async operation
		var result = SteamUGC.SendQueryUGCRequest(queryHandle).ToValueTaskWithCancellation<SteamUGCQueryCompleted_t>(out var ctsInSomeField);

		// somewhere else that want to cancel
		ctsInSomeField.Cancel()
		```
	- `SteamAPICallException` will thrown if call-result is **failed**.
- Spawn automatic steam callback dispatcher thread by `SteamCallbackDispatch.Singleton.SpawnCallbackDispatchThread()` method.
	- Stop the dispatcher thread by the returned `CancellationTokenSource`. `SteamCallbackDispatch.Singleton.StopDispatchSource` is also the same.
		Beware, once the thread is stopped, it can't be restarted.
	- Lived in namespace `FaGe.Steamworks.NET.Extensions`
- Wrap `UgcQueryHandle_t` for both query and result.
	- Create fluent query builder from methods in static class `FaGe.Steamworks.NET.Extensions.Ugc.UgcQueryBuilders`.
		Each factory method wraps a corresponding `CreateQuery*UGCRequest()`. 
		- Exception
			- `UgcQueryBuildException`: query operator is failed to apply.
		- If you want to abandon a query, you should call `*UgcQueryBuilder.AbandonQuery()` to release handle.
		- For now in the unit test project `DetailsUgcQueryBuilder.FilterCloudFileName()` will **fail**	
		  due to unknown reason, no corresponding IPC log found in steam console. Not sure if real application will fail too.
	- Once query built, call `UgcQuery.Send()` to get result wrapped in `UgcQueryResult`.
		- `UgcQueryFailedException` will thrown for **failed** query.
	- The `UgcQueryResult` is a collection of query result, each item is a `UgcQueryResultItem`. Methods like `GetDetails()`
		and `GetTags()` are used to retrieve information of a single UGC item. There are also some count properties.
