using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS11_OfflineShoppingApp.Models
{
    [Table("Customer")]
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int CustomerId { get; set; }

        [NotNull]
        public string Name { get; set; } = string.Empty;

        [NotNull]
        public string Surname { get; set; } = string.Empty;
    }
}
