using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Data;

namespace PDYS.Helper
{
    public static class DataGridDependencyProperty
    {
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.RegisterAttached("SelectedItems", typeof(object), typeof(DataGridDependencyProperty), new PropertyMetadata(OnSelectedItemsChanged));

        public static object GetSelectedItems(DependencyObject d)
        {
            return d.GetValue(SelectedItemsProperty);
        }


        public static void SetSelectedItems(DependencyObject d, object value)
        {
            d.SetValue(SelectedItemsProperty, value);
        }


        

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            //DataGrid datagrid = d as DataGrid;
            //if (datagrid == null)
            //    return;

            //if (e.OldValue != null)
            //{
            //    datagrid.SelectionChanged -= new System.Windows.Controls.SelectionChangedEventHandler(DataGridDependencyProperty_SelectionChanged);
            //    datagrid.Loaded -= new RoutedEventHandler(datagrid_Loaded);
            //}
            //if (e.NewValue != null)
            //{
            //    datagrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(DataGridDependencyProperty_SelectionChanged);
            //    datagrid.Loaded += new RoutedEventHandler(datagrid_Loaded);
            //}

        }

        static void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            //IList items = GetSelectedItems((DependencyObject)sender) as IList;
            //DataGrid datagrid = (DataGrid)sender;

            //datagrid.SelectedItem = null;

            //if (items != null)
            //    items.Clear();
        }

        static void DataGridDependencyProperty_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //IList items = GetSelectedItems((DependencyObject)sender) as IList;

            //if (items == null)
            //    return;

            //foreach (var additem in e.AddedItems)
            //{
            //    items.Add(additem);
            //}

            //foreach (var removeitem in e.RemovedItems)
            //{
            //    items.Remove(removeitem);
            //} 

            
        }
   
    }
}
