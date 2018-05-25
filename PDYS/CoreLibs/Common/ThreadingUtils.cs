using System;

namespace Common
{
	public static class ThreadingUtils
	{
		public static void RunOnUI(Action action)
		{
			if (!CurrentDispatcher.Instance.CheckAccess())
			{
				CurrentDispatcher.Instance.BeginInvoke(action);
			}
			else
			{
				action();
			}
		}
	}
}