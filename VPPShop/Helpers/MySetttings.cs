namespace VPPShop.Helpers
{
    public class MySetttings
    {
        public static string CART_KEY = "MYCART";
        public static string CLAIM_CUSTOMERID = "CustomerId"; 
        public static string UploadImage(IFormFile img, string folder)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    "img", folder, img.FileName);
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    img.CopyTo(myfile);
                }
                return img.FileName;
            } catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
