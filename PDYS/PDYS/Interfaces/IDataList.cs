using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvvm;
using System.Collections.ObjectModel;

namespace PDYS.Interfaces
{
    public interface IDataList
    {
        IDataItem SelectedItem { get; set; }
        IEnumerable<IDataItem> SelectedItems { get; }
        IEnumerable<IDataItem> Items { get; }

        RelayCommand MouseDoubleClickCommand { get; set; }

        void OpenRecord(IDataItem SelectedRecord);

        #region Command Visibility

        bool IsMultiSelect { get; set; }

        bool IsOpenCommand {get;set;}
        bool IsNewCommand {get;set;}
        bool IsAppendCommand {get;set;}
        bool IsDeleteCommand {get;set;}

        #endregion
        
    }
}
