using Bookinist.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;
using Bookinist.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist
{
    public partial class App
    {
        private static IHost _host;

        #region Свойства

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        public static Window ActiveWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static IHost Host => _host
            ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddServices()
            .AddViewModels();

        #endregion

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;

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
