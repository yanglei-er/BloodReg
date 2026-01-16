using BloodReg.ViewModels;
using System.Windows;
using Wpf.Ui.Controls;

namespace BloodReg.Views.Dialogs
{
    public partial class DatabaseExportDialog : ContentDialog
    {
        public DatabaseExportDialogViewModel ViewModel { get; }
        public DatabaseExportDialog(DatabaseExportDialogViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide(ContentDialogResult.None);
        }

        private async void Card_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CardAction card)
            {
                if (await ViewModel.Export(card.Name))
                {
                    Hide(ContentDialogResult.Primary);
                }
            }
        }
    }
}