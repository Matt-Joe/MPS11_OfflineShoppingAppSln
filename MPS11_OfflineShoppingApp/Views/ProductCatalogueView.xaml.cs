
using MPS11_OfflineShoppingApp.ViewModels;

namespace MPS11_OfflineShoppingApp.Views;

public partial class ProductCatalogueView
    : BaseView<ProductCatalogueViewModel>
{
    public ProductCatalogueView(ProductCatalogueViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
