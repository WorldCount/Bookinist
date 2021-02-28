using System.Linq;
using Bookinist.DB.Context;
using Bookinist.DB.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.DB.Repositories
{
    class DealsRepository : DbRepository<Deal>
    {
        public override IQueryable<Deal> Items => base.Items
            .Include(item => item.Book)
            .Include(item => item.Seller)
            .Include(item => item.Buyer)
        ;

        public DealsRepository(BookinistDb db) : base(db) { }
    }
}