
namespace Bookinist.Services.Interfaces
{
    internal interface IUserDialog
    {
        void Information(string title, string message);

        void Warning(string title, string message);

        void Error(string title, string message);
    }
}
