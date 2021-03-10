using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UpaProject.Infrastracture.Converters
{
    public class StringBuilderToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)=> ((StringBuilder)value).ToString();
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)=> DependencyProperty.UnsetValue;
        
    }
}
