namespace MPS11_OfflineShoppingApp.Models
{
    public class ProductCatalogueItem
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public int Price { get; set; }

        public int Stock { get; set; }

        public string FormattedPrice =>
        (Price / 100m).ToString("C2",
        System.Globalization.CultureInfo.GetCultureInfo("en-ZA"));
    }
}
