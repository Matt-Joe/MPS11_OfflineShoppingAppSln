using Microsoft.Extensions.Logging;
using MPS11_OfflineShoppingApp.Services;

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
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
