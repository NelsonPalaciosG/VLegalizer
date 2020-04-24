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
    public class AddTripPageViewModel :ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private EmployeeResponse _employee;
        private TripResponse _trip;
        private List<TripItemViewModel> _trips;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _saveCommand;

        public AddTripPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            _navigationService = navigationService;
            Title = Languages.Add_trip;
            IsEnabled = true;
            LoadTrips();

        }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string City { get; set; }

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

        public EmployeeResponse Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }

        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        private void LoadTrips()
        {
            _employee = JsonConvert.DeserializeObject<EmployeeResponse>(Settings.Employee);
            Trips = new List<TripItemViewModel>(_employee.Trips.Select(t => new TripItemViewModel(_navigationService)
            {
                City = t.City,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                TotalAmount = t.TripDetails.Sum(td => td.Amount),
                TripDetails = t.TripDetails

            }).ToList()); ;

        }
  

        private async void SaveAsync()
        {

            IsRunning = true;
            IsEnabled = false;

            _trip = JsonConvert.DeserializeObject<TripResponse>(Settings.Trips);
            TripRequest tripRequest = new TripRequest
            {
                StartDate = StartDate,
                EndDate = EndDate,
                City = City,
                EmployeeId = new Guid(Trip.Employee.EmployeeId)
            };



            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.RegisterTripAsync(url, "/api", "/Trips/PostTrips", tripRequest, "bearer", token.Token);



            IsRunning = false;
            IsEnabled = true;



            if (response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }



            await App.Current.MainPage.DisplayAlert("Ok", Languages.TripAdded, Languages.Accept);
            await _navigationService.NavigateAsync($"/VLegalizerMasterDetailPage/NavigationPage/TripsPage");
        }


    }
}
