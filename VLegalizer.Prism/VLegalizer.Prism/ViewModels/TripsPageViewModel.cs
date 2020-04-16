using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VLegalizer.Common.Models;
using VLegalizer.Common.Models.Services;

namespace VLegalizer.Prism.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private TripResponse _trip;
        private bool _isRunning;

        public TripsPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Trips";
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public TripResponse Trips
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("trips"))
            {
                _trip = parameters.GetValue<TripResponse>("trips");
                

            }

        }

    }
}
