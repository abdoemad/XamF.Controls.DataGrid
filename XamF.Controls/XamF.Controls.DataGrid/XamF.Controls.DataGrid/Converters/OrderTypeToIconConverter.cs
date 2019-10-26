using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using XamF.Controls.DataGrid.DataGridControl.Core.Enums;

namespace XamF.Controls.DataGrid.Converters
{
    public class OrderTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconSource = string.Empty;
            if (value is OrderType order)
            {
                switch (order)
                {
                    case OrderType.Ascending:
                        iconSource = "iconDownArrow.png";
                        break;

                    case OrderType.Decscending:
                        iconSource = "iconUpArrow.png";
                        break;
                }
            }
            return iconSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
