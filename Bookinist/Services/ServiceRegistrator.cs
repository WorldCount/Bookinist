﻿using Bookinist.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IDataService, DataService>()
            .AddTransient<IUserDialog, UserDialogService>();
    }
}
