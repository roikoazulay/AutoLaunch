using System;
using System.Globalization;
using System.Windows.Data;
using AutomationCommon;

namespace AutomationClient
{
    public class RadioBoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                               object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string checkValue = value.ToString();
            string targetValue = parameter.ToString();
            return checkValue.Equals(targetValue,
                     StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            bool useValue = (bool)value;
            string targetValue = parameter.ToString();
            if (useValue)
                return Enum.Parse(targetType, targetValue);

            return Enums.OnFailerAction.non;//this value must be ignored in the set function other wise it will always set the value to non
        }
    }
}