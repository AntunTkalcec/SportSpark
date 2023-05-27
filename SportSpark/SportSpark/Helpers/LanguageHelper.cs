using System.ComponentModel;
using System.Resources;
using System.Globalization;

namespace SportSpark.Helpers
{
    public class LanguageHelper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ResourceManager manager = Resources.AppRes.ResourceManager;

        private LanguageHelper()
        {

        }

        static LanguageHelper instance;
        public static LanguageHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LanguageHelper();
                }
                return instance;
            }
        }

        public string GetString(string resourceName)
        {
            return manager.GetString(resourceName);
        }

        public string this[string key] => GetString(key);

        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
