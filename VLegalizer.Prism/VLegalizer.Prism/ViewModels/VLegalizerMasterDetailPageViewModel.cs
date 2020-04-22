using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VLegalizer.Common.Models;
using VLegalizer.Prism.Helpers;

namespace VLegalizer.Prism.ViewModels
{
    public class VLegalizerMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public VLegalizerMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_card_travel",
                    PageName = "TripsPage",
                    Title = Languages.Trips
                },

                new Menu
                {
                    Icon = "ic_add_circle",
                    PageName = "AddTripPage",
                    Title = Languages.Add_trip
                },

                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "AccountPage",
                    Title = Languages.Account
                },


                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = Languages.Logout
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());

        }
    }
}

