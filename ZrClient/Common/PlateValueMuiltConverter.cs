using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ZrClient.Common
{
    public class PlateValueMuiltConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double pv1 = 0.0;
            double pv2 = 0.0;
            if (values != null && values.Length == 2)
            {
                double.TryParse(values[0].ToString(), out pv1);
                double.TryParse(values[1].ToString(), out pv2);
            }


            return 180;// (int)((pv1 - pv2) * 2.7) + 135;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
