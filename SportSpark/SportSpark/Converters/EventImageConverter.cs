using SportSparkCoreSharedLibrary.DTOs;
using System.Globalization;

namespace SportSpark.Converters
{
    public class EventImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EventDTO val)
            {
                switch (val.EventType.Name)
                {
                    case "Football":
                        return ImageSource.FromFile("Images/football.jpg");
                    case "Chess":
                        return ImageSource.FromFile("Images/chess.jpg");
                    case "Baseball":
                        return ImageSource.FromFile("Images/baseball.jpg");
                    case "Other":
                        return ImageSource.FromFile("Images/other_sport.png");
                }
            }
            return ImageSource.FromFile("Images/other_sport.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
