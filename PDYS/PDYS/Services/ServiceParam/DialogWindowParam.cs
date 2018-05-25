using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using System.Windows;

namespace PDYS.Services.ServiceParam
{

    public class DialogWindowParam
    {
        public ViewModelBase ModelView { get; set; }
        public Action<bool> OnClose { get; set; }
    }
}
