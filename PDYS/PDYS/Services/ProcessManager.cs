using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;
using DevExpress.Xpf.Core;

namespace PDYS.Services
{
    public class ProcessManager
    {
        public static void Execute(string processName, Delegate method, params object[] args)
        {
            LoadingWindow loading = new LoadingWindow();
            loading.Title = processName;
            loading.method = () =>
            {
                try
                {
                    if (method != null)
                        method.DynamicInvoke(args);
                }
                catch (Exception e)
                {
                    DXMessageBox.Show(e.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
                }
                finally
                {
                    loading.ProgramaticClose();
                }
            };
            
            loading.ShowDialog();
        }
    }
}
