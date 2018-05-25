using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Services;
using PDYS.Services.ServiceParam;

namespace PDYS.Interfaces
{
    public interface IDialogService
    {

        void ShowInformationMessage(string message);
        void ShowErrorMessage(string message);
        void ShowAlertMessage(string message);
        void ConfirmMessage(ConfirmParam confirmparam);
        void OpenWindow(DialogWindowParam windowparam);
        void OpenBaseWindow(DialogWindowParam dialogParam);
        void OpenLookupWindow(DialogWindowParam windowparam);
        void OpenFileDialog(DialogFileParam fileparam);
        void CloseWindow(bool dialogResult);
        void CloseApplication(int exitCode);


    }
}
