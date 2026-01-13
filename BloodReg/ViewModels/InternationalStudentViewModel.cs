using BloodReg.Helpers;
using BloodReg.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SqlSugar;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using Wpf.Ui;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace BloodReg.ViewModels
{
    public partial class InternationalStudentViewModel : ObservableObject, INavigationAware
    {
        private readonly ISqlSugarClient db;
        private readonly ISnackbarService snackbarService;
        private readonly IContentDialogService contentDialogService;
        private int TotalPageCount;
        private bool needRefresh = false;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _chineseName = string.Empty;

        [ObservableProperty]
        private string _studentId = string.Empty;

        [ObservableProperty]
        private string _donationVolume = string.Empty;

        [ObservableProperty]
        private string _accountNumber = string.Empty;

        [ObservableProperty]
        private string _accountBank = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _passportNumber = string.Empty;

        [ObservableProperty]
        private string _nationality = string.Empty;

        [ObservableProperty]
        private string _birthday = string.Empty;

        [ObservableProperty]
        private string _firstEntryDate = string.Empty;

        [ObservableProperty]
        private string _weixinID = string.Empty;

        [ObservableProperty]
        private string _clerk = string.Empty;

        [ObservableProperty]
        private string _searchBoxText = string.Empty;

        [ObservableProperty]
        private bool _databaseEmpty = false;

        [ObservableProperty]
        private DataView _dataGridItems = new();

        [ObservableProperty]
        private List<int> _pageCountList = [20, 30, 50, 80];

        [ObservableProperty]
        private int _totalCount = 0;

        [ObservableProperty]
        private int _displayIndex = SettingsHelper.GetInt("InternationalStudentDisplayIndex");

        [ObservableProperty]
        private ObservableCollection<PageButton> _pageButtonList = [];

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int _targetPage = 1;

        [ObservableProperty]
        private bool _isPageUpEnabled = false;

        [ObservableProperty]
        private bool _isPageDownEnabled = false;

        [ObservableProperty]
        private bool _isFlyoutOpen = false;

        [ObservableProperty]
        private string _flyoutText = string.Empty;

        [ObservableProperty]
        private bool _isBottombarEnabled = false;

        public InternationalStudentViewModel(ISqlSugarClient db, ISnackbarService snackbarService, IContentDialogService contentDialogService)
        {
            this.db = db;
            this.snackbarService = snackbarService;
            this.contentDialogService = contentDialogService;

            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "database.db")))
            {
                db.DbMaintenance.CreateDatabase();
            }
            if (!db.DbMaintenance.IsAnyTable("InternationalStudent"))
            {
                db.CodeFirst.InitTables<InternationalStudent>();
            }
            RefreshAsync();
            PagerAsync();

            WeakReferenceMessenger.Default.Register<string>(this, (r, m) => { needRefresh = true; });
        }

        private async void RefreshAsync()
        {
            TotalCount = await db.Queryable<InternationalStudent>().CountAsync();
            if (TotalCount == 0)
            {
                DatabaseEmpty = true;
                IsBottombarEnabled = false;
                TotalPageCount = 0;
                return;
            }
            else
            {
                DatabaseEmpty = false;
                IsBottombarEnabled = true;
            }
            TotalPageCount = TotalCount / PageCountList[DisplayIndex] + ((TotalCount % PageCountList[DisplayIndex]) == 0 ? 0 : 1);
            if (CurrentPage > TotalPageCount) CurrentPage = TotalPageCount;
            if (TotalPageCount == 1) { IsPageUpEnabled = false; IsPageDownEnabled = false; return; }
            if (CurrentPage != 1) { IsPageUpEnabled = true; }
            if (CurrentPage != TotalPageCount) { IsPageDownEnabled = true; }
        }

        private async void PagerAsync()
        {
            DataGridItems = (await db.Queryable<InternationalStudent>().ToDataTablePageAsync(CurrentPage, PageCountList[DisplayIndex])).DefaultView;

            PageButtonList.Clear();
            if (TotalPageCount <= 7)
            {
                for (int i = 1; i <= TotalPageCount; i++)
                {
                    PageButtonList.Add(new PageButton(i.ToString(), CurrentPage == i));
                }
            }
            else
            {
                if (CurrentPage <= 4)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        PageButtonList.Add(new PageButton(i.ToString(), CurrentPage == i));
                    }
                    PageButtonList.Add(new PageButton("...", false, false));
                    PageButtonList.Add(new PageButton(TotalPageCount.ToString()));
                }
                else if (CurrentPage >= TotalPageCount - 3)
                {
                    PageButtonList.Add(new PageButton("1"));
                    PageButtonList.Add(new PageButton("...", false, false));
                    PageButtonList.Add(new PageButton((TotalPageCount - 4).ToString(), CurrentPage == TotalPageCount - 4));
                    PageButtonList.Add(new PageButton((TotalPageCount - 3).ToString(), CurrentPage == TotalPageCount - 3));
                    PageButtonList.Add(new PageButton((TotalPageCount - 2).ToString(), CurrentPage == TotalPageCount - 2));
                    PageButtonList.Add(new PageButton((TotalPageCount - 1).ToString(), CurrentPage == TotalPageCount - 1));
                    PageButtonList.Add(new PageButton(TotalPageCount.ToString(), CurrentPage == TotalPageCount));
                }
                else
                {
                    PageButtonList.Add(new PageButton("1"));
                    PageButtonList.Add(new PageButton("...", false, false));
                    PageButtonList.Add(new PageButton((CurrentPage - 1).ToString()));
                    PageButtonList.Add(new PageButton(CurrentPage.ToString(), true));
                    PageButtonList.Add(new PageButton((CurrentPage + 1).ToString()));
                    PageButtonList.Add(new PageButton("...", false, false));
                    PageButtonList.Add(new PageButton(TotalPageCount.ToString()));
                }
            }
        }

        partial void OnDisplayIndexChanged(int value)
        {
            SettingsHelper.SetConfig("InternationalStudentDisplayIndex", value.ToString());
            RefreshAsync();
            if (CurrentPage == 1) PagerAsync();
            CurrentPage = 1;
        }

        [RelayCommand]
        private void OnPageButtonClick(string parameter)
        {
            if (parameter == "PageUp")
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    if (!IsPageDownEnabled) IsPageDownEnabled = true;
                }
            }
            else
            {
                if (CurrentPage < TotalPageCount)
                {
                    CurrentPage++;
                    if (!IsPageUpEnabled) IsPageUpEnabled = true;
                }
            }
        }

        partial void OnCurrentPageChanged(int value)
        {
            TargetPage = value;
            PagerAsync();
            if (CurrentPage == 1) IsPageUpEnabled = false;
            else if (CurrentPage == TotalPageCount) IsPageDownEnabled = false;
        }

        [RelayCommand]
        private void GotoPage(string page)
        {
            CurrentPage = int.Parse(page);
            if (CurrentPage > 1) IsPageUpEnabled = true;
            if (CurrentPage < TotalPageCount) IsPageDownEnabled = true;
        }

        [RelayCommand]
        partial void OnTargetPageChanged(int value)
        {
            if (value > TotalPageCount)
            {
                FlyoutText = $"输入页码超过最大页码！";
                IsFlyoutOpen = true;
                TargetPage = TotalPageCount;
            }
            else if (value == 0)
            {
                FlyoutText = "最小页码为 1 ";
                IsFlyoutOpen = true;
                TargetPage = 1;
            }
            else if (value > 0 && value < TotalPageCount)
            {
                if (IsFlyoutOpen)
                {
                    IsFlyoutOpen = false;
                }
            }
        }

        public void GotoTargetPage(string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                TargetPage = -1;
                TargetPage = CurrentPage;
            }
            else
            {
                CurrentPage = TargetPage;
                if (CurrentPage > 1) IsPageUpEnabled = true;
                if (CurrentPage < TotalPageCount) IsPageDownEnabled = true;
            }
            if (IsFlyoutOpen)
            {
                IsFlyoutOpen = false;
            }
        }

        partial void OnSearchBoxTextChanged(string value)
        {
            AutoSuggest(value);
        }

        private async void AutoSuggest(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                IsBottombarEnabled = false;
                if (int.TryParse(value, out int num))
                {
                    DataGridItems = (await db.Queryable<InternationalStudent>().Where(it => it.StudentId.Contains(value)).ToDataTableAsync()).DefaultView;
                }
                else
                {
                    DataGridItems = (await db.Queryable<InternationalStudent>().Where(it => it.Name.Contains(value)).ToDataTableAsync()).DefaultView;
                }
                if (DataGridItems.Count > 0)
                {
                    DatabaseEmpty = false;
                }
                else
                {
                    DatabaseEmpty = true;
                }
                TotalCount = DataGridItems.Count;
            }
            else
            {
                RefreshAsync();
                PagerAsync();
            }
        }

        public async Task AddAsync()
        {
            StringBuilder tip = new();
            if (string.IsNullOrEmpty(Name))
            {
                tip.AppendLine("姓名不能为空！");
            }
            if (string.IsNullOrEmpty(ChineseName))
            {
                tip.AppendLine("中文名不能为空！");
            }
            if (string.IsNullOrEmpty(DonationVolume))
            {
                tip.AppendLine("献血量不能为空！");
            }
            if (string.IsNullOrEmpty(StudentId))
            {
                tip.AppendLine("学号不能为空！");
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                tip.AppendLine("手机号不能为空！");
            }
            if (string.IsNullOrEmpty(AccountNumber))
            {
                tip.AppendLine("银行卡号不能为空！");
            }
            if (string.IsNullOrEmpty(AccountBank))
            {
                tip.AppendLine("开户支行不能为空！");
            }
            if (string.IsNullOrEmpty(PassportNumber))
            {
                tip.AppendLine("护照号不能为空！");
            }
            if (string.IsNullOrEmpty(Nationality))
            {
                tip.AppendLine("国籍不能为空！");
            }
            if (string.IsNullOrEmpty(Birthday))
            {
                tip.AppendLine("生日不能为空！");
            }
            if (string.IsNullOrEmpty(FirstEntryDate))
            {
                tip.AppendLine("第一次入境时间不能为空！");
            }
            if (string.IsNullOrEmpty(WeixinID))
            {
                tip.AppendLine("微信号不能为空！");
            }
            if (string.IsNullOrEmpty(Clerk))
            {
                tip.AppendLine("录入人不能为空！");
            }
            if (!string.IsNullOrEmpty(tip.ToString()))
            {
                System.Media.SystemSounds.Asterisk.Play();
                snackbarService.Show("完善信息", tip.ToString().TrimEnd('\r', '\n'), ControlAppearance.Caution, new SymbolIcon(SymbolRegular.Warning16), TimeSpan.FromSeconds(3));
                return;
            }

            if (await IsInternationalStudentExistsAsync(StudentId))
            {
                System.Media.SystemSounds.Asterisk.Play();
                snackbarService.Show("学号重复", $"学号【{StudentId}】已在数据库中。", ControlAppearance.Caution, new SymbolIcon(SymbolRegular.Warning16), TimeSpan.FromSeconds(3));
                return;
            }

            db.Insertable(new InternationalStudent() { Name = Name, ChineseName = ChineseName, StudentId = StudentId, PhoneNumber = PhoneNumber, DonationVolume = DonationVolume, AccountNumber = AccountNumber, AccountBank = AccountBank, PassportNumber = PassportNumber, Nationality = Nationality, FirstEntryDate = FirstEntryDate, Birthday = Birthday, WeixinID = WeixinID, Clerk = Clerk }).ExecuteCommand();
            System.Media.SystemSounds.Asterisk.Play();
            snackbarService.Show("添加成功", $"校外人员【{Name}】已添加到数据库中。", ControlAppearance.Success, new SymbolIcon(SymbolRegular.Info16), TimeSpan.FromSeconds(3));
            RefreshAsync();
            PagerAsync();

            Name = string.Empty;
            ChineseName = string.Empty;
            StudentId = string.Empty;
            DonationVolume = string.Empty;
            AccountNumber = string.Empty;
            AccountBank = string.Empty;
            PhoneNumber = string.Empty;
            WeixinID = string.Empty;
            FirstEntryDate = string.Empty;
            PassportNumber = string.Empty;
            Nationality = string.Empty;
            Birthday = string.Empty;
        }

        public async ValueTask<bool> IsInternationalStudentExistsAsync(string id)
        {
            return await db.Queryable<InternationalStudent>().AnyAsync(it => it.StudentId == id);
        }

        public async ValueTask<bool> Update(InternationalStudent internationalStudent, string oldId)
        {
            if (internationalStudent.StudentId != oldId && await IsInternationalStudentExistsAsync(internationalStudent.StudentId))
            {
                System.Media.SystemSounds.Asterisk.Play();
                snackbarService.Show("更改失败", $"学号【{StudentId}】已在数据库中。", ControlAppearance.Caution, new SymbolIcon(SymbolRegular.Warning16), TimeSpan.FromSeconds(3));
                return false;
            }
            else
            {
                await db.Ado.ExecuteCommandAsync("UPDATE InternationalStudent SET Name=@name, ChineseName=@chineseName, StudentId=@id, DonationVolume=@volume, PhoneNumber=@phoneNumber, AccountNumber=@accountNumber, AccountBank=@accountBank, PassportNumber=@passportNumber, Nationality=@nationality, Birthday=@birthday, FirstEntryDate=@firstEntryDate, WeixinID=@weixinID, Clerk=@clerk WHERE StudentId=@oldid",
                new List<SugarParameter>() { new("@name", internationalStudent.Name), new("@chineseName", internationalStudent.ChineseName), new("@id", internationalStudent.StudentId), new("@volume", internationalStudent.DonationVolume), new("@phoneNumber", internationalStudent.PhoneNumber), new("@accountNumber", internationalStudent.AccountNumber), new("@accountBank", internationalStudent.AccountBank), new("@passportNumber", internationalStudent.PassportNumber), new("@nationality", internationalStudent.Nationality), new("@birthday", internationalStudent.Birthday), new("@firstEntryDate", internationalStudent.FirstEntryDate), new("@weixinID", internationalStudent.WeixinID), new("@clerk", internationalStudent.Clerk), new("oldid", oldId) });
                return true;
            }
        }

        [RelayCommand]
        private async Task Del(DataRowView selectedItem)
        {
            System.Media.SystemSounds.Asterisk.Play();
            ContentDialogResult result = await contentDialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions()
            {
                Title = "删除",
                Content = $"是否删除留学生【{(string)selectedItem[0]}】 ，此操作不可撤销！",
                PrimaryButtonText = "是",
                CloseButtonText = "否",
            });

            if (result == ContentDialogResult.Primary)
            {
                db.Deleteable<InternationalStudent>().Where(it => it.StudentId == (string)selectedItem[2]).ExecuteCommand();
                RefreshAsync();
                PagerAsync();
            }
        }

        public Task OnNavigatedToAsync()
        {
            Utils.ChangeAppTitle("献血信息登记系统 - 留学生录入");
            if (needRefresh)
            {
                RefreshAsync();
                PagerAsync();
                needRefresh = false;
            }
            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync()
        {
            return Task.CompletedTask;
        }
    }
}
