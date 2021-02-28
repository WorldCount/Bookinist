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
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Seller> _sellerRepository;
        private readonly IRepository<Buyer> _buyerRepository;
        private readonly ISalesService _salesService;

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
