using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using PDYS.Models;
using System.Collections.ObjectModel;

namespace PDYS.Converter
{
    public class ParameterConverter : IValueConverter
    {

        private ObservableCollection<Parameter> _ParameterList;
        public ObservableCollection<Parameter> ParameterList
        {
            get { return this._ParameterList = this._ParameterList ?? new ObservableCollection<Parameter>(); }
            private set { this._ParameterList = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is int)
            {
                if (!this.ParameterList.Any(item => item.Name == (string)parameter))
                    PDYSEntities.DataContext.ParameterSet.Where(item => item.Name == (string)parameter).ToList().ForEach(item => this.ParameterList.Add(item));

                var result = this.ParameterList.FirstOrDefault(item => item.Name == (string)parameter && item.Value == (int)value);
                if (result == null)
                    return string.Empty;
                else
                    return result.Text;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
