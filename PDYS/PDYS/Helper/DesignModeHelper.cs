using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace PDYS.Helper
{
    public static class DesignModeHelper
    {
        public static readonly DependencyObject obj= new DependencyObject();

        public static bool IsDesignMode
        {
            get
            {
                bool result = DesignerProperties.GetIsInDesignMode(obj);
                return result;
            }
        }
    }
}
