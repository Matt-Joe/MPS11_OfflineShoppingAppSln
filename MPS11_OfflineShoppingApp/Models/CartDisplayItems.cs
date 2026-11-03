using System.Globalization;

namespace MPS11_OfflineShoppingApp.Models
{
    public class CartDisplayItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }

        // All monetary values are stored in cents.
        public int UnitPrice { get; set; }
        public int LineTotal { get; set; }

        public string FormattedUnitPrice =>
            (UnitPrice / 100m).ToString(
                "C2",
                CultureInfo.GetCultureInfo("en-ZA"));

        public string FormattedLineTotal =>
            (LineTotal / 100m).ToString(
                "C2",
                CultureInfo.GetCultureInfo("en-ZA"));
    }
}