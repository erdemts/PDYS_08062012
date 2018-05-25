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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PDYS
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(LoadingWindow_Loaded);
            this.Closing += new System.ComponentModel.CancelEventHandler(LoadingWindow_Closing);
        }

        bool isprogramaticclose = false;

        void LoadingWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isprogramaticclose)
                e.Cancel = true;
        }

        public Action method { get; set; }


        public void ProgramaticClose()
        {
            this.isprogramaticclose = true;
            this.Close();
        }

        void LoadingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke(method, DispatcherPriority.Background, null);
        }
    }
}
