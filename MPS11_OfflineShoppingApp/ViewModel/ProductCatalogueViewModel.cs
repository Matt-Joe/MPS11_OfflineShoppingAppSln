
using MPS11_OfflineShoppingApp.Models;
using MPS11_OfflineShoppingApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MPS11_OfflineShoppingApp.ViewModels
{
    public class ProductCatalogueViewModel : BaseViewModel
    {
        private readonly DatabaseService databaseService;
        private readonly CustomerSessionService customerSessionService;
        private readonly INavigationService navigationService;

        private ObservableCollection<ProductCatalogueItem> products = new();
        private ProductCatalogueItem? selectedProduct;
        private bool isLoading;

        public ObservableCollection<ProductCatalogueItem> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public ProductCatalogueItem? SelectedProduct
        {
            get => selectedProduct;
            set
            {
                if (SetProperty(ref selectedProduct, value)
                    && value != null)
                {
                    _ = OpenProductDetailsAsync(value);
                }
            }
        }

        public string CustomerName =>
            customerSessionService.SelectedCustomer is Customer customer
                ? $"{customer.Name} {customer.Surname}"
                : "No customer selected";

        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public ProductCatalogueViewModel(
            DatabaseService databaseService,
            CustomerSessionService customerSessionService,
            INavigationService navigationService)
        {
            this.databaseService = databaseService;
            this.customerSessionService = customerSessionService;
            this.navigationService = navigationService;

            _ = LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                IsLoading = true;

                List<Product> productList =
                    await databaseService.GetProductsAsync();

                List<Category> categoryList =
                    await databaseService.GetCategoriesAsync();

                Dictionary<int, string> categoryLookup =
                    categoryList.ToDictionary(
                        category => category.CategoryId,
                        category => category.Name);

                List<ProductCatalogueItem> catalogueItems =
                    productList.Select(product =>
                        new ProductCatalogueItem
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            CategoryName = categoryLookup.TryGetValue(
                                product.CategoryId,
                                out string? categoryName)
                                    ? categoryName
                                    : "Uncategorised",
                            Price = product.Price,
                            Stock = product.Stock
                        }).ToList();

                Products = new ObservableCollection<ProductCatalogueItem>(
                    catalogueItems);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading products: {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task OpenProductDetailsAsync(
            ProductCatalogueItem product)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "ProductId", product.ProductId }
                };

                await navigationService.GoToAsync(
                    "product-details",
                    parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"Error opening product details: {ex}");
            }
        }
    }
}
