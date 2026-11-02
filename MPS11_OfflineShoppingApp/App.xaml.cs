using MPS11_OfflineShoppingApp.Services;
using System.Diagnostics;

namespace MPS11_OfflineShoppingApp
{
    public partial class App : Application
    {
        public App(DatabaseService databaseService)
        {
            InitializeComponent();

            // Create a loading screen while the database initializes.
            ContentPage loadingPage = new ContentPage
            {
                Content = new ActivityIndicator
                {
                    IsRunning = true,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                }
            };

            MainPage = loadingPage;

            InitializeDatabaseAsync(databaseService, loadingPage);
        }

        private async void InitializeDatabaseAsync(
            DatabaseService databaseService,
            ContentPage loadingPage)
        {
            try
            {
                await databaseService.InitializeAsync();

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    MainPage = new AppShell();
                });
            }
            catch (Exception ex)
            {
                // Write the complete exception to the Debug Output window.
                Debug.WriteLine(ex.ToString());

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await loadingPage.DisplayAlert(
                        "Database Error",
                        ex.Message,
                        "OK");
                });
            }
        }
    }
}
