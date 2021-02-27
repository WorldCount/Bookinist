using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bookinist.DB.Context;
using Bookinist.DB.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bookinist.Data
{
    internal class DbInitializer
    {
        private readonly BookinistDb _db;
        private readonly ILogger<DbInitializer> _logger;

        #region Данные для инициализации тестовых данных

        private const int CategoriesCount = 10;
        private Category[] _categories;

        private const int BooksCount = 10;
        private Book[] _books;

        private const int BuyersCount = 10;
        private Buyer[] _buyers;

        private const int SellersCount = 10;
        private Seller[] _sellers;

        private const int DealsCount = 1000;

        #endregion


        public DbInitializer(BookinistDb db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация БД...");

            _logger.LogInformation("Удаление существующей БД...");
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);

            //_db.Database.EnsureCreated();

            _logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync();
            _logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

            if (await _db.Books.AnyAsync()) return;

            await InitializeCategoryes();
            await InitializeBooks();
            await InitializeSellers();
            await InitializeBuyers();
            await InitializeDeals();

            _logger.LogInformation("Инициализация БД выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private async Task InitializeCategoryes()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация категорий...");

            _categories = Enumerable.Range(1, CategoriesCount)
                .Select(i => new Category
                {
                    Name = $"Категория {i}"
                })
                .ToArray();

            await _db.Categories.AddRangeAsync(_categories);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация категорий выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private async Task InitializeBooks()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация книг...");

            var rnd = new Random();

            _books = Enumerable.Range(1, BooksCount)
                .Select(i => new Book
                {
                    Name = $"Книга {i}",
                    Category = rnd.NextItem(_categories)
                })
                .ToArray();

            await _db.Books.AddRangeAsync(_books);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация книг выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private async Task InitializeBuyers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация покупателей...");

            _buyers = Enumerable.Range(1, BuyersCount)
                .Select(i => new Buyer
                {
                    Name = $"Покупатель-Имя {i}",
                    Surname = $"Покупатель-Фамилия {i}",
                    Patronymic = $"Покупатель-Отчество {i}"
                })
                .ToArray();

            await _db.Buyers.AddRangeAsync(_buyers);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация покупателей выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private async Task InitializeSellers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация продавцов...");

            _sellers = Enumerable.Range(1, SellersCount)
                .Select(i => new Seller
                {
                    Name = $"Продавец-Имя {i}",
                    Surname = $"Продавец-Фамилия {i}",
                    Patronymic = $"Продавец-Отчество {i}"
                })
                .ToArray();

            await _db.Sellers.AddRangeAsync(_sellers);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация продавцов выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private async Task InitializeDeals()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация сделок...");

            var rnd = new Random();

            var deals = Enumerable.Range(1, DealsCount)
                .Select(i => new Deal
                {
                    Book = rnd.NextItem(_books),
                    Seller = rnd.NextItem(_sellers),
                    Buyer = rnd.NextItem(_buyers),
                    Price = (decimal) (rnd.NextDouble() * 4000 + 700)
                });

            await _db.Deals.AddRangeAsync(deals);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация сделок выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }                                   
    }                                       
}
