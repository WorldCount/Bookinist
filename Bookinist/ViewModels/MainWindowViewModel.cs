using System;
using System.Linq;
using System.Windows.Input;
using Bookinist.DB.Entityes;
using Bookinist.Infrastructure.Commands;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Приватные поля

        private readonly IUserDialog _userDialog;
        private readonly ISalesService _salesService;

        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Seller> _sellerRepository;
        private readonly IRepository<Buyer> _buyerRepository;

        #endregion

        #region Свойства

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

        #region CurrentModel : ViewModel - Текущая дочерная модель-представления

        /// <summary>Текущая дочерная модель-представления</summary>
        private ViewModel _currentModel;

        /// <summary>Текущая дочерная модель-представления</summary>
        public ViewModel CurrentModel
        {
            get => _currentModel;
            set => Set(ref _currentModel, value);
        }

        #endregion

        #endregion

        #region Команды

        #region Отобразить представление книг

        private ICommand _showBooksViewCommand;

        /// <summary>Отобразить представление книг</summary>
        public ICommand ShowBooksViewCommand => _showBooksViewCommand ??= new LambdaCommand(OnShowBooksViewCommandExecuted, CanShowBooksViewCommandExecute);

        private bool CanShowBooksViewCommandExecute(object p) => true;

        private void OnShowBooksViewCommandExecuted(object p)
        {
            CurrentModel = new BooksViewModel(_bookRepository);
        }

        #endregion

        #region Отобразить представление покупателей

        private ICommand _showBuyersViewCommand;

        /// <summary>Отобразить представление покупателей</summary>
        public ICommand ShowBuyersViewCommand => _showBuyersViewCommand ??= new LambdaCommand(OnShowBuyersViewCommandExecuted, CanShowBuyersViewCommandExecute);

        private bool CanShowBuyersViewCommandExecute(object p) => true;

        private void OnShowBuyersViewCommandExecuted(object p)
        {
            CurrentModel = new BuyersViewModel(_buyerRepository);
        }

        #endregion

        #region Отобразить представление статистики

        private ICommand _showStatisticsViewCommand;

        /// <summary>Отобразить представление статистики</summary>
        public ICommand ShowStatisticsViewCommand => _showStatisticsViewCommand ??= new LambdaCommand(OnShowStatisticsViewCommandExecuted, CanShowStatisticsViewCommandExecute);

        private bool CanShowStatisticsViewCommandExecute(object p) => true;

        private void OnShowStatisticsViewCommandExecuted(object p)
        {
            CurrentModel = new StatisticsViewModel(
                _bookRepository, _buyerRepository, _sellerRepository
                );
        }

        #endregion

        #endregion



        public MainWindowViewModel(
            IUserDialog userDialog, 
            IRepository<Book> bookRepository,
            IRepository<Seller> sellerRepository, 
            IRepository<Buyer> buyerRepository, 
            ISalesService salesService)
        {
            _userDialog = userDialog;
            _bookRepository = bookRepository;
            _sellerRepository = sellerRepository;
            _buyerRepository = buyerRepository;
            _salesService = salesService;

            Test();
        }

        private async void Test()
        {
            var dealsCount = _salesService.Deals.Count();

            var book = _bookRepository.Get(5);
            var buyer = _buyerRepository.Get(3);
            var seller = _sellerRepository.Get(7);

            var deal = await _salesService.MakeADealAsync(book.Name, seller, buyer, 100m).ConfigureAwait(false);

            var dealsCount2 = _salesService.Deals.Count();
        }
    }
}
