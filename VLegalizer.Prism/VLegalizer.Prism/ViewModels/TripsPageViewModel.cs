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

namespace VLegalizer.Prism.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private EmployeeResponse _employee;
        private List<TripItemViewModel> _trips;
        private bool _isRunning;

        public TripsPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Trips";
            LoadTrips();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        private void LoadTrips()
        {
            _employee = JsonConvert.DeserializeObject<EmployeeResponse>(Settings.Employee);
            Trips = new List<TripItemViewModel>(_employee.Trips.Select(t => new TripItemViewModel(_navigationService)
            {
                City = t.City,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                TotalAmount = t.TotalAmount

            }).ToList());;

        }

    }
}
