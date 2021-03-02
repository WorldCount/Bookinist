using Bookinist.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;
using Bookinist.Data;
using Bookinist.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist
{
    public partial class App
    {
        private static IHost _host;

        #region Свойства

        public static bool IsDisignTime { get; private set; } = true;

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        public static Window ActiveWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static IHost Host => _host
            ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddDatabase(host.Configuration.GetSection("Database"))
            .AddServices()
            .AddViewModels();

        #endregion

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDisignTime = false;

            var host = Host;

            using (var scope = Services.CreateScope())
            {
                await scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync();
            }

            base.OnStartup(e);
            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            using (Host) await Host.StopAsync().ConfigureAwait(false);
        }
    }
}
