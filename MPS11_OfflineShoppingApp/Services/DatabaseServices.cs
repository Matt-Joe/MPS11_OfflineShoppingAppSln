
using MPS11_OfflineShoppingApp.Models;
using SQLite;

namespace MPS11_OfflineShoppingApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? database;

        private SQLiteAsyncConnection Database =>
            database ?? throw new InvalidOperationException(
                "Database has not been initialized.");

        // Initialise the database and copy it to writable storage.
        public async Task InitializeAsync()
        {
            string databasePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "OfflineStoreDb.db");

            if (!File.Exists(databasePath))
            {
                using Stream source =
                    await FileSystem.OpenAppPackageFileAsync(
                        "OfflineStoreDb.db");

                using FileStream destination =
                    File.Create(databasePath);

                await source.CopyToAsync(destination);
            }

            database = new SQLiteAsyncConnection(databasePath);

            // Enable foreign key enforcement for this connection.
            await Database.ExecuteAsync(
                "PRAGMA foreign_keys = ON;");
        }

        // Retrieve all customers.
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await Database
                .Table<Customer>()
                .ToListAsync();
        }

        // Retrieve a customer using their ID.
        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await Database
                .Table<Customer>()
                .Where(customer => customer.CustomerId == customerId)
                .FirstOrDefaultAsync();
        }

        // Retrieve all products.
        public async Task<List<Product>> GetProductsAsync()
        {
            return await Database
                .Table<Product>()
                .ToListAsync();
        }

        // Retrieve a product using its ID.
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await Database
                .Table<Product>()
                .Where(product => product.ProductId == productId)
                .FirstOrDefaultAsync();
        }

        // Retrieve all categories.
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await Database
                .Table<Category>()
                .ToListAsync();
        }

        // Retrieve a category using its ID.
        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await Database
                .Table<Category>()
                .Where(category => category.CategoryId == categoryId)
                .FirstOrDefaultAsync();
        }

        // Add a product to a customer's cart.
        // If the product is already in the cart, increase its quantity.
        public async Task<(bool Success, string Message)> AddToCartAsync(
            int customerId,
            int productId,
            int quantity)
        {
            if (quantity <= 0)
            {
                return (false, "Quantity must be greater than zero.");
            }

            // Confirm that the customer exists.
            Customer? customer =
                await GetCustomerByIdAsync(customerId);

            if (customer == null)
            {
                return (false, "The selected customer could not be found.");
            }

            // Retrieve the product from the database.
            Product? product =
                await GetProductByIdAsync(productId);

            if (product == null)
            {
                return (false, "The selected product could not be found.");
            }

            if (product.Stock <= 0)
            {
                return (false, "This product is currently out of stock.");
            }

            // Check whether this customer already has this product in the cart.
            CartItem? existingCartItem =
                await Database
                    .Table<CartItem>()
                    .Where(item =>
                        item.CustomerId == customerId &&
                        item.ProductId == productId)
                    .FirstOrDefaultAsync();

            int currentQuantity = existingCartItem?.Quantity ?? 0;
            int updatedQuantity = currentQuantity + quantity;

            // Ensure the total cart quantity does not exceed stock.
            if (updatedQuantity > product.Stock)
            {
                return (
                    false,
                    $"Only {product.Stock} unit(s) are available. " +
                    $"Your cart already contains {currentQuantity}."
                );
            }

            // Prices and line totals are stored in cents.
            int updatedLineTotal = product.Price * updatedQuantity;

            if (existingCartItem != null)
            {
                // Update the existing cart row.
                existingCartItem.Quantity = updatedQuantity;
                existingCartItem.LineTotal = updatedLineTotal;

                await Database.UpdateAsync(existingCartItem);
            }
            else
            {
                // Create a new cart row for this customer and product.
                CartItem newCartItem = new CartItem
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    Quantity = quantity,
                    LineTotal = product.Price * quantity
                };

                await Database.InsertAsync(newCartItem);
            }

            return (true, "Product added to your cart.");
        }

        // Retrieve a customer's cart and combine cart rows with product details.
        public async Task<List<CartDisplayItem>> GetCartItemsAsync(
            int customerId)
        {
            List<CartItem> cartItems = await Database
                .Table<CartItem>()
                .Where(item => item.CustomerId == customerId)
                .ToListAsync();

            List<Product> products = await GetProductsAsync();

            Dictionary<int, Product> productLookup = products
                .ToDictionary(product => product.ProductId);

            List<CartDisplayItem> displayItems = new();

            foreach (CartItem cartItem in cartItems)
            {
                if (!productLookup.TryGetValue(
                        cartItem.ProductId,
                        out Product? product))
                {
                    // Skip cart rows whose product no longer exists.
                    continue;
                }

                displayItems.Add(new CartDisplayItem
                {
                    CartItemId = cartItem.CartItemId,
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Quantity = cartItem.Quantity,
                    UnitPrice = product.Price,
                    LineTotal = cartItem.LineTotal
                });
            }

            return displayItems;
        }

        // Calculate the total for a customer's cart, in cents.
        public async Task<int> GetCartTotalAsync(int customerId)
        {
            List<CartItem> cartItems = await Database
                .Table<CartItem>()
                .Where(item => item.CustomerId == customerId)
                .ToListAsync();

            return cartItems.Sum(item => item.LineTotal);
        }

        // Remove one cart item belonging to the specified customer.
        public async Task<bool> RemoveCartItemAsync(
            int customerId,
            int cartItemId)
        {
            CartItem? cartItem = await Database
                .Table<CartItem>()
                .Where(item =>
                    item.CartItemId == cartItemId &&
                    item.CustomerId == customerId)
                .FirstOrDefaultAsync();

            if (cartItem == null)
            {
                return false;
            }

            await Database.DeleteAsync(cartItem);

            return true;
        }
    }
}
