using System.Collections.Generic;

namespace VPPShop.Models
{
    public class FavoriteViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string? Image { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }

        //Giá sau khi giảm
        public double DiscountedPrice => UnitPrice * (1 - Discount);

        //Phần trăm giảm
        public int DiscountPercent => (int)(Discount * 100);
    }

    public class FavoriteIconViewModel
    {
        public int Count { get; set; }
    }
}
