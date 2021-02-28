using System.Linq;
using Bookinist.DB.Entityes;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IUserDialog _userDialog;
        private readonly IDataService _dataService;
        private readonly IRepository<Book> _bookRepository;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _title = "Главное окно";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _status = "Готов!";

        /// <summary>Статус</summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        public MainWindowViewModel(IUserDialog userDialog, IDataService dataService, IRepository<Book> bookRepository)
        {
            _userDialog = userDialog;
            _dataService = dataService;
            _bookRepository = bookRepository;

            var books = _bookRepository.Items.Take(10).ToArray();
        }
    }
}
