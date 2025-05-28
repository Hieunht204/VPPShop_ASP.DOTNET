using VPPShop.Entities;

namespace VPPShop.Models
{
    public class ProductCardVM
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double UnitPrice { get; set; }
        public string? Image { get; set; }
        public double Discount { get; set; }

        //Giá sau khi giảm
        public double DiscountedPrice => UnitPrice * (1 - Discount);

        //Phần trăm giảm
        public int DiscountPercent => (int)(Discount * 100);
    }

    public class ProductListVM
    {
        public IEnumerable<ProductCardVM> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int DisplayNumber { get; set; }
        public string? Keyword { get; set; }
        public string? CategoryId { get; set; }
    }

    public class ProductDetailsVM
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UnitDescription { get; set; }
        public double UnitPrice { get; set; }
        public string? Image { get; set; }
        public DateTime ManufactureDate { get; set; }
        public double Discount { get; set; }
        public int ViewCount { get; set; }
        public string SupplierId { get; set; }
        public string SupplierCompanyName { get; set; }

        //Giá sau khi giảm
        public double DiscountedPrice => UnitPrice * (1 - Discount);

        //Phần trăm giảm
        public int DiscountPercent => (int)(Discount * 100);

        public string? Description { get; set; }
    }

    public class HomePageViewModel
    {
        public List<ProductCardVM> FeaturedProducts { get; set; }
        public List<ProductCardVM> NewestProducts { get; set; }
        public List<ProductCardVM> OfferProducts { get; set; }
    }
}
