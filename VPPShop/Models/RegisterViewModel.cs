using System.ComponentModel.DataAnnotations;

namespace VPPShop.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "* Nhập đầy đủ mật khẩu")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "* Nhập họ tên")]
        public string Fullname { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; } = true;

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(RegisterViewModel), nameof(ValidateDOB))]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Địa chỉ")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string Address { get; set; }

        [Display(Name = "Số điện thoại")]
        [MaxLength(24, ErrorMessage = "Tối đa 24 ký tự")]
        [RegularExpression(@"^0[3|5|7|8|9][0-9]{8}$", ErrorMessage = "Chưa đúng định dạng số điện thoại Việt Nam")]
        public string Phonenumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "* Nhập đầy đủ email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string Email { get; set; } 

        public string? Image { get; set; }

        public static ValidationResult ValidateDOB(DateTime? dob, ValidationContext context)
        {
            if (dob != null && dob > DateTime.Today)
            {
                return new ValidationResult("Ngày sinh không được vượt quá hiện tại");
            }
            return ValidationResult.Success;
        }
    }
}
