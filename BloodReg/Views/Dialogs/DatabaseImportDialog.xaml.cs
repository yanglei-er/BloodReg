using BloodReg.ViewModels;
using System.Windows;
using Wpf.Ui.Controls;

namespace BloodReg.Views.Dialogs
{
    public partial class DatabaseImportDialog : ContentDialog
    {
        public DatabaseImportDialogViewModel ViewModel { get; }
        public DatabaseImportDialog(DatabaseImportDialogViewModel viewModel)
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
                if (await ViewModel.Import(card.Name))
                {
                    Hide(ContentDialogResult.Primary);
                }
            }
        }
    }
}
