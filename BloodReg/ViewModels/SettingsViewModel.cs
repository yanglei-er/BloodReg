using BloodReg.Helpers;
using BloodReg.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SqlSugar;
using System.Windows;
using System.Windows.Media;
using Wpf.Ui;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace BloodReg.ViewModels
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private readonly ISqlSugarClient db;
        private readonly ISnackbarService snackbarService;
        private readonly IContentDialogService contentDialogService;

        [ObservableProperty]
        private int _currentApplicationThemeIndex = Utils.GetCurrentApplicationThemeIndex(SettingsHelper.GetConfig("Theme"));

        [ObservableProperty]
        private bool _isCustomizedAccentColor = SettingsHelper.GetBoolean("IsCustomizedAccentColor");

        #region AccentColorGroup
        [ObservableProperty]
        private SolidColorBrush _systemAccentColor = new();
        [ObservableProperty]
        private SolidColorBrush? _light1;
        [ObservableProperty]
        private SolidColorBrush? _light2;
        [ObservableProperty]
        private SolidColorBrush? _light3;
        [ObservableProperty]
        private SolidColorBrush? _dark1;
        [ObservableProperty]
        private SolidColorBrush? _dark2;
        [ObservableProperty]
        private SolidColorBrush? _dark3;
        #endregion AccentColorGroup

        [ObservableProperty]
        private int _currentBackdropIndex = Utils.GetCurrentBackdropIndex(SettingsHelper.GetConfig("Backdrop"));

        #region FileOccupancy
        [ObservableProperty]
        private bool _isFileOccupancyExpanded = false;
        [ObservableProperty]
        private string _dataCount = "正在计算";
        #endregion FileOccupancy

        public SettingsViewModel(ISqlSugarClient _db, ISnackbarService _snackbarService, IContentDialogService _contentDialogService)
        {
            db = _db;
            snackbarService = _snackbarService;
            contentDialogService = _contentDialogService;
        }

        partial void OnCurrentApplicationThemeIndexChanged(int value)
        {
            if (value == 0)
            {
                SettingsHelper.SetConfig("Theme", "System");
                ApplicationTheme theme = Utils.GetUserApplicationTheme("System");
                ApplicationThemeManager.Apply(theme, Utils.GetUserBackdrop(SettingsHelper.GetConfig("Backdrop")), false);
            }
            else if (value == 1)
            {
                SettingsHelper.SetConfig("Theme", "Light");
                ApplicationThemeManager.Apply(ApplicationTheme.Light, Utils.GetUserBackdrop(SettingsHelper.GetConfig("Backdrop")), false);
            }
            else
            {
                SettingsHelper.SetConfig("Theme", "Dark");
                ApplicationThemeManager.Apply(ApplicationTheme.Dark, Utils.GetUserBackdrop(SettingsHelper.GetConfig("Backdrop")), false);
            }
        }

        partial void OnIsCustomizedAccentColorChanged(bool value)
        {
            SettingsHelper.SetConfig("IsCustomizedAccentColor", value.ToString());
            if (value)
            {
                SystemAccentColor = Utils.StringToSolidColorBrush(SettingsHelper.GetConfig("CustomizedAccentColor"));
            }
            else
            {
                ApplicationAccentColorManager.ApplySystemAccent();
                SystemAccentColor = (SolidColorBrush)ApplicationAccentColorManager.SystemAccentBrush;
            }
            Color _color = SystemAccentColor.Color;
            Light1 = Utils.ColorToSolidColorBrush(_color.Update(15f, -12f));
            Light2 = Utils.ColorToSolidColorBrush(_color.Update(30f, -24f));
            Light3 = Utils.ColorToSolidColorBrush(_color.Update(45f, -36f));
            Dark1 = Utils.ColorToSolidColorBrush(_color.UpdateBrightness(-5f));
            Dark2 = Utils.ColorToSolidColorBrush(_color.UpdateBrightness(-10f));
            Dark3 = Utils.ColorToSolidColorBrush(_color.UpdateBrightness(-15f));
        }

        public void ColorExpander_Expanded()
        {
            if (IsCustomizedAccentColor)
            {
                SystemAccentColor = Utils.StringToSolidColorBrush(SettingsHelper.GetConfig("CustomizedAccentColor"));
            }
            else
            {
                SystemAccentColor = (SolidColorBrush)ApplicationAccentColorManager.SystemAccentBrush;
            }
            Color _color = SystemAccentColor.Color;
            Light1 = Utils.ColorToSolidColorBrush(_color.Update(15f, -12f));
            Light2 = Utils.ColorToSolidColorBrush(_color.Update(30f, -24f));
            Light3 = Utils.ColorToSolidColorBrush(_color.Update(45f, -36f));
            Dark1 = Utils.ColorToSolidColorBrush(_color.UpdateBrightness(-5f));
            Dark2 = Utils.ColorToSolidColorBrush(_color.UpdateBrightness(-10f));
            Dark3 = Utils.ColorToSolidColorBrush(_color.UpdateBrightness(-15f));
        }

        [RelayCommand]
        private void OnCustomizedAccentColorChanged(string color)
        {
            if (color != SystemAccentColor.ToString())
            {
                if (IsCustomizedAccentColor)
                {
                    SystemAccentColor = Utils.StringToSolidColorBrush(color);
                    SettingsHelper.SetConfig("CustomizedAccentColor", color);
                }
                else
                {
                    SystemAccentColor = (SolidColorBrush)ApplicationAccentColorManager.SystemAccentBrush;
                }
                Color _color = SystemAccentColor.Color;
                Light1 = Utils.ColorToSolidColorBrush(_color.Update(15f, -12f));
                Light2 = Utils.ColorToSolidColorBrush(_color.Update(30f, -24f));
                Light3 = Utils.ColorToSolidColorBrush(_color.Update(45f, -36f));
                Dark1 = Utils.ColorToSolidColorBrush(_color.UpdateBrightness(-5f));
                Dark2 = Utils.ColorToSolidColorBrush(_color.UpdateBrightness(-10f));
                Dark3 = Utils.ColorToSolidColorBrush(_color.UpdateBrightness(-15f));
            }
        }

        partial void OnCurrentBackdropIndexChanged(int value)
        {
            ApplicationTheme theme = Utils.GetUserApplicationTheme(SettingsHelper.GetConfig("Theme"));
            if (value == 0)
            {
                SettingsHelper.SetConfig("Backdrop", "None");
                ApplicationThemeManager.Apply(theme, WindowBackdropType.None);
            }
            else if (value == 1)
            {
                SettingsHelper.SetConfig("Backdrop", "Acrylic");
                ApplicationThemeManager.Apply(theme, WindowBackdropType.Acrylic);
            }
            else if (value == 2)
            {
                SettingsHelper.SetConfig("Backdrop", "Mica");
                ApplicationThemeManager.Apply(theme, WindowBackdropType.Mica);
            }
            else
            {
                SettingsHelper.SetConfig("Backdrop", "Tabbed");
                ApplicationThemeManager.Apply(theme, WindowBackdropType.Tabbed);
            }
        }

        public async void FileOccupancyExpander_Expanded()
        {
            if (IsFileOccupancyExpanded)
            {
                DataCount = "数据库文件已占用 " + await FileOccupancy.GetFileSizeAsync(Environment.CurrentDirectory + @"\database.db");
            }
        }

        [RelayCommand]
        private async Task OnCleanDatabaseButtonClick()
        {
            System.Media.SystemSounds.Asterisk.Play();
            ContentDialogResult result = await contentDialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions()
            {
                Title = "重置数据库",
                Content = "您的所有数据将被删除，且无法恢复，您确定要继续吗?",
                PrimaryButtonText = "是",
                CloseButtonText = "否",
            });
            if (result == ContentDialogResult.Primary)
            {
                if (db.DbMaintenance.IsAnyTable("Student"))
                {
                    db.DbMaintenance.TruncateTable<Student>();
                }
                if (db.DbMaintenance.IsAnyTable("Teacher"))
                {
                    db.DbMaintenance.TruncateTable<Teacher>();
                }
                if (db.DbMaintenance.IsAnyTable("InternationalStudent"))
                {
                    db.DbMaintenance.TruncateTable<InternationalStudent>();
                }
                if (db.DbMaintenance.IsAnyTable("OutsidePeople"))
                {
                    db.DbMaintenance.TruncateTable<OutsidePeople>();
                }
                DataCount = "数据库文件已占用 " + await FileOccupancy.GetFileSizeAsync(Environment.CurrentDirectory + @"\database.db");
                WeakReferenceMessenger.Default.Send("refresh");
                snackbarService.Show("重置成功", "所有数据已清除。", ControlAppearance.Success, new SymbolIcon(SymbolRegular.Info16), TimeSpan.FromSeconds(3));
            }
        }

        public void CopyMailAddress()
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetText("zhao.yanglei@foxmail.com");
                snackbarService.Show("复制成功", "邮箱地址已复制到剪贴板", ControlAppearance.Success, new SymbolIcon(SymbolRegular.Info16), TimeSpan.FromSeconds(3));
            }
            catch (Exception e)
            {
                snackbarService.Show("复制失败", $"{e.Message}", ControlAppearance.Caution, new SymbolIcon(SymbolRegular.Info16), TimeSpan.FromSeconds(3));
            }
        }

        public Task OnNavigatedToAsync()
        {
            Utils.ChangeAppTitle("献血信息登记系统 - 设置");
            FileOccupancyExpander_Expanded();
            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync()
        {
            return Task.CompletedTask;
        }
    }
}
