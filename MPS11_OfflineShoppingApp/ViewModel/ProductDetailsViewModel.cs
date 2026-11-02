using MPS11_OfflineShoppingApp.Commands;
using MPS11_OfflineShoppingApp.Models;
using MPS11_OfflineShoppingApp.Services;
using System.Diagnostics;
using System.Globalization;

namespace MPS11_OfflineShoppingApp.ViewModels
{
    public class ProductDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly DatabaseService databaseService;

        private Product? product;
        private string categoryName = string.Empty;
        private int quantity = 1;
        private bool isLoading;
        private string statusMessage = string.Empty;

        public Product? Product
        {
            get => product;
            set
            {
                if (SetProperty(ref product, value))
                {
                    OnPropertyChanged(nameof(FormattedPrice));
                    IncreaseQuantityCommand.RaiseCanExecuteChanged();
                    DecreaseQuantityCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string CategoryName
        {
            get => categoryName;
            set => SetProperty(ref categoryName, value);
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (SetProperty(ref quantity, value))
                {
                    OnPropertyChanged(nameof(TotalPrice));
                    IncreaseQuantityCommand.RaiseCanExecuteChanged();
                    DecreaseQuantityCommand.RaiseCanExecuteChanged();
                }
            }
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

        public string FormattedPrice =>
            Product == null
                ? "R 0.00"
                : (Product.Price / 100m).ToString(
                    "C2",
                    CultureInfo.GetCultureInfo("en-ZA"));

        public string TotalPrice =>
            Product == null
                ? "R 0.00"
                : ((Product.Price * Quantity) / 100m).ToString(
                    "C2",
                    CultureInfo.GetCultureInfo("en-ZA"));

        public AsyncCommand IncreaseQuantityCommand { get; }

        public AsyncCommand DecreaseQuantityCommand { get; }

        public ProductDetailsViewModel(DatabaseService databaseService)
        {
            this.databaseService = databaseService;

            IncreaseQuantityCommand = new AsyncCommand(
                IncreaseQuantityAsync,
                CanIncreaseQuantity);

            DecreaseQuantityCommand = new AsyncCommand(
                DecreaseQuantityAsync,
                CanDecreaseQuantity);
        }

        public void ApplyQueryAttributes(
            IDictionary<string, object> query)
        {
            if (query.TryGetValue("ProductId", out object? value)
                && int.TryParse(value?.ToString(), out int productId))
            {
                _ = LoadProductAsync(productId);
            }
        }

        private async Task LoadProductAsync(int productId)
        {
            try
            {
                IsLoading = true;
                StatusMessage = string.Empty;

                Product? selectedProduct =
                    await databaseService.GetProductByIdAsync(productId);

                if (selectedProduct == null)
                {
                    StatusMessage = "Product could not be found.";
                    return;
                }

                Product = selectedProduct;

                Category? category =
                    await databaseService.GetCategoryByIdAsync(
                        selectedProduct.CategoryId);

                CategoryName = category?.Name ?? "Uncategorised";

                Quantity = 1;

                IncreaseQuantityCommand.RaiseCanExecuteChanged();
                DecreaseQuantityCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading product: {ex}");
                StatusMessage = "Unable to load product details.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private Task IncreaseQuantityAsync()
        {
            if (CanIncreaseQuantity())
            {
                Quantity++;
            }

            return Task.CompletedTask;
        }

        private Task DecreaseQuantityAsync()
        {
            if (CanDecreaseQuantity())
            {
                Quantity--;
            }

            return Task.CompletedTask;
        }

        private bool CanIncreaseQuantity()
        {
            return Product != null && Quantity < Product.Stock;
        }

        private bool CanDecreaseQuantity()
        {
            return Quantity > 1;
        }
    }
}
