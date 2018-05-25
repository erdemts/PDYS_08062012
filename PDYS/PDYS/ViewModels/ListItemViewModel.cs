using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Interfaces;

namespace PDYS.ViewModels
{
    public class ListItemViewModel : ViewModelBase, IDataItem
    {
        public ListItemViewModel(int ID, string DisplayName)
        {
            this.ID = ID;
            this.DisplayName = DisplayName;
        }

        public ListItemViewModel()
        { 
        }

        public int ID { get; set; }
        public string DisplayName { get; set; }
    }
}
