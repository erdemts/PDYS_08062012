using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvvm.Messenger;
using System.Windows;
using System.IO;
using PDYS.Interfaces;
using PDYS.Services.ServiceParam;
using DevExpress.Xpf.Core;
using System.Windows.Threading;

namespace PDYS.Services
{
    public class DialogServiceReceiver : IDialogService
    {
        public DialogServiceReceiver()
        {
            MessageProvider.Register(this);
        }

        public static IDialogService Initialize()
        {
            return new DialogServiceReceiver();
        }


        [MessageProviderSinkAttribute("ShowInformationMessage", ParameterType = typeof(string))]
        public void ShowInformationMessage(string message)
        {
            DXMessageBox.Show(message, "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);
        }

        [MessageProviderSinkAttribute("ShowErrorMessage", ParameterType = typeof(string))]
        public void ShowErrorMessage(string message)
        {
            DXMessageBox.Show(message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
        }

        [MessageProviderSinkAttribute("ShowAlertMessage", ParameterType = typeof(string))]
        public void ShowAlertMessage(string message)
        {
            DXMessageBox.Show(message, "Hata", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.None);
        }

        

        [MessageProviderSinkAttribute("ConfirmMessage", ParameterType = typeof(ConfirmParam))]
        public void ConfirmMessage(ConfirmParam confirmparam)
        {
           MessageBoxResult result =  DXMessageBox.Show( confirmparam.Message, "Soru",MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.None);

            if (confirmparam.OnConfirmResult!=null)
                confirmparam.OnConfirmResult((ConfirmParam.ConfirmResult)result);
          
        }

        [MessageProviderSinkAttribute("OpenFileDialog", ParameterType = typeof(DialogFileParam))]
        public void OpenFileDialog(DialogFileParam fileparam)
        {
            if (fileparam == null)
                return;

            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = false;

            // Set filter for file extension and default file extension
            if (!string.IsNullOrEmpty(fileparam.FileFilter))
            {
                //dlg.DefaultExt = ".txt";
                dlg.Filter = fileparam.FileFilter;
            }

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                Stream filedata = dlg.OpenFile();

                byte[] buffer = new byte[filedata.Length];
                filedata.Read(buffer, 0, buffer.Length);

                if (fileparam.FileLoadedAction != null)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action<byte[]>(fileparam.FileLoadedAction), DispatcherPriority.Background, new object[] { buffer });
                }
                    
            }

           
        }


        

        [MessageProviderSinkAttribute("OpenLookupWindow", ParameterType = typeof(DialogWindowParam))]
        public void OpenLookupWindow(DialogWindowParam dialogParam)
        {
            if (dialogParam == null)
                return;

            LookupWindow editWin = new LookupWindow();
            editWin.DataContext = dialogParam.ModelView;
            editWin.Focus();
            bool? result = editWin.ShowDialog();

            if (dialogParam.OnClose != null)
                dialogParam.OnClose(result.Value);
        }


        [MessageProviderSinkAttribute("OpenWindow", ParameterType = typeof(DialogWindowParam))]
        public void OpenWindow(DialogWindowParam dialogParam)
        {
            if (dialogParam == null)
                return;

            PopupWindow editWin = new PopupWindow();
            editWin.DataContext = dialogParam.ModelView;
            editWin.Focus();
            bool? result = editWin.ShowDialog();

            
                

            if (dialogParam.OnClose != null)
                dialogParam.OnClose((result.HasValue) ? result.Value : false);
        }


        [MessageProviderSinkAttribute("OpenBaseWindow", ParameterType = typeof(DialogWindowParam))]
        public void OpenBaseWindow(DialogWindowParam dialogParam)
        {
            if (dialogParam == null)
                return;

            BaseWindowView baseWin = new BaseWindowView();
            baseWin.DataContext = dialogParam.ModelView;
            baseWin.Focus();
            bool? result = baseWin.ShowDialog();

            if (dialogParam.OnClose != null)
                dialogParam.OnClose(result.Value);
        }

        [MessageProviderSinkAttribute("CloseWindow", ParameterType = typeof(bool))]
        public void CloseWindow(bool dialogResult)
        {
            Window window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (window != null)
            {
                window.DialogResult = dialogResult;
                window.Close();
            }
        }


        [MessageProviderSinkAttribute("CloseApplication", ParameterType = typeof(int))]
        public void CloseApplication(int exitCode)
        {
            Application.Current.Shutdown(exitCode);
        }


        


    }
}
