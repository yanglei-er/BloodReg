using BloodReg.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace BloodReg.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<object> _navigationItems = [];

        [ObservableProperty]
        private ObservableCollection<object> _navigationFooter = [];

        public MainWindowViewModel()
        {
            NavigationItems =
                [
                    new NavigationViewItem()
                    {
                        Content = "主页",
                        Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                        TargetPageType = typeof(Views.Pages.Home),
                    },
                    new NavigationViewItem()
                    {
                        Content = "学生",
                        Icon = new ImageIcon{Source=ImageProcess.StringToBitmapImage("pack://application:,,,/Assets/student.png"), Width=28, Height=28},
                        TargetPageType = typeof(Views.Pages.Student)
                    },
                    new NavigationViewItem()
                    {
                        Content = "教职工",
                        Icon = new ImageIcon{Source=ImageProcess.StringToBitmapImage("pack://application:,,,/Assets/teacher.png"), Width=28, Height=28},
                        TargetPageType = typeof(Views.Pages.Teacher)
                    },
                    new NavigationViewItem()
                    {
                        Content = "留学生",
                        Icon = new ImageIcon{Source=ImageProcess.StringToBitmapImage("pack://application:,,,/Assets/Internationalstudent.png"), Width=28, Height=28},
                        TargetPageType = typeof(Views.Pages.InternationalStudent)
                    },
                    new NavigationViewItem()
                    {
                        Content = "校外人员",
                        Icon = new ImageIcon{Source = ImageProcess.StringToBitmapImage("pack://application:,,,/Assets/outsidepeople.png"), Width = 28, Height = 28},
                        TargetPageType = typeof(Views.Pages.OutsidePeople)
                    },
                ];

            NavigationFooter.Add(new NavigationViewItem()
            {
                Content = "设置",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.Settings)
            });
        }
    }
}
