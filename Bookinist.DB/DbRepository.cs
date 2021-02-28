using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bookinist.DB.Context;
using Bookinist.DB.Entityes.Base;
using Bookinist.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.DB
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly BookinistDb _db;
        private readonly DbSet<T> _set;

        public bool AutoSaveChanges { get; set; } = true;

        public DbRepository(BookinistDb db) 
        {
            _db = db;
            _set = db.Set<T>();
        }

        public virtual IQueryable<T> Items => _set;

        public T Get(int id)
        {
            return Items.SingleOrDefault(item => item.Id == id);
        }

        public async Task<T> GetAsync(int id, CancellationToken cancel = default)
        {
            return await Items
                .SingleOrDefaultAsync(item => item.Id == id, cancel)
                .ConfigureAwait(false);
        }

        public T Add(T item)
        {
            if(item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Added;
            if(AutoSaveChanges)
                _db.SaveChanges();

            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

            return item;
        }

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }

        public void Remove(int id)
        {
            //var item = Get(id);

            //if(item is null) return;

            //_db.Entry(item).State = EntityState.Deleted;

            _db.Remove(new T {Id = id});

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken cancel = default)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }
    }
}
