using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS11_OfflineShoppingApp.Models
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int CategoryId { get; set; }

        [NotNull]
        public string Name { get; set; } = string.Empty;
    }
}
