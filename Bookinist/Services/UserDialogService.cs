using Bookinist.Services.Interfaces;
using System.Windows;

namespace Bookinist.Services
{
    internal class UserDialogService : IUserDialog
    {
        public void Information(string title, string message) =>
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);

        public void Warning(string title, string message) =>
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);

        public void Error(string title, string message) =>
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
