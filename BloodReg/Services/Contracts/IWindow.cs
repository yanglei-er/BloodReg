using System.Windows;

namespace BloodReg.Services.Contracts
{
    public interface IWindow
    {
        event RoutedEventHandler Loaded;
        void Show();
    }
}