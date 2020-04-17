using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VLegalizer.Common.Models;

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
                    Title = "My trips"
                },

                new Menu
                {
                    Icon = "ic_add_circle",
                    PageName = "AddTripPage",
                    Title = "Add new trip"
                },

                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "AccountPage",
                    Title = "Admin my account"
                },


                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Logout"
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

