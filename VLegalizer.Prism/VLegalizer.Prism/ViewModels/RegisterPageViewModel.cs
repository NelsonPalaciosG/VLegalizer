using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using VLegalizer.Common.Helpers;
using VLegalizer.Common.Models;
using VLegalizer.Common.Models.Services;
using VLegalizer.Prism.Helpers;

namespace VLegalizer.Prism.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _registerCommand;

        public RegisterPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Register2;
            IsEnabled = true;
        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(Register));

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string FixedPhone { get; set; }

        public string CellPhone { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void Register()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            var request = new EmployeeRequest
            {
                Address = Address,
                Document = Document,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                FixedPhone = FixedPhone,
                CellPhone = CellPhone
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.RegisterUserAsync(
                url,
                "/api",
                "/Account",
                request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok", response.Message, Languages.Accept);
            await _navigationService.GoBackAsync();


        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Document))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Document2, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstName2, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastName2, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Address))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Address2, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Email) || !RegexHelper.IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Enter_email, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(FixedPhone))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FixedPhone2, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(CellPhone))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FixedPhone2, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Password2, Languages.Accept);
                return false;
            }

            if (Password.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordLength, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordConfirm2, Languages.Accept);
                return false;
            }

            if (!Password.Equals(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordnoMatch, Languages.Accept);
                return false;
            }

            return true;
        }


      

     
    }


}


