using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bookinist.DB.Entityes;
using Bookinist.Infrastructure.Commands;
using Bookinist.Interfaces;
using Bookinist.Models;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.ViewModels
{
    internal class StatisticsViewModel : ViewModel
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Buyer> _buyerRepository;
        private readonly IRepository<Seller> _sellerRepository;
        private readonly IRepository<Deal> _dealRepository;

        #region Свойства

        #region BooksCount : int - Количество книг

        /// <summary>Количество книг</summary>
        private int _booksCount;

        /// <summary>Количество книг</summary>
        public int BooksCount
        {
            get => _booksCount;
            set => Set(ref _booksCount, value);
        }

        #endregion

        public ObservableCollection<BestSellerInfo> BestSellers = new ObservableCollection<BestSellerInfo>();

        #endregion

        #region Команды

        #region Вычислить статистические данные

        private ICommand _computeStatisticCommand;

        /// <summary>Вычислить статистические данные</summary>
        public ICommand ComputeStatisticCommand => _computeStatisticCommand ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted);

        private async Task OnComputeStatisticCommandExecuted(object p)
        {
            BooksCount = await _bookRepository.Items.CountAsync();

            await ComputedDealsStatisticAsync();
        }

        private async Task ComputedDealsStatisticAsync()
        {
            var bestSellerQuery = _dealRepository.Items
                .GroupBy(b => b.Book.Id)
                .Select(deals => new
                {
                    BookId = deals.Key,
                    Count = deals.Count()
                })
                .OrderByDescending(deals => deals.Count)
                .Take(5)
                .Join(_bookRepository.Items,
                    deals => deals.BookId,
                    book => book.Id,
                    (deals, book) => new BestSellerInfo { Book = book, SellCount = deals.Count });

            BestSellers.Clear();
            foreach (var bestSellerInfo in await bestSellerQuery.ToArrayAsync())
                BestSellers.Add(bestSellerInfo);
        }

        #endregion

        #endregion

        

        public StatisticsViewModel(IRepository<Book> bookRepository, IRepository<Buyer> buyerRepository, 
            IRepository<Seller> sellerRepository, IRepository<Deal> dealRepository)
        {
            _bookRepository = bookRepository;
            _buyerRepository = buyerRepository;
            _sellerRepository = sellerRepository;
            _dealRepository = dealRepository;
        }
    }
}
