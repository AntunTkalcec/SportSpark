using SportSparkCoreSharedLibrary.DTOs;
using System.Globalization;

namespace SportSpark.Converters
{
    public class EventCountConverterAlt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                if (culture.Name == "en-US")
                {
                    return (value as UserDTO).Events.Count + " events";
                }
                else
                {
                    return (value as UserDTO).Events.Count + " događaja";
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
