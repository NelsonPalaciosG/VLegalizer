using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Common.Helpers;
using VLegalizer.Common.Models;
using VLegalizer.Common.Models.Services;
using VLegalizer.Prism.Helpers;

namespace VLegalizer.Prism.ViewModels
{
    public class AccountPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private EmployeeResponse _employee;
        private List<TripItemViewModel> _trips;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _saveCommand;


        public AccountPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Account;
            Employee = JsonConvert.DeserializeObject<EmployeeResponse>(Settings.Employee);
            IsEnabled = true;

        }
       
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        public EmployeeResponse Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }

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

        private async void SaveAsync()
        {
            var isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            EmployeeRequest employeeRequest = new EmployeeRequest
            {

                Address = Employee.Address,
                Document = Employee.Document,
                FirstName = Employee.FirstName,
                LastName = Employee.LastName,
                Email = Employee.Email,
                Password = "123456",
                FixedPhone = Employee.FixedPhone,
                CellPhone = Employee.CellPhone
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.PutAsync(
                url,
                "api",
                "/Account",
                employeeRequest,
                "bearer",
                token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            Settings.Employee = JsonConvert.SerializeObject(Employee);

            await App.Current.MainPage.DisplayAlert(
                "Ok",
                Languages.UpdateSucess,
                Languages.Accept);


        }


        private async Task<bool> ValidateDataAsync()
          {
              if (string.IsNullOrEmpty(Employee.Document))
              {
                  await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Document2, Languages.Accept);
                  return false;
              }

              if (string.IsNullOrEmpty(Employee.FirstName))
              {
                  await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstName2, Languages.Accept);
                  return false;
              }

              if (string.IsNullOrEmpty(Employee.LastName))
              {
                  await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastName2, Languages.Accept);
                  return false;
              }

            if (string.IsNullOrEmpty(Employee.Email) || !RegexHelper.IsValidEmail(Employee.Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Enter_email, Languages.Accept);
                return false;
            }


            if (string.IsNullOrEmpty(Employee.Address))
              {
                  await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Address2, Languages.Accept);
                  return false;
              }

              if (string.IsNullOrEmpty(Employee.FixedPhone))
              {
                  await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FixedPhone2, Languages.Accept);
                  return false;
              }

              if (string.IsNullOrEmpty(Employee.CellPhone))
              {
                  await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FixedPhone2, Languages.Accept);
                  return false;
              }


              return true;
          }
    }
}
