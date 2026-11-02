using MPS11_OfflineShoppingApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS11_OfflineShoppingApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection database;

        public DatabaseService()
        {
            string databasePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "OfflineStore.db3");

            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task InitializeAsync()
        {
            /*await database.CreateTableAsync<Customer>();
            await database.CreateTableAsync<Category>();
            await database.CreateTableAsync<Product>();
            await database.CreateTableAsync<CartItem>();
            await database.CreateTableAsync<Order>();
            await database.CreateTableAsync<OrderItem>();*/
        }
    }
}
