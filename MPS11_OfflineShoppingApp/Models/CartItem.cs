using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS11_OfflineShoppingApp.Models
{
    [Table("CartItem")]
    public class CartItem
    {
        [PrimaryKey, AutoIncrement]
        public int CartItemId { get; set; }

        [NotNull]
        public int ProductId { get; set; }

        [NotNull]
        public int CustomerId { get; set; }

        [NotNull]
        public int Quantity { get; set; }

        [NotNull]
        public int LineTotal { get; set; }
    }
}
