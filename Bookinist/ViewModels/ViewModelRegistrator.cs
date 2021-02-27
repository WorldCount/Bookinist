using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
        ;
    }
}
