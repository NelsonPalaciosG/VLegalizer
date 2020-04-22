using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VLegalizer.Prism.Helpers;

namespace VLegalizer.Prism.ViewModels
{
    public class TripDetailsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public TripDetailsPageViewModel(
            INavigationService navigationService
            ) : base(navigationService)
        {
            _navigationService = navigationService;

            Title = Languages.Trip_details;


        }
    }
}
