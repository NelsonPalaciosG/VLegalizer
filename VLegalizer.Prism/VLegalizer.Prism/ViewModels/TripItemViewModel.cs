using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using VLegalizer.Common.Helpers;
using VLegalizer.Common.Models;

namespace VLegalizer.Prism.ViewModels
{
    
    public class TripItemViewModel : TripResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectTripCommand;
        public TripItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }



        public DelegateCommand SelectTripCommand => _selectTripCommand ??
            (_selectTripCommand = new DelegateCommand(SelectTrip));

        private async void SelectTrip()
        {
            Settings.Trips = JsonConvert.SerializeObject(this);//serializar un objeto a un string
            await _navigationService.NavigateAsync("TripDetailsTabbedPage");
        }
    }
}
