using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Editors;
using System.Windows;
using PDYS.Interfaces;
using PDYS.UserControls;
using System.Windows.Data;

namespace PDYS.UserControls
{
    public class ExtendLookup : ButtonEdit
    {

        #region Fields

        // Dependency property backing variables
        public static readonly DependencyProperty LookupViewModelProperty;
        public static readonly DependencyProperty SelectedDataModelProperty;

        #endregion

        public ExtendLookup()
        {
            this.IsTextEditable = false;
            this.DefaultButtonClick += new RoutedEventHandler(Button_Click);
        }

        static ExtendLookup()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonEdit), new FrameworkPropertyMetadata(typeof(ButtonEdit)));

            LookupViewModelProperty = DependencyProperty.RegisterAttached("LookupViewModel", typeof(IPopup), typeof(ExtendLookup), new UIPropertyMetadata(LookupViewModelOnChange));
            SelectedDataModelProperty = DependencyProperty.Register("SelectedDataModel", typeof(IDataItem), typeof(ExtendLookup), new PropertyMetadata(SelectedDataModelOnChange));
        }
      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.LookupViewModel != null)
                this.LookupViewModel.OpenPopup();
        }


        #region Properties


        public IPopup LookupViewModel
        {
            get { return (IPopup)GetValue(LookupViewModelProperty); }
            set { SetValue(LookupViewModelProperty, value); }
        }

        public IDataItem SelectedDataModel
        {
            get { return (IDataItem)GetValue(SelectedDataModelProperty); }
            set { SetValue(SelectedDataModelProperty, value); }
        }


        #endregion


     

        #region CallBack

        static void SelectedDataModelOnChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IDataItem dataModel = e.NewValue as IDataItem;
            ExtendLookup btnEdit = (ExtendLookup)d;

            btnEdit.Text = (dataModel == null) ? string.Empty : dataModel.DisplayName;
        }


        static void LookupViewModelOnChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExtendLookup extendlookup = d as ExtendLookup;

            if (extendlookup == null)
                return;

            IPopup oldLkpVM = e.OldValue as IPopup;
            IPopup newLkpVM = e.NewValue as IPopup;


            if (oldLkpVM != null)
            {
                oldLkpVM.Closed -= new ClosedPopup(extendlookup.LookupWindow_Closed);
                oldLkpVM.Clean -= new Action(extendlookup.LookupWindow_Clean);
            }
            if (newLkpVM != null)
            {
                newLkpVM.Closed += new ClosedPopup(extendlookup.LookupWindow_Closed);
                newLkpVM.Clean += new Action(extendlookup.LookupWindow_Clean);
            }
        }

        void LookupWindow_Clean()
        {
            this.SelectedDataModel = null;
        }

        void LookupWindow_Closed(IEnumerable<IDataItem> selecteditems)
        {
            if (selecteditems != null)
                this.SelectedDataModel = selecteditems.LastOrDefault();
            else
                this.SelectedDataModel = null;
        }

        #endregion
    }
}
