
using MPS11_OfflineShoppingApp.ViewModels;

namespace MPS11_OfflineShoppingApp.Views;

public partial class ProductDetailsView
    : BaseView<ProductDetailsViewModel>
{
    public ProductDetailsView(ProductDetailsViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
