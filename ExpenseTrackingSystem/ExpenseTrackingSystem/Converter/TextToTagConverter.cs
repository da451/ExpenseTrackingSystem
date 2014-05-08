using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using ExpenseTrackingSystem.Model;

namespace ExpenseTrackingSystem.Converter
{
    public class TextToTagConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TagModel tag = value as TagModel;

            if (tag == null)
            {
                return "";
            }

            return tag.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = value as string;

            if (text == null)
            {
                text = string.Empty;
            }

            return new TagModel()
            {
                Name = text
            };
        }

        #endregion
    }
}
