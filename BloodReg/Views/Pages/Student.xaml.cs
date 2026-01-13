using BloodReg.Models;
using BloodReg.ViewModels;
using System.Data;
using System.Windows.Input;
using Wpf.Ui.Abstractions.Controls;

namespace BloodReg.Views.Pages
{
    public partial class Student : INavigableView<StudentViewModel>
    {
        private readonly Models.Student oldStudent = new();
        public StudentViewModel ViewModel { get; }

        public Student(StudentViewModel viewModel)
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
                StudentIdBox.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                DonationVolumeBox.Clear();
            }
        }

        private void StudentIdBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _ = ViewModel.AddAsync();
                CheckEmpty();
            }
            else if (e.Key == Key.Escape)
            {
                StudentIdBox.Clear();
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
                oldStudent.Name = (string)dataRowView[0];
                oldStudent.StudentId = (string)dataRowView[1];
                oldStudent.DonationVolume = (string)dataRowView[2];
                oldStudent.Clerk = (string)dataRowView[3];
            }
        }

        private async void DataGrid_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            if (e.Row.Item is DataRowView dataRowView)
            {
                if (string.IsNullOrEmpty(dataRowView[0].ToString()))
                {
                    dataRowView[0] = oldStudent.Name;
                }
                if (string.IsNullOrEmpty(dataRowView[1].ToString()))
                {
                    dataRowView[1] = oldStudent.StudentId;
                }
                if (string.IsNullOrEmpty(dataRowView[2].ToString()))
                {
                    dataRowView[2] = oldStudent.DonationVolume;
                }

                Models.Student newStudent = new()
                {
                    Name = (string)dataRowView[0],
                    StudentId = (string)dataRowView[1],
                    DonationVolume = (string)dataRowView[2],
                    Clerk = (string)dataRowView[3]
                };
                if (!oldStudent.Equals(newStudent))
                {
                    if (!await ViewModel.Update(newStudent, oldStudent.StudentId))
                    {
                        dataRowView[1] = oldStudent.StudentId;
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
            else if (string.IsNullOrEmpty(ClerkBox.Text))
            {
                ClerkBox.Focus();
            }
        }
    }
}
