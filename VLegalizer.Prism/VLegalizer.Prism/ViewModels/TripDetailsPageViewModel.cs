using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.ObjectModel;
using VLegalizer.Common.Helpers;
using VLegalizer.Common.Models;
using VLegalizer.Common.Models.Services;
using VLegalizer.Prism.Helpers;

namespace VLegalizer.Prism.ViewModels
{
    public class TripDetailsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private TripResponse _trips;
        private ObservableCollection<TripDetailResponse> _tripDetails;

        public TripDetailsPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Trips = JsonConvert.DeserializeObject<TripResponse>(Settings.Trips);
            TripDetails = new ObservableCollection<TripDetailResponse>(Trips.TripDetails);
            Title = Languages.Trip_details;
        }

        public TripResponse Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        public ObservableCollection<TripDetailResponse> TripDetails
        {
            get => _tripDetails;
            set => SetProperty(ref _tripDetails, value);
        }
    }
}
