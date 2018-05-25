using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvvm;

namespace PDYS.Interfaces
{
    public delegate void ClosedPopup(IEnumerable<IDataItem> Selectedltems);

    public interface IPopup
    {

        void OpenSelectedRecord(IDataItem SelectedDataModel);
        void OpenPopup();

        event ClosedPopup Closed;
        event Action Clean;

        RelayCommand AcceptCommand { get; }
        RelayCommand CancelCommand { get; }
        RelayCommand CleanCommand { get; }
    }
}
