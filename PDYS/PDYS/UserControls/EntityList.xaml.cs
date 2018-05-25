using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PDYS.UserControls
{
    /// <summary>
    /// Interaction logic for EntityList.xaml
    /// </summary>
    public partial class EntityList : UserControl
    {
        public EntityList()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EntityList_Loaded);
        }

        void EntityList_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        #region GridContent

        public static readonly DependencyProperty DataGridContentProperty = DependencyProperty.Register("GridContent", typeof(object), typeof(EntityList));

        public object GridContent
        {
            get { return GetValue(DataGridContentProperty); }
            set { SetValue(DataGridContentProperty, value); }
        }

        #endregion

       

    }
}
