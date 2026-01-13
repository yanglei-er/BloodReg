using BloodReg.ViewModels;
using System.Data;
using System.Windows.Input;
using Wpf.Ui.Abstractions.Controls;

namespace BloodReg.Views.Pages
{
    public partial class Teacher : INavigableView<TeacherViewModel>
    {
        private readonly Models.Teacher oldTeacher = new();
        public TeacherViewModel ViewModel { get; }
        public Teacher(TeacherViewModel viewModel)
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
                EmployeeIDBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                DonationVolumeBox.Clear();
            }
        }

        private void EmployeeIDBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PhoneNumberBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                EmployeeIDBox.Clear();
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
                _ = ViewModel.AddAsync();
                CheckEmpty();
            }
            else if (e.Key == Key.Escape)
            {
                AccountBankBox.Clear();
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
                oldTeacher.Name = (string)dataRowView[0];
                oldTeacher.EmployeeID = (string)dataRowView[1];
                oldTeacher.PhoneNumber = (string)dataRowView[2];
                oldTeacher.AccountNumber = (string)dataRowView[3];
                oldTeacher.AccountBank = (string)dataRowView[4];
                oldTeacher.DonationVolume = (string)dataRowView[5];
                oldTeacher.Clerk = (string)dataRowView[6];
            }
        }

        private async void DataGrid_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            if (e.Row.Item is DataRowView dataRowView)
            {
                if (string.IsNullOrEmpty(dataRowView[0].ToString()))
                {
                    dataRowView[0] = oldTeacher.Name;
                }
                if (string.IsNullOrEmpty(dataRowView[1].ToString()))
                {
                    dataRowView[1] =    oldTeacher.EmployeeID;
                }
                if (string.IsNullOrEmpty(dataRowView[2].ToString()))
                {
                    dataRowView[3] = oldTeacher.PhoneNumber;
                }
                if (string.IsNullOrEmpty(dataRowView[3].ToString()))
                {
                    dataRowView[4] = oldTeacher.AccountNumber;
                }
                if (string.IsNullOrEmpty(dataRowView[4].ToString()))
                {
                    dataRowView[5] = oldTeacher.AccountBank;
                }
                if (string.IsNullOrEmpty(dataRowView[5].ToString()))
                {
                    dataRowView[6] = oldTeacher.DonationVolume;
                }

                Models.Teacher newTeacher = new()
                {
                    Name = (string)dataRowView[0],
                    EmployeeID = (string)dataRowView[1],
                    PhoneNumber = (string)dataRowView[2],
                    AccountNumber = (string)dataRowView[3],
                    AccountBank = (string)dataRowView[4],
                    DonationVolume = (string)dataRowView[5],
                    Clerk = (string)dataRowView[6]
                };
                if (!newTeacher.Equals(oldTeacher))
                {
                    if (!await ViewModel.Update(newTeacher, oldTeacher.EmployeeID))
                    {
                        dataRowView[1] = oldTeacher.EmployeeID;
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
            else if (string.IsNullOrEmpty(DonationVolumeBox.Text))
            {
                DonationVolumeBox.Focus();
            }
            else if (string.IsNullOrEmpty(EmployeeIDBox.Text))
            {
                EmployeeIDBox.Focus();
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
            else if (string.IsNullOrEmpty(ClerkBox.Text))
            {
                ClerkBox.Focus();
            }
        }
    }
}
