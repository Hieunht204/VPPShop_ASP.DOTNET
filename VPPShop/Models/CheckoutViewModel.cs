namespace VPPShop.Models
{
    public class CheckoutViewModel
    {
        public bool SamePerson { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
    }
}
