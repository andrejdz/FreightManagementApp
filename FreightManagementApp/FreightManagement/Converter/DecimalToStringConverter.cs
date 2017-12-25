﻿using System;
using System.Globalization;
using System.Windows.Data;
using static System.Convert;

namespace FreightManagement.Converter
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ToDecimal(value);
        }
    }
}
