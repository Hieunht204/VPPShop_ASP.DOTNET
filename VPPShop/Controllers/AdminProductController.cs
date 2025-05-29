using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using VPPShop.Entities;
using VPPShop.Helpers;
using VPPShop.Models;

namespace VPPShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        private readonly TmdtContext _context;
        private readonly IMapper _mapper;

        public AdminProductController(TmdtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Index(Trang chủ)
        [HttpGet]
        public IActionResult Index(string? keyword)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Where(p => string.IsNullOrEmpty(keyword) || p.ProductName.Contains(keyword))
                .Select(p => new ProductCardVM
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    UnitPrice = (double)p.UnitPrice,
                    Image = p.Image,
                    Discount = (double)p.Discount
                })
                .ToList();
            ViewBag.Search = keyword;
            return View(products);
        }
        #endregion

        #region Edit(Chỉnh sửa thông tin sản phẩm)
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return Redirect("/404");

            var model = new AdminEditProductViewModel
            {
                ProductName = product.ProductName,
                AliasName = product.AliasName,
                UnitPrice = (double)product.UnitPrice,
                Discount = product.Discount,
                ManufactureDate = product.ManufactureDate,
                Description = product.Description,
                UnitDescription = product.UnitDescription,
                //Image = product.Image,
                SelectedCategoryId = product.CategoryId,
                SelectedSupplierId = product.SupplierId
            };

            model.CategoryOptions = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId,
                    Text = c.CategoryName
                }).ToList();

            model.SupplierOptions = _context.Suppliers
                .Select(s => new SelectListItem
                {
                    Value = s.SupplierId,
                    Text = s.CompanyName
                }).ToList();

            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(string id, AdminEditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
                    if (product == null) return Redirect("/404");

                    product.ProductName = model.ProductName;
                    product.AliasName = model.AliasName;
                    product.UnitPrice = model.UnitPrice;
                    product.Discount = model.Discount;
                    product.ManufactureDate = model.ManufactureDate;
                    product.Description = model.Description;
                    product.UnitDescription = model.UnitDescription;
                    product.SupplierId = model.SelectedSupplierId;
                    product.CategoryId = model.SelectedCategoryId;

                    //if (img != null && img.Length > 0)
                    //{
                    //    var newImage = MySetttings.UploadImage(img, "products");
                    //    if (!string.IsNullOrEmpty(newImage))
                    //    {
                    //        product.Image = newImage;
                    //    }
                    //}
                    //else
                    //{
                    //    product.Image = model.Image;
                    //}

                    _context.Update(product);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "AdminProduct");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi cập nhật sản phẩm: " + ex.Message);
                }
            }
            else
            {
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Key: {error.Key}");
                    foreach (var e in error.Value.Errors)
                    {
                        Console.WriteLine($"Error: {e.ErrorMessage}");
                    }
                }
            }

            model.SupplierOptions = _context.Suppliers.Select(s => new SelectListItem
            {
                Value = s.SupplierId.ToString(),
                Text = s.CompanyName
            }).ToList();

            model.CategoryOptions = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();


            return View(model);
        }
        #endregion


        #region Create(Thêm sản phẩm mới)
        [HttpGet]
        public IActionResult Create()
        {
            var model = new AdminAddProductViewModel
            {
                CategoryOptions = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId,
                        Text = c.CategoryName
                    }).ToList(),

                SupplierOptions = _context.Suppliers
                    .Select(s => new SelectListItem
                    {
                        Value = s.SupplierId,
                        Text = s.CompanyName
                    }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdminAddProductViewModel model, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var productIds = _context.Products
                        .Where(c => c.ProductId.StartsWith("prd"))
                        .Select(c => c.ProductId)
                        .ToList();

                    int maxNumber = productIds
                        .Select(id =>
                        {
                            var match = Regex.Match(id, @"prd(\d+)$");
                            return match.Success && int.TryParse(match.Groups[1].Value, out int n) ? n : 0;
                        })
                        .DefaultIfEmpty(0)
                        .Max();

                    string newProductId = "prd" + (maxNumber + 1);

                    var product = _mapper.Map<Product>(model);
                    product.ProductId = newProductId;
                    product.ViewCount = 0;

                    if (img != null)
                    {
                        product.Image = MySetttings.UploadImage(img, "products");
                    }

                    _context.Products.Add(product);
                    _context.SaveChanges();

                    TempData["Success"] = "Thêm sản phẩm thành công.";

                    return RedirectToAction("Index", "AdminProduct");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi thêm sản phẩm: " + ex.Message);
                }
            }
            model.SupplierOptions = _context.Suppliers.Select(s => new SelectListItem
            {
                Value = s.SupplierId.ToString(),
                Text = s.CompanyName
            }).ToList();

            model.CategoryOptions = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();

            return View(model);
        }
        #endregion


        #region Delete(Xoá sản phẩm)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm để xóa.";
                return RedirectToAction("Index");
            }

            try
            {
                MySetttings.DeleteImage(product.Image, "products");

                _context.Products.Remove(product);
                _context.SaveChanges();

                TempData["Success"] = "Đã xóa sản phẩm thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi khi xóa: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
        #endregion

    }
}
