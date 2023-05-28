using SportSpark.Helpers;
using System.Globalization;

namespace SportSpark.Converters
{
    public class ProfileFriendBtnConverter : IValueConverter
    {
        public LanguageHelper Language
        {
            get
            {
                return LanguageHelper.Instance;
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                if ((bool)value)
                {
                    return Language["EditProfile"];
                }
                else
                {
                    return Language["AddFriend"];
                }
            }
            return "Localization error";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
