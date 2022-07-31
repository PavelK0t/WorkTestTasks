public static class TestServer
{
	private static int count = 0;
	static ReaderWriterLockSlim rwls = new ReaderWriterLockSlim();

	/// <summary>
	/// Метод для чтения переменной count
	/// </summary>
	public static void GetCount() 
	{
		rwls.EnterReadLock();
		try
		{
			Console.WriteLine(count);
		}
		finally
		{
			rwls.ExitReadLock();
		}
	}

	/// <summary>
	/// Метод для записи в переменную count
	/// </summary>
	/// <param name="value"></param>
	public static void AddToCount(int value) 
	{
		rwls.EnterWriteLock();
		try
		{        
			count += value;                    
		}
		finally
		{
			rwls.ExitWriteLock();
		}
	}
}

/// <summary>
/// Класс с запускоим события с timeout
/// </summary>
public class AsyncCaller
{
	private EventHandler _handler;

	public AsyncCaller(EventHandler handler)
	{
		_handler = handler;
	}

	/// <summary>
	/// Реализованный метод Invoke с timeout
	/// </summary>
	public bool Invoke(int milliseconds, object sender, EventArgs args)
	{
		var task = Task.Factory.StartNew(() => _handler?.Invoke(sender, args));

		return task.Wait(milliseconds);
	}
}
