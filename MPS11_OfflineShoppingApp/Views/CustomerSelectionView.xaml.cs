
using MPS11_OfflineShoppingApp.ViewModels;

namespace MPS11_OfflineShoppingApp.Views;

public partial class CustomerSelectionView
    : BaseView<CustomerSelectionViewModel>
{
    public CustomerSelectionView(
        CustomerSelectionViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
