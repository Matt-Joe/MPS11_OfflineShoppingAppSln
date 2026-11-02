
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
    }
}
