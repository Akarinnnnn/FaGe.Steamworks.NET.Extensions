using Steamworks;
using System.Collections;
using System.Runtime.CompilerServices;

namespace FaGe.Steamworks.NET.Extensions.Ugc;

/// <summary>
/// Wrapped <see cref="SteamUGCQueryCompleted_t"/> for convenient operations;
/// </summary>
public class UgcQueryResult : IReadOnlyList<UgcQueryResultItem>, IDisposable
{
	private bool disposedValue;
	private readonly bool isGameServer;

	public UgcQueryResult(SteamUGCQueryCompleted_t completedResult, bool isGameServer)
	{
		CompletedResultOriginal = completedResult;
		if (completedResult.m_eResult != EResult.k_EResultOK)
		{
			throw new SteamUgcQueryFailedException($"UGC query failed, EResult: {completedResult.m_eResult}", completedResult.m_eResult);
		}
		SuccessfulQueryHandle = completedResult.m_handle;

		// original NextCursor is based on byte[] buffer due to marshal issues, access it
		// will scan for end '\0' and convert encoding every time
		NextCursor = completedResult.m_rgchNextCursor;
		this.isGameServer = isGameServer;
	}

	public SteamUGCQueryCompleted_t CompletedResultOriginal { get; }
	public UGCQueryHandle_t SuccessfulQueryHandle { get; }

	public EResult QueryResult => CompletedResultOriginal.m_eResult;

	public uint TotalMatchingResults => CompletedResultOriginal.m_unTotalMatchingResults;

	public string NextCursor { get; }

	public uint Count => CompletedResultOriginal.m_unNumResultsReturned;

	int IReadOnlyCollection<UgcQueryResultItem>.Count => (int)Count;

	public UgcQueryResultItem this[int index] => this[checked((uint)index)];

	public UgcQueryResultItem this[uint index]
	{
		get
		{
			if (index < Count)
			{
				return CreateItem(SuccessfulQueryHandle, index);
			}

			throw new IndexOutOfRangeException("Specified index is greater than item count");
		}
	}

	private UgcQueryResultItem CreateItem(UGCQueryHandle_t successfulQueryHandle, uint index)
	{
		if (isGameServer)
		{
			return CreateItemGS(successfulQueryHandle, index).Initialize();
		}
		else
		{
			return CreateItemCL(successfulQueryHandle, index).Initialize();
		}
	}

	private static UgcQueryResultItemGS CreateItemGS(UGCQueryHandle_t successfulQueryHandle, uint index)
		=> new(successfulQueryHandle, index);

	private static UgcQueryResultItemCL CreateItemCL(UGCQueryHandle_t successfulQueryHandle, uint index)
		=> new(successfulQueryHandle, index);


	private void CheckNotDisposed()
	{
		ObjectDisposedException.ThrowIf(disposedValue, this);
	}

	#region Enumerable
	public struct Enumerator : IEnumerator<UgcQueryResultItem>
	{
		private readonly UgcQueryResult parent;
		private int state = -1;

		public Enumerator(UgcQueryResult parent)
		{
			if (parent.Count > int.MaxValue)
			{
				throw new IndexOutOfRangeException("UGC items are too many(more than int.MaxValue).");
			}

			this.parent = parent;
		}

		public UgcQueryResultItem Current
		{
			get
			{
				if (state == -1)
				{
					throw new InvalidOperationException("UGC item enumeration is not started.");
				}
				else if (state >= (int)parent.Count)
				{
					throw new InvalidOperationException("UGC item enumeration is ended.");
				}

				return parent.CreateItem(parent.SuccessfulQueryHandle, (uint)state);
			}
		}

		object IEnumerator.Current { get => Current; }

		public readonly void Dispose()
		{
			
		}

		public bool MoveNext()
		{
			if (state < (int)parent.Count - 1)
			{
				state += 1;
				return true;
			}
			else
			{
				state += 1;
				return false;
			}

		}

		public void Reset()
		{
			state = -1;
		}
	}

	public Enumerator GetEnumerator()
	{
		CheckNotDisposed();
		return new(this);
	}

	IEnumerator<UgcQueryResultItem> IEnumerable<UgcQueryResultItem>.GetEnumerator()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable<UgcQueryResultItem>)this).GetEnumerator();
	}
	#endregion

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			// if (disposing) // no managed disposable

			// 释放未托管的资源(未托管的对象)并重写终结器
			// steam native resources
			if (isGameServer)
			{
				SteamGameServerUGC.ReleaseQueryUGCRequest(SuccessfulQueryHandle);
			}
			else
			{
				SteamUGC.ReleaseQueryUGCRequest(SuccessfulQueryHandle);
			}

			// TODO: 将大型字段设置为 null
			disposedValue = true;
		}
	}

	~UgcQueryResult()
	{
		Dispose(false);
	}

	public void Dispose()
	{
		// 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}