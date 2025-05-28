namespace VPPShop.Models
{
    public class CartViewModel
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
        public int Quantity { get; set; }
        public double? Total => Quantity * DiscountedPrice;
    }
    public class CartIconViewModel
    {
        public int Quantity { get; set; }
        public double? Total { get; set; }
    }
}
