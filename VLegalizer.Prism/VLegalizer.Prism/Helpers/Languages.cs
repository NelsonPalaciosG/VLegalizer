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

        public static string Account => Resource.Account;

        public static string Add_details => Resource.Add_details;

        public static string Add_trip => Resource.Add_trip;

        public static string Email => Resource.Email;

        public static string Email2 => Resource.Email2;

        public static string Email_exist => Resource.Email_exist;

        public static string Enter_email => Resource.Enter_email;

        public static string Enter_password => Resource.Enter_password;

        public static string Error => Resource.Error;

        public static string Error2 => Resource.Error2;

        public static string Error3 => Resource.Error3;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string Loading => Resource.Loading;

        public static string Login => Resource.Login;

        public static string Logout => Resource.Logout;

        public static string Password => Resource.Password;

        public static string Password2 => Resource.Password2;

        public static string Register => Resource.Register;

        public static string Trips => Resource.Trips;

        public static string Trip_details => Resource.Trip_details;

        public static string Register2 => Resource.Register2;

        public static string Address => Resource.Address;

        public static string Address2 => Resource.Address2;

        public static string CellPhone => Resource.CellPhone;

        public static string Document => Resource.Document;

        public static string Document2 => Resource.Document2;

        public static string FirstName => Resource.FirstName;

        public static string FirstName2 => Resource.FirstName2;

        public static string FixedPhone => Resource.FixedPhone;

        public static string FixedPhone2 => Resource.FixedPhone2;

        public static string LastName => Resource.LastName;

        public static string LastName2 => Resource.LastName2;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordConfirm2 => Resource.PasswordConfirm2;

        public static string Registering => Resource.Registering;

        public static string PasswordLength => Resource.PasswordLength;

        public static string PasswordnoMatch => Resource.PasswordnoMatch;

        public static string PasswordRecover => Resource.PasswordRecover;

        public static string Remembering => Resource.Remembering;

        public static string ValidEmail => Resource.ValidEmail;

        public static string Remember => Resource.Remember;

        public static string Updating => Resource.Updating;

        public static string Update => Resource.Update;

        public static string ChangePassword => Resource.ChangePassword;

        public static string UpdateSucess => Resource.UpdateSucess;

        public static string UserdontExist => Resource.UserdontExist;

        public static string TripAdded => Resource.TripAdded;

        public static string StartDate => Resource.StartDate;

        public static string EndDate => Resource.EndDate;

        public static string City => Resource.City;
    }

}
