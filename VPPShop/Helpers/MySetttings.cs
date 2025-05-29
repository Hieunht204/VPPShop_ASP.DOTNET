namespace VPPShop.Helpers
{
    public class MySetttings
    {
        public static string CART_KEY = "MYCART";
        public static string CLAIM_CUSTOMERID = "CustomerId";
        public const string FAVORITES_KEY = "Favorites";
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

        public static string ReplaceImage(IFormFile newImage, string folder, string oldFileName)
        {
            try
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", folder);
                var newFilePath = Path.Combine(uploadDir, newImage.FileName);

                if (!string.IsNullOrEmpty(oldFileName))
                {
                    var oldFilePath = Path.Combine(uploadDir, oldFileName);
                    if (File.Exists(oldFilePath) && oldFileName != newImage.FileName)
                    {
                        File.Delete(oldFilePath);
                    }
                }

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    newImage.CopyTo(stream);
                }

                return newImage.FileName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static void DeleteImage(string fileName, string folder)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName)) return;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", folder, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}
