using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bookinist.DB.Entityes;
using Bookinist.Interfaces;

namespace Bookinist.Infrastructure.DebugServices
{
    internal class DedugBooksRepository : IRepository<Book>
    {
        public IQueryable<Book> Items { get; }

        public DedugBooksRepository()
        {
            var books = Enumerable.Range(1, 100)
                .Select(i => new Book
                {
                    Id = i,
                    Name = $"Книга {i}",
                    Category = new Category { Id = 0, Name = "Категория" }

                }).ToArray();

            var categories = Enumerable.Range(1, 10)
                .Select(i => new Category
                {
                    Id = i,
                    Name = $"Категория {i}"
                })
                .ToArray();

            foreach (var book in books)
            {
                var category = categories[book.Id % categories.Length];
                category.Books.Add(book);
                book.Category = category;
            }

            Items = books.AsQueryable();
        }

        public Book Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetAsync(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Book Add(Book item)
        {
            throw new NotImplementedException();
        }

        public Task<Book> AddAsync(Book item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public void Update(Book item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Book item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
