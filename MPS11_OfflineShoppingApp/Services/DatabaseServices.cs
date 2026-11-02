using MPS11_OfflineShoppingApp.Models;
using SQLite;

namespace MPS11_OfflineShoppingApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? database;

        // Provides safe access to the database connection.
        private SQLiteAsyncConnection Database =>
            database ?? throw new InvalidOperationException(
                "Database has not been initialized.");


        public async Task InitializeAsync()
        {
            string databasePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "OfflineStoreDb.db");

            System.Diagnostics.Debug.WriteLine(
                "STEP 1: Checking database file...");

            if (!File.Exists(databasePath))
            {
                System.Diagnostics.Debug.WriteLine(
                    "STEP 2: Database does not exist. Copying...");

                using Stream source =
                    await FileSystem.OpenAppPackageFileAsync(
                        "OfflineStoreDb.db");

                using FileStream destination =
                    File.Create(databasePath);

                await source.CopyToAsync(destination);

                System.Diagnostics.Debug.WriteLine(
                    "STEP 3: Database copied successfully.");
            }

            System.Diagnostics.Debug.WriteLine(
                "STEP 4: Opening SQLite connection...");

            database = new SQLiteAsyncConnection(databasePath);

            System.Diagnostics.Debug.WriteLine(
                "STEP 5: SQLite connection created.");

            await Database.ExecuteAsync(
                "PRAGMA foreign_keys = ON;");

            System.Diagnostics.Debug.WriteLine(
                "STEP 6: Foreign keys enabled. Initialization complete.");
        }


        // Retrieve all customers.
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await Database
                .Table<Customer>()
                .ToListAsync();
        }

        // Retrieve a customer by their ID.
        public async Task<Customer?> GetCustomerByIdAsync(
            int customerId)
        {
            return await Database
                .Table<Customer>()
                .Where(customer =>
                    customer.CustomerId == customerId)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            return await Database
                .Table<Product>()
                .ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await Database
                .Table<Category>()
                .ToListAsync();
        }
    }
}
