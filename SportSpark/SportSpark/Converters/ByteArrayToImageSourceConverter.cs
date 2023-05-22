using System.Globalization;

namespace SportSpark.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] imageData)
            {
                if (imageData.Length > 0)
                {
                    return ImageSource.FromStream(() => new MemoryStream(imageData));
                }
                return ImageSource.FromFile("Images/anonymous_person.jpg");
            }

            return ImageSource.FromFile("Images/anonymous_person.jpg");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
