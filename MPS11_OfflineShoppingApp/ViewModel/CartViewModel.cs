using MPS11_OfflineShoppingApp.Commands;
using MPS11_OfflineShoppingApp.Models;
using MPS11_OfflineShoppingApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace MPS11_OfflineShoppingApp.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private readonly DatabaseService databaseService;
        private readonly CustomerSessionService customerSessionService;
        private readonly INavigationService navigationService;

        private ObservableCollection<CartDisplayItem> items = new();
        private bool isLoading;
        private string statusMessage = string.Empty;
        private int cartTotal;

        public ObservableCollection<CartDisplayItem> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public string StatusMessage
        {
            get => statusMessage;
            set => SetProperty(ref statusMessage, value);
        }

        public int CartTotal
        {
            get => cartTotal;
            set
            {
                if (SetProperty(ref cartTotal, value))
                {
                    OnPropertyChanged(nameof(FormattedCartTotal));
                }
            }
        }

        public string FormattedCartTotal =>
            (CartTotal / 100m).ToString(
                "C2",
                CultureInfo.GetCultureInfo("en-ZA"));

        public string CustomerName =>
            customerSessionService.SelectedCustomer is Customer customer
                ? $"{customer.Name} {customer.Surname}"
                : "No customer selected";

        public AsyncCommand LoadCartCommand { get; }
        public AsyncCommand RemoveItemCommand { get; }
        public AsyncCommand ContinueShoppingCommand { get; }

        public CartViewModel(
            DatabaseService databaseService,
            CustomerSessionService customerSessionService,
            INavigationService navigationService)
        {
            this.databaseService = databaseService;
            this.customerSessionService = customerSessionService;
            this.navigationService = navigationService;

            LoadCartCommand = new AsyncCommand(LoadCartAsync);
            RemoveItemCommand = new AsyncCommand(RemoveItemAsync);
            ContinueShoppingCommand =
                new AsyncCommand(ContinueShoppingAsync);

            _ = LoadCartAsync();
        }

        private async Task LoadCartAsync()
        {
            Customer? customer =
                customerSessionService.SelectedCustomer;

            if (customer == null)
            {
                Items.Clear();
                CartTotal = 0;
                StatusMessage = "Please select a customer first.";
                return;
            }

            try
            {
                IsLoading = true;
                StatusMessage = string.Empty;

                List<CartDisplayItem> cartItems =
                    await databaseService.GetCartItemsAsync(
                        customer.CustomerId);

                Items = new ObservableCollection<CartDisplayItem>(
                    cartItems);

                CartTotal =
                    await databaseService.GetCartTotalAsync(
                        customer.CustomerId);

                if (Items.Count == 0)
                {
                    StatusMessage = "Your cart is currently empty.";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading cart: {ex}");
                StatusMessage = "Unable to load your cart.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task RemoveItemAsync()
        {
            // The selected item is passed as the command parameter.
            // AsyncCommand currently does not support parameters, so
            // removal will be wired through a selected-item property.
            await Task.CompletedTask;
        }

        private async Task ContinueShoppingAsync()
        {
            await navigationService.GoBackAsync();
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            Customer? customer =
                customerSessionService.SelectedCustomer;

            if (customer == null)
            {
                StatusMessage = "Please select a customer first.";
                return;
            }

            try
            {
                bool removed =
                    await databaseService.RemoveCartItemAsync(
                        customer.CustomerId,
                        cartItemId);

                StatusMessage = removed
                    ? "Item removed from your cart."
                    : "The cart item could not be found.";

                await LoadCartAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error removing cart item: {ex}");
                StatusMessage = "Unable to remove this item.";
            }
        }
    }
}
