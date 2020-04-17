using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VLegalizer.Prism.ViewModels
{
    public class AccountPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public AccountPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Account";
        }
    }
}
