using Bookinist.DB.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.DB.Context
{
    public class BookinistDb : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Deal> Deals { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public BookinistDb(DbContextOptions<BookinistDb> options) : base(options) { }
    }
}
