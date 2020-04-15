using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VLegalizer.Common.Models;

namespace VLegalizer.Prism.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        private EmployeeResponse _employee;

        public TripsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Trips";
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            if (parameters.ContainsKey("employee"))
            {
                _employee = parameters.GetValue<EmployeeResponse>("employee");


            }
        }
    }
}
