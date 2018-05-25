using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using PDYS.Models;
using PDYS.Interfaces;
using PDYS.InfraStructure;
using System.Windows.Data;
using System.Collections;
using PDYS.Converter;

namespace PDYS.Helper
{
    public static class GlobalHandler
    {
        public static void SetEventListener()
        {
            //EventManager.RegisterClassHandler(typeof(DatePicker), DatePicker.LoadedEvent, new RoutedEventHandler(DatePicker_Loaded));
            EventManager.RegisterClassHandler(typeof(ComboBox), ComboBox.LostFocusEvent, new RoutedEventHandler(ComboBox_LostFocus));
            //EventManager.RegisterClassHandler(typeof(UserControl), UserControl.LoadedEvent, new RoutedEventHandler(VM_Loaded));
            //EventManager.RegisterClassHandler(typeof(Window), Window.LoadedEvent, new RoutedEventHandler(VM_Loaded));
            EventManager.RegisterClassHandler(typeof(DataGrid), DataGrid.LoadedEvent, new RoutedEventHandler(DataGrid_Loaded));
            EventManager.RegisterClassHandler(typeof(DataGrid), DataGrid.SelectionChangedEvent, new SelectionChangedEventHandler(DataGrid_SelectionChanged));

            
        }

        #region DataGrid

        static readonly SelectionModeConverter selectionmodeconverter = new SelectionModeConverter();

        static void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid datagrid = (DataGrid)sender;

            datagrid.SetBinding(DataGridDependencyProperty.SelectedItemsProperty,new Binding("SelectedItems"));
            datagrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding("Items"));
            datagrid.SetBinding(DataGrid.SelectedItemProperty, new Binding("SelectedItem") { Mode = BindingMode.TwoWay });
            datagrid.SetBinding(DataGrid.SelectionModeProperty, new Binding("IsMultiSelect") { Converter = selectionmodeconverter });
            
            
            IList items = datagrid.GetValue(DataGridDependencyProperty.SelectedItemsProperty) as IList;

            if (items != null)
                items.Clear();

            datagrid.SelectedItem = null;
        }

        static void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid datagrid = (DataGrid)sender;

            IList items = datagrid.GetValue(DataGridDependencyProperty.SelectedItemsProperty) as IList;

            if (items == null)
                return;

            foreach (var additem in e.AddedItems)
            {
                items.Add(additem);
            }

            foreach (var removeitem in e.RemovedItems)
            {
                items.Remove(removeitem);
            } 
        }

        #endregion 


        #region Loaded

        static void VM_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement window = sender as FrameworkElement;
            
            if (window != null)
            {
                ViewModelBase vm = window.DataContext as ViewModelBase;

                if (vm != null)
                    vm.OnLoadCommand.Execute();
            }

        }

        #endregion

        #region Datepicker

        static void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var dp = sender as DatePicker;
            if (dp == null) return;

            var tb = GetChildOfType<DatePickerTextBox>(dp);
            if (tb == null) return;

            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null) return;

            wm.Content = null;
        }

        #endregion

        #region Combobox

        static void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null || (comboBox != null && !comboBox.IsEditable)) return;

            TextBox textBox = comboBox.Template.FindName("PART_EditableTextBox", comboBox) as TextBox;
            
            if (comboBox.SelectedItem == null)
            {
                textBox.Text = null;
            }
            else if (string.IsNullOrEmpty(textBox.Text))
            {
                comboBox.SelectedItem = null;
            }
            else if (comboBox.SelectedItem is IDataItem)
            {
                var selectItem = (IDataItem)comboBox.SelectedItem;
                textBox.Text = selectItem.DisplayName;
            }
        }

        #endregion

        #region Helper

        public static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        #endregion
    }
}
