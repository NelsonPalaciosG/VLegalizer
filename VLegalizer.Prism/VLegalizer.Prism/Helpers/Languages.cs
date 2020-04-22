using System.Globalization;
using VLegalizer.Common.Interfaces;
using VLegalizer.Prism.Resources;
using Xamarin.Forms;


namespace VLegalizer.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string Error2 => Resource.Error2;

        public static string Enter_email => Resource.Enter_email;

        public static string Enter_password => Resource.Enter_password;

        public static string Error3 => Resource.Error3;

        public static string Account => Resource.Account;

        public static string Trips => Resource.Trips;

        public static string Trip_details => Resource.Trip_details;

        public static string Password => Resource.Password;

        public static string Register => Resource.Register;

        public static string Login => Resource.Login;

        public static string Logout => Resource.Logout;

        public static string Email => Resource.Email;

        public static string Add_trip => Resource.Add_trip;

        public static string Add_details => Resource.Add_details;




    }

}
