using Microsoft.Extensions.Logging;
using MPS11_OfflineShoppingApp.Services;
using MPS11_OfflineShoppingApp.ViewModels;
using MPS11_OfflineShoppingApp.Views;

namespace MPS11_OfflineShoppingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<AppShell>();

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddSingleton<App>();

            builder.Services.AddTransient<CustomerSelectionViewModel>();

            builder.Services.AddTransient<CustomerSelectionView>();

            builder.Services.AddSingleton<CustomerSessionService>();

            builder.Services.AddTransient<ProductCatalogueViewModel>();

            builder.Services.AddTransient<ProductCatalogueView>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
