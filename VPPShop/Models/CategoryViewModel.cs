namespace VPPShop.Models
{
    public class CategoryCardViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Image { get; set; }
        public int Count { get; set; }
    }
    public class FilterSidebarViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
    }
}
