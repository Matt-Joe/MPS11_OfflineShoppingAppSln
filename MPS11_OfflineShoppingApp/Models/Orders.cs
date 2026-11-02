using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS11_OfflineShoppingApp.Models
{
    [Table("Orders")]
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        [Column("OrdersId")]
        public int OrderId { get; set; }

        [NotNull]
        public int CustomerId { get; set; }

        [NotNull]
        public DateTime OrdersDate { get; set; }

        [NotNull]
        public int OrdersTotal { get; set; }
    }
}
