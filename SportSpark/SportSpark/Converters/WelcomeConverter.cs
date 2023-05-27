using SportSparkCoreSharedLibrary.DTOs;
using System.Globalization;

namespace SportSpark.Converters
{
    public class WelcomeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                if (culture.Name == "en-US")
                {
                    return "Welcome, " + (value as UserDTO).FirstName;
                }
                else
                {
                    return "Dobrodošli, " + (value as UserDTO).FirstName;
                }
            }
            return "SportSpark";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
