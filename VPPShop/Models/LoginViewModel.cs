using System.ComponentModel.DataAnnotations;

namespace VPPShop.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
