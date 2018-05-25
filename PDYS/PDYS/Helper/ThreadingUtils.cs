using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Threading;
using System.Windows;

namespace PDYS.Helper
{
    public static class ThreadingUtils
    {
        public static void RunOnUI(Action action)
        {
            CurrentDispatcher.BeginInvoke(action, null);
        }

        public static void RunOnUI<T>(Action<T> action, T obj)
        {
            CurrentDispatcher.BeginInvoke(action, DispatcherPriority.Loaded, obj);
            //return;
            //if (!CurrentDispatcher.CheckAccess())
            //{
                
            //}
            //else
            //{
            //    action.BeginInvoke(obj, null, null);
            //}
        }

        private static Dispatcher currentDispatcher;
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		private static Dispatcher CurrentDispatcher
		{
			get
			{
				if (currentDispatcher == null)
				{
                    if (Application.Current != null)
                        currentDispatcher = Application.Current.Dispatcher;
                    else
                        currentDispatcher = Dispatcher.CurrentDispatcher;
				}

				return currentDispatcher;
			}
			set { currentDispatcher = value; }
		}
    }
}
