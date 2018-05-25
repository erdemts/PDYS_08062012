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
using PDYS.Models;
using PDYS.InfraStructure;
using PDYS.Interfaces;
using System.ComponentModel;

namespace PDYS.UserControls
{
    /// <summary>
    /// Interaction logic for LookupView.xaml
    /// </summary>
    public partial class LookupControl : UserControl
    {
        public LookupControl()
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(LookupControl_KeyUp);

        }

        void LookupControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && this.LookupViewModel != null)
            {
                this.LookupViewModel.CleanCommand.Execute();
            }
            else if (e.Key == Key.Enter && this.LookupViewModel != null)
            {
                this.LookupViewModel.OpenPopup();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.LookupViewModel != null)
                this.LookupViewModel.OpenPopup();
        }


        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (this.LookupViewModel != null && this.SelectedDataModel != null)
                this.LookupViewModel.OpenSelectedRecord(this.SelectedDataModel);
        }

        #region SelectedItem

        public static readonly DependencyProperty SelectedDataModelProperty = DependencyProperty.Register("SelectedDataModel", typeof(IDataItem), typeof(LookupControl));

        public IDataItem SelectedDataModel
        {
            get { return (IDataItem)GetValue(SelectedDataModelProperty); }
            set { SetValue(SelectedDataModelProperty, value); }
        }



        #endregion


        


        #region LookupViewModel

        public static readonly DependencyProperty LookupViewModelProperty = DependencyProperty.Register("LookupViewModel", typeof(IPopup), typeof(LookupControl), new PropertyMetadata() { PropertyChangedCallback = new PropertyChangedCallback(LookupViewModelOnChange) });

        public IPopup LookupViewModel
        {
            get { return (IPopup)GetValue(LookupViewModelProperty); }
            set { SetValue(LookupViewModelProperty, value); }
        }

        static void LookupViewModelOnChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LookupControl lkp = (LookupControl)d;
            
            IPopup oldLkpVM = e.OldValue as IPopup;
            IPopup newLkpVM = e.NewValue as IPopup;


            if (oldLkpVM != null)
            {
                oldLkpVM.Closed -= new ClosedPopup(lkp.LookupWindow_Closed);
            }
            if (newLkpVM != null)
            {
                newLkpVM.Closed += new ClosedPopup(lkp.LookupWindow_Closed);
            }
        }

        void LookupWindow_Closed(IEnumerable<IDataItem> selecteditems)
        {
            if (selecteditems != null)
                this.SelectedDataModel = selecteditems.LastOrDefault();
            else
                this.SelectedDataModel = null;
        }

       
        #endregion

        private void lkpMainCtrl_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            var error = Validation.GetErrors(this);
            if (error.Any())
            {
                
                
                //e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default;
                e.ErrorContent = error[0].ErrorContent;
                e.IsValid = false;
                e.Handled = true;

                
            }
        }

       




       
    }
}
