using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS11_OfflineShoppingApp.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [PrimaryKey, AutoIncrement]
        public int OrderItemId { get; set; }

        [NotNull]
        public int ProductId { get; set; }

        [NotNull]
        public int OrderId { get; set; }

        [NotNull]
        public int PricePaid { get; set; }

        [NotNull]
        public int Quantity { get; set; }
    }
}
