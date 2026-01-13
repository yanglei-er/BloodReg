using BloodReg.ViewModels;
using System.Data;
using System.Windows.Input;
using Wpf.Ui.Abstractions.Controls;

namespace BloodReg.Views.Pages
{
    public partial class OutsidePeople : INavigableView<OutsidePeopleViewModel>
    {
        private readonly Models.OutsidePeople oldOutsidePeople = new();
        public OutsidePeopleViewModel ViewModel { get; }
        public OutsidePeople(OutsidePeopleViewModel viewModel)
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
                IDNumberBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                EmployeeIDBox.Clear();
            }
        }

        private void IDNumberBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PhoneNumberBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                IDNumberBox.Clear();
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
                oldOutsidePeople.Name = (string)dataRowView[0];
                oldOutsidePeople.EmployeeID = (string)dataRowView[1];
                oldOutsidePeople.IDNumber = (string)dataRowView[2];
                oldOutsidePeople.PhoneNumber = (string)dataRowView[3];
                oldOutsidePeople.AccountNumber = (string)dataRowView[4];
                oldOutsidePeople.AccountBank = (string)dataRowView[5];
                oldOutsidePeople.DonationVolume = (string)dataRowView[6];
                oldOutsidePeople.Clerk = (string)dataRowView[7];
            }
        }

        private async void DataGrid_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            if (e.Row.Item is DataRowView dataRowView)
            {
                if (string.IsNullOrEmpty(dataRowView[0].ToString()))
                {
                    dataRowView[0] = oldOutsidePeople.Name;
                }
                if (string.IsNullOrEmpty(dataRowView[1].ToString()))
                {
                    dataRowView[1] = oldOutsidePeople.EmployeeID;
                }
                if (string.IsNullOrEmpty(dataRowView[2].ToString()))
                {
                    dataRowView[2] = oldOutsidePeople.IDNumber;
                }
                if (string.IsNullOrEmpty(dataRowView[2].ToString()))
                {
                    dataRowView[3] = oldOutsidePeople.PhoneNumber;
                }
                if (string.IsNullOrEmpty(dataRowView[3].ToString()))
                {
                    dataRowView[4] = oldOutsidePeople.AccountNumber;
                }
                if (string.IsNullOrEmpty(dataRowView[4].ToString()))
                {
                    dataRowView[5] = oldOutsidePeople.AccountBank;
                }
                if (string.IsNullOrEmpty(dataRowView[5].ToString()))
                {
                    dataRowView[6] = oldOutsidePeople.DonationVolume;
                }

                Models.OutsidePeople newOutsidePeople = new()
                {
                    Name = (string)dataRowView[0],
                    EmployeeID = (string)dataRowView[1],
                    IDNumber = (string)dataRowView[2],
                    PhoneNumber = (string)dataRowView[3],
                    AccountNumber = (string)dataRowView[4],
                    AccountBank = (string)dataRowView[5],
                    DonationVolume = (string)dataRowView[6],
                    Clerk = (string)dataRowView[7]
                };
                if (!newOutsidePeople.Equals(oldOutsidePeople))
                {
                    if (!await ViewModel.Update(newOutsidePeople, oldOutsidePeople.EmployeeID))
                    {
                        dataRowView[2] = oldOutsidePeople.EmployeeID;
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
            else if (string.IsNullOrEmpty(IDNumberBox.Text))
            {
                IDNumberBox.Focus();
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
