﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CCD.Models.SRC
{
    /// <summary>
    /// Класс для конвертирования значений ProgressBar
    /// </summary>
    internal class ProgressToHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is double value &&
                values[1] is double maximum &&
                values[2] is double height)
            {
                if (maximum == 0) return 0;
                return (value / maximum) * height;
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
