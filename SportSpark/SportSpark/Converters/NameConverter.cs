using SportSparkCoreSharedLibrary.DTOs;
using System.Globalization;

namespace SportSpark.Converters
{
    public class NameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                UserDTO val = value as UserDTO;
                if (culture.Name == "en-US")
                {
                    return val.FirstName + "'s profile";
                }
                else
                {
                    return "Profil - " + val.FirstName + " " + val.LastName;
                }
            }
            else
            {
                if (culture.Name == "en-US")
                {
                    return "User profile";
                }
                else
                {
                    return "Korisnički profil";
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
