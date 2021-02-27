using System;
using Bookinist.DB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<BookinistDb>(opt =>
            {
                var type = configuration["Type"];

                switch (type)
                {
                    case null: throw new InvalidOperationException("Не определен тип БД");

                    default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");

                    case "MSSQL":
                        opt.UseSqlServer(configuration.GetConnectionString(type));
                        break;

                    case "SQLite":
                        opt.UseSqlite(configuration.GetConnectionString(type));
                        break;

                    case "InMemory":
                        opt.UseInMemoryDatabase("Bookinist");
                        break;
                }
            })
            .AddTransient<DbInitializer>()
        ;
    }
}
