using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VLegalizer.Common.Models.Services;
using VLegalizer.Prism.Helpers;

namespace VLegalizer.Prism.ViewModels
{
    public class AddTripPageViewModel :ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public AddTripPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.Add_trip;
        }




    }
}
