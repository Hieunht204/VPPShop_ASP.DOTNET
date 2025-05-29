using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace VPPShop.Models
{
    public class AdminAddProductViewModel
    {
        public string? ProductId { get; set; } 

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm tối đa 100 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }

        [Display(Name = "Tên thay thế (Alias)")]
        [StringLength(100)]
        public string? AliasName { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        [Display(Name = "Tên danh mục")]
        public string SelectedCategoryId { get; set; }

        [Display(Name = "Đơn vị tính")]
        [StringLength(50)]
        public string? UnitDescription { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số không âm")]
        [Display(Name = "Giá")]
        public double UnitPrice { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Ngày sản xuất là bắt buộc")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sản xuất")]
        [CustomValidation(typeof(AdminAddProductViewModel), nameof(ValidateDOB))]
        public DateTime ManufactureDate { get; set; }

        [Range(0, 1, ErrorMessage = "Giảm giá phải từ 0 đến 1")]
        [Display(Name = "Giảm giá (%)")]
        public double Discount { get; set; }

        [Display(Name = "Lượt xem")]
        public int ViewCount { get; set; } = 0;

        [Display(Name = "Mô tả")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Nhà cung cấp là bắt buộc")]
        [Display(Name = "Tên nhà cung cấp")]
        public string? SelectedSupplierId { get; set; }

        public static ValidationResult ValidateDOB(DateTime? dob, ValidationContext context)
        {
            if (dob != null && dob > DateTime.Today)
            {
                return new ValidationResult("Ngày sản xuất không được vượt quá hiện tại");
            }
            return ValidationResult.Success;
        }

        public List<SelectListItem>? CategoryOptions { get; set; }
        public List<SelectListItem>? SupplierOptions { get; set; }
    }

    public class AdminEditProductViewModel
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm tối đa 100 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }

        [Display(Name = "Tên thay thế (Alias)")]
        [StringLength(100)]
        public string? AliasName { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        [Display(Name = "Tên danh mục")]
        public string SelectedCategoryId { get; set; }

        [Display(Name = "Đơn vị tính")]
        [StringLength(50)]
        public string? UnitDescription { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số không âm")]
        [Display(Name = "Giá")]
        public double UnitPrice { get; set; }

        //[Display(Name = "Hình ảnh")]
        //public string? Image { get; set; }

        [Required(ErrorMessage = "Ngày sản xuất là bắt buộc")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sản xuất")]
        [CustomValidation(typeof(AdminEditProductViewModel), nameof(ValidateManufactureDate))]
        public DateTime ManufactureDate { get; set; }

        [Range(0, 1, ErrorMessage = "Giảm giá phải từ 0 đến 1")]
        [Display(Name = "Giảm giá (%)")]
        public double Discount { get; set; }

        [Display(Name = "Lượt xem")]
        public int ViewCount { get; set; } = 0;

        [Display(Name = "Mô tả")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Nhà cung cấp là bắt buộc")]
        [Display(Name = "Tên nhà cung cấp")]
        public string? SelectedSupplierId { get; set; }

        public List<SelectListItem>? CategoryOptions { get; set; }
        public List<SelectListItem>? SupplierOptions { get; set; }

        public static ValidationResult ValidateManufactureDate(DateTime? date, ValidationContext context)
        {
            if (date != null && date > DateTime.Today)
            {
                return new ValidationResult("Ngày sản xuất không được vượt quá hiện tại");
            }
            return ValidationResult.Success;
        }
    }
}
