using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS11_OfflineShoppingApp.Models
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ProductId { get; set; }

        [NotNull]
        public int CategoryId { get; set; }

        [NotNull]
        public string Name { get; set; } = string.Empty;

        [NotNull]
        public int Price { get; set; }

        [NotNull]
        public int Stock { get; set; }
    }
}
