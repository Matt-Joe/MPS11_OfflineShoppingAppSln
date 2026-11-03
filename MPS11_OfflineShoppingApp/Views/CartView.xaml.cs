using MPS11_OfflineShoppingApp.ViewModels;

namespace MPS11_OfflineShoppingApp.Views;

public partial class CartView : BaseView<CartViewModel>
{
    public CartView(CartViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    private async void OnRemoveClicked(
        object sender,
        EventArgs e)
    {
        if (sender is Button button
            && button.CommandParameter is int cartItemId)
        {
            await ViewModel.RemoveCartItemAsync(cartItemId);
        }
    }
}
