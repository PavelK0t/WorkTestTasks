public static class TestServer
{
	private static int count = 0;
	static ReaderWriterLockSlim rwls = new ReaderWriterLockSlim();

	/// <summary>
	/// Метод для чтения переменной count
	/// </summary>
	public static void GetCount() 
	{
		try
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
		catch (ApplicationException)
		{}
	}

	/// <summary>
	/// Метод для записи в переменную count
	/// </summary>
	/// <param name="value"></param>
	public static void AddToCount(int value) 
	{
		try
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
		catch (ApplicationException)
		{}
	}
}

public class AsyncCaller
{
	private EventHandler _handler;

	public AsyncCaller(EventHandler handler)
	{
		_handler = handler;
	}

	public bool Invoke(int milliseconds, object sender, EventArgs args)
	{
		var task = Task.Factory.StartNew(() => _handler.Invoke(sender, args));

		return task.Wait(milliseconds);
	}
}