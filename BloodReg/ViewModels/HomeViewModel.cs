using BloodReg.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui;
using Wpf.Ui.Abstractions.Controls;

namespace BloodReg.ViewModels
{
    public partial class HomeViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService navigationService;

        public HomeViewModel(INavigationService _navigationService)
        {
            navigationService = _navigationService;
        }

        [RelayCommand]
        private void OnCardClick(string parameter)
        {
            if (parameter == "学生")
            {
                navigationService.Navigate(typeof(Views.Pages.Student));
            }
            else if (parameter == "教职工")
            {
                navigationService.Navigate(typeof(Views.Pages.Teacher));
            }
            else if (parameter == "留学生")
            {
                navigationService.Navigate(typeof(Views.Pages.InternationalStudent));
            }
            else if (parameter == "校外人员")
            {
                navigationService.Navigate(typeof(Views.Pages.OutsidePeople));
            }
        }

        public Task OnNavigatedToAsync()
        {
            Utils.ChangeAppTitle("献血信息登记系统 - 主页");
            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync()
        {
            return Task.CompletedTask;
        }
    }
}
