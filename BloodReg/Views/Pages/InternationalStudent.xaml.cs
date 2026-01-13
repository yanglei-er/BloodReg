using BloodReg.Models;
using BloodReg.ViewModels;
using System.Data;
using System.Windows.Input;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Controls;

namespace BloodReg.Views.Pages
{
    public partial class InternationalStudent : INavigableView<InternationalStudentViewModel>
    {
        private readonly Models.InternationalStudent oldInternationalStudent = new();
        public InternationalStudentViewModel ViewModel { get; }
        public InternationalStudent(InternationalStudentViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }

        private void SearchBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                XuNiBox.Focus();
            }
        }

        private void NameBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DonationVolumeBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                NameBox.Clear();
            }
        }

        private void DonationVolumeBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ChineseNameBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                DonationVolumeBox.Clear();
            }
        }

        private void ChineseNameBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                StudentIdBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                ChineseNameBox.Clear();
            }
        }

        private void StudentIdBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PhoneNumberBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                StudentIdBox.Clear();
            }
        }

        private void PhoneNumberBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AccountNumberBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                PhoneNumberBox.Clear();
            }
        }

        private void AccountNumberBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AccountBankBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                AccountNumberBox.Clear();
            }
        }

        private void AccountBankBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PassportNumberBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                AccountBankBox.Clear();
            }
        }

        private void PassportNumberBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NationalityBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                PassportNumberBox.Clear();
            }
        }

        private void NationalityBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BirthdayBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                NationalityBox.Clear();
            }
        }

        private void BirthdayBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FirstEntryDateBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                BirthdayBox.Clear();
            }
        }

        private void FirstEntryDateBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                WeixinIDBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                FirstEntryDateBox.Clear();
            }
        }

        private void WeixinIDBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _ = ViewModel.AddAsync();
                CheckEmpty();
            }
            else if (e.Key == Key.Escape)
            {
                WeixinIDBox.Clear();
            }
        }

        private void ClerkBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _ = ViewModel.AddAsync();
                CheckEmpty();
            }
            else if (e.Key == Key.Escape)
            {
                ClerkBox.Clear();
            }
        }

        private void PageBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                XuNiBox.Focus();
                ViewModel.GotoTargetPage(textBox.Text);
            }
        }

        private void DataGrid_BeginningEdit(object sender, System.Windows.Controls.DataGridBeginningEditEventArgs e)
        {
            if (e.Row.Item is DataRowView dataRowView)
            {
                oldInternationalStudent.Name = (string)dataRowView[0];
                oldInternationalStudent.ChineseName = (string)dataRowView[1];
                oldInternationalStudent.StudentId = (string)dataRowView[2];
                oldInternationalStudent.PhoneNumber = (string)dataRowView[3];
                oldInternationalStudent.AccountNumber = (string)dataRowView[4];
                oldInternationalStudent.AccountBank = (string)dataRowView[5];
                oldInternationalStudent.PassportNumber = (string)dataRowView[6];
                oldInternationalStudent.Nationality = (string)dataRowView[7];
                oldInternationalStudent.Birthday = (string)dataRowView[8];
                oldInternationalStudent.FirstEntryDate = (string)dataRowView[9];
                oldInternationalStudent.WeixinID = (string)dataRowView[10];
                oldInternationalStudent.DonationVolume = (string)dataRowView[11];
                oldInternationalStudent.Clerk = (string)dataRowView[12];
            }
        }

        private async void DataGrid_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            if (e.Row.Item is DataRowView dataRowView)
            {
                if (string.IsNullOrEmpty(dataRowView[0].ToString()))
                {
                    dataRowView[0] = oldInternationalStudent.Name;
                }
                if (string.IsNullOrEmpty(dataRowView[1].ToString()))
                {
                    dataRowView[1] = oldInternationalStudent.ChineseName;
                }
                if (string.IsNullOrEmpty(dataRowView[2].ToString()))
                {
                    dataRowView[2] = oldInternationalStudent.StudentId;
                }
                if (string.IsNullOrEmpty(dataRowView[3].ToString()))
                {
                    dataRowView[3] = oldInternationalStudent.PhoneNumber;
                }
                if (string.IsNullOrEmpty(dataRowView[4].ToString()))
                {
                    dataRowView[4] = oldInternationalStudent.AccountNumber;
                }
                if (string.IsNullOrEmpty(dataRowView[5].ToString()))
                {
                    dataRowView[5] = oldInternationalStudent.AccountBank;
                }
                if (string.IsNullOrEmpty(dataRowView[6].ToString()))
                {
                    dataRowView[6] = oldInternationalStudent.PassportNumber;
                }
                if (string.IsNullOrEmpty(dataRowView[7].ToString()))
                {
                    dataRowView[6] = oldInternationalStudent.Nationality;
                }
                if (string.IsNullOrEmpty(dataRowView[8].ToString()))
                {
                    dataRowView[6] = oldInternationalStudent.Birthday;
                }
                if (string.IsNullOrEmpty(dataRowView[9].ToString()))
                {
                    dataRowView[6] = oldInternationalStudent.FirstEntryDate;
                }
                if (string.IsNullOrEmpty(dataRowView[10].ToString()))
                {
                    dataRowView[6] = oldInternationalStudent.WeixinID;
                }
                if (string.IsNullOrEmpty(dataRowView[11].ToString()))
                {
                    dataRowView[6] = oldInternationalStudent.DonationVolume;
                }

                Models.InternationalStudent newInternationalStudent = new()
                {
                    Name = (string)dataRowView[0],
                    ChineseName = (string)dataRowView[1],
                    StudentId = (string)dataRowView[2],
                    PhoneNumber = (string)dataRowView[3],
                    AccountNumber = (string)dataRowView[4],
                    AccountBank = (string)dataRowView[5],
                    PassportNumber = (string)dataRowView[6],
                    Nationality = (string)dataRowView[7],
                    Birthday = (string)dataRowView[8],
                    FirstEntryDate = (string)dataRowView[9],
                    WeixinID = (string)dataRowView[10],
                    DonationVolume = (string)dataRowView[11],
                    Clerk = (string)dataRowView[12]
                };
                if (!newInternationalStudent.Equals(oldInternationalStudent))
                {
                    if (!await ViewModel.Update(newInternationalStudent, oldInternationalStudent.StudentId))
                    {
                        dataRowView[2] = oldInternationalStudent.StudentId;
                    }
                }
            }
        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _ = ViewModel.AddAsync();
            CheckEmpty();
        }

        private void CheckEmpty()
        {
            if (string.IsNullOrEmpty(NameBox.Text))
            {
                NameBox.Focus();
            }
            else if (string.IsNullOrEmpty(ChineseNameBox.Text))
            {
                ChineseNameBox.Focus();
            }
            else if (string.IsNullOrEmpty(DonationVolumeBox.Text))
            {
                DonationVolumeBox.Focus();
            }
            else if (string.IsNullOrEmpty(StudentIdBox.Text))
            {
                StudentIdBox.Focus();
            }
            else if (string.IsNullOrEmpty(PhoneNumberBox.Text))
            {
                PhoneNumberBox.Focus();
            }
            else if (string.IsNullOrEmpty(AccountNumberBox.Text))
            {
                AccountNumberBox.Focus();
            }
            else if (string.IsNullOrEmpty(AccountBankBox.Text))
            {
                AccountBankBox.Focus();
            }
            else if (string.IsNullOrEmpty(PassportNumberBox.Text))
            {
                PassportNumberBox.Focus();
            }
            else if (string.IsNullOrEmpty(NationalityBox.Text))
            {
                NationalityBox.Focus();
            }
            else if (string.IsNullOrEmpty(BirthdayBox.Text))
            {
                BirthdayBox.Focus();
            }
            else if (string.IsNullOrEmpty(FirstEntryDateBox.Text))
            {
                FirstEntryDateBox.Focus();
            }
            else if (string.IsNullOrEmpty(WeixinIDBox.Text))
            {
                WeixinIDBox.Focus();
            }
            else if (string.IsNullOrEmpty(ClerkBox.Text))
            {
                ClerkBox.Focus();
            }
        }
    }
}
