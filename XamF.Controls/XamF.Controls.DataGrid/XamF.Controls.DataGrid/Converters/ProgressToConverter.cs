using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace XamF.Controls.DataGrid.Converters
{
    public class ProgressToConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var progressValue = double.Parse(value.ToString());
            var progressBarControl = ((Binding)parameter).Source as ProgressBar;
            //TODO: "/ 30" is business!
            progressBarControl.ProgressTo(progressValue / 30, 500, Easing.Linear);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
