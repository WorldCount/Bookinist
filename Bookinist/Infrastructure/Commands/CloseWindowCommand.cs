using Bookinist.Infrastructure.Commands.Base;
using System.Windows;

namespace Bookinist.Infrastructure.Commands
{
    class CloseWindowCommand : Command
    {
        private static Window GetWindow(object p) => p as Window ?? App.FocusedWindow ?? App.ActiveWindow;

        protected override bool CanExecute(object p) => GetWindow(p) != null;

        protected override void Execute(object p) => GetWindow(p)?.Close();
    }
}
