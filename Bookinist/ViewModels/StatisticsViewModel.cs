using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bookinist.DB.Entityes;
using Bookinist.Infrastructure.Commands;
using Bookinist.Interfaces;
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

        #endregion

        #region Команды

        #region Вычислить статистические данные

        private ICommand _computeStatisticCommand;

        /// <summary>Вычислить статистические данные</summary>
        public ICommand ComputeStatisticCommand => _computeStatisticCommand ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted, CanComputeStatisticCommandExecute);

        private bool CanComputeStatisticCommandExecute(object p) => true;

        private async Task OnComputeStatisticCommandExecuted(object p)
        {
            BooksCount = await _bookRepository.Items.CountAsync();

            var deals = _dealRepository.Items.Include(i => i.Book);
            var bestSeller = await deals.GroupBy(deal => deal.Book)
                .Select(bookDeals => new
                {
                    Book = bookDeals.Key, 
                    Count = bookDeals.Count()
                })
                .OrderByDescending(book => book.Count)
                .Take(5)
                .ToArrayAsync();
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
