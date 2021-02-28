using Bookinist.DB.Entityes;
using Bookinist.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.DB.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Book>, BooksRepository>()
            .AddTransient<IRepository<Category>, DbRepository<Category>>()
            .AddTransient<IRepository<Buyer>, DbRepository<Buyer>>()
            .AddTransient<IRepository<Seller>, DbRepository<Seller>>()
            .AddTransient<IRepository<Deal>, DealsRepository>()
        ;
    }
}
