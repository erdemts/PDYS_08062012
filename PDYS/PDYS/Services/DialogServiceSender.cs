using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvvm.Messenger;
using PDYS.Interfaces;
using PDYS.Services.ServiceParam;

namespace PDYS.Services
{
    public class DialogServiceSender : IDialogService
    {

        public void ShowInformationMessage(string message)
        {
            MessageProvider.NotifyColleagues<string>("ShowInformationMessage", message);
        }

        public void ShowErrorMessage(string message)
        {
            MessageProvider.NotifyColleagues<string>("ShowErrorMessage", message);
        }

        public void ShowAlertMessage(string message)
        {
            MessageProvider.NotifyColleagues<string>("ShowAlertMessage", message);
        }

        public void ConfirmMessage(ConfirmParam confirmparam)
        {
            MessageProvider.NotifyColleagues<ConfirmParam>("ConfirmMessage", confirmparam);
        }

        public void OpenWindow(DialogWindowParam dialogParam)
        {
            MessageProvider.NotifyColleagues<DialogWindowParam>("OpenWindow", dialogParam);
        }

        public void OpenLookupWindow(DialogWindowParam dialogParam)
        {
            MessageProvider.NotifyColleagues<DialogWindowParam>("OpenLookupWindow", dialogParam);
        }

        public void OpenBaseWindow(DialogWindowParam dialogParam)
        {
            MessageProvider.NotifyColleagues<DialogWindowParam>("OpenBaseWindow", dialogParam);
        }

        public void CloseWindow(bool dialogResult)
        {
            MessageProvider.NotifyColleagues<bool>("CloseWindow", dialogResult);
        }

        public void OpenFileDialog(DialogFileParam fileparam)
        {
            MessageProvider.NotifyColleagues<DialogFileParam>("OpenFileDialog", fileparam);
        }

        public void CloseApplication(int exitCode)
        {
            MessageProvider.NotifyColleagues<int>("CloseApplication", exitCode);
        }


        

       
    }
}
