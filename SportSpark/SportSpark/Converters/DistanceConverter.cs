using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Windows.Input;

namespace SportSpark.Converters
{
    public class DistanceConverter : IValueConverter
    {
        private Location _location;
        private ICommand GetLocation { get; }
        public DistanceConverter()
        {
            GetLocation = new AsyncRelayCommand(async () =>
            {
                _location = await Geolocation.Default.GetLastKnownLocationAsync();
            });
            GetLocation.Execute(null);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double[] val = (double[])value;
                if (val[0] != 0 && val[1] != 0)
                {
                    return ((int)_location.CalculateDistance(new Location(val[0], val[1]), DistanceUnits.Kilometers)).ToString() + "km away";
                }
                return "Unknown distance";
            }
            catch (Exception ex)
            {
                return "Unknown distance";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
