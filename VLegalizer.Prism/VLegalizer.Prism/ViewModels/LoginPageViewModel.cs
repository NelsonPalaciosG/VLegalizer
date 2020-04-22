using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VLegalizer.Common.Helpers;
using VLegalizer.Common.Models;
using VLegalizer.Common.Models.Services;
using VLegalizer.Prism.Helpers;

namespace VLegalizer.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;

        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Login;
            IsEnabled = true;
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));

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

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Enter_email, Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Enter_password, Languages.Accept);
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.GetTokenAsync(url, "Account", "/CreateToken", request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error2, Languages.Accept);
                Password = string.Empty;
                return;

            }

            var token = (TokenResponse)response.Result;
            var response2 = await _apiService.GetTripByEmailAsync(url, "api", "/Trips/GetTripByEmail", "bearer", token.Token, Email);

            if (!response2.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error3, Languages.Accept);
                Password = string.Empty;
                return;
            }

            var trips = (EmployeeResponse)response2.Result;
            Settings.Employee = JsonConvert.SerializeObject(trips);
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsLogin = true;


            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync("/VLegalizerMasterDetailPage/NavigationPage/TripsPage");
            Password = string.Empty;

        }

        private void RegisterAsync()
        {
        }
    }
}