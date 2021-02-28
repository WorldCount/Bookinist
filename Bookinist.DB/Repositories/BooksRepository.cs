using System.Linq;
using Bookinist.DB.Context;
using Bookinist.DB.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.DB.Repositories
{
    class BooksRepository : DbRepository<Book>
    {
        public override IQueryable<Book> Items => base.Items.Include(item => item.Category);

        public BooksRepository(BookinistDb db) : base(db) { }
    }
}