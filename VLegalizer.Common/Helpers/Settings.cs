using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace VLegalizer.Common.Helpers
{

    public static class Settings
    {
        private const string _token = "token";
        private const string _employee = "employee";
        private const string _isLogin= "isLogin";
        private const string _trips = "trips";
        private const string _isRemembered = "IsRemembered";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;
    
        private static ISettings AppSettings => CrossSettings.Current;

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }
        public static string Employee
        {
            get => AppSettings.GetValueOrDefault(_employee, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_employee, value);
        }

        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }
        public static string Trips
        {
            get => AppSettings.GetValueOrDefault(_trips, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_trips, value);
        }

        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }

    }

}
