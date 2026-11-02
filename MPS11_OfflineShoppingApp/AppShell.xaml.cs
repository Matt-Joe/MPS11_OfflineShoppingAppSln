using MPS11_OfflineShoppingApp.Views;

namespace MPS11_OfflineShoppingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(
            "product-catalogue",
            typeof(ProductCatalogueView));
        }
    }
}
