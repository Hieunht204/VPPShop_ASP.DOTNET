using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VPPShop.Entities;
using VPPShop.Models;

namespace VPPShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly TmdtContext _context;
        public ProductController(TmdtContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index(string? category, int displayNumber = 12, int page = 1)
        {
            var query = _context.Products
                .Join(_context.Categories,
                      p => p.CategoryId,
                      c => c.CategoryId,
                      (p, c) => new { Product = p, Category = c });

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(pc => pc.Category.CategoryId == category);
            }

            int totalProducts = query.Count();

            var productList = query
                .Skip((page - 1) * displayNumber)
                .Take(displayNumber)
                .Select(pc => new ProductCardVM
                {
                    ProductId = pc.Product.ProductId,
                    ProductName = pc.Product.ProductName,
                    CategoryId = pc.Category.CategoryId,
                    CategoryName = pc.Category.CategoryName,
                    Image = pc.Product.Image,
                    UnitPrice = (double)pc.Product.UnitPrice,
                    Discount = (double)pc.Product.Discount,
                })
                .ToList();

            var model = new ProductListVM
            {
                Products = productList,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalProducts / displayNumber),
                DisplayNumber = displayNumber,
                CategoryId = category
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Search(string? keyword, int displayNumber = 12, int page = 1)
        {
            var query = _context.Products
                .Join(_context.Categories,
                      p => p.CategoryId,
                      c => c.CategoryId,
                      (p, c) => new { Product = p, Category = c });

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(pc => pc.Product.ProductName.Contains(keyword));
            }

            int totalItems = query.Count();
            if (totalItems == 0)
            {
                return Redirect("/404");
            }

            int totalPages = (int)Math.Ceiling(totalItems / (double)displayNumber);

            var products = query
                .OrderByDescending(x => x.Product.ProductId)
                .Skip((page - 1) * displayNumber)
                .Take(displayNumber)
                .Select(x => new ProductCardVM
                {
                    ProductId = x.Product.ProductId,
                    ProductName = x.Product.ProductName,
                    CategoryId = x.Category.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    Image = x.Product.Image,
                    UnitPrice = (double)x.Product.UnitPrice,
                    Discount = (double)x.Product.Discount,
                })
                .ToList();

            var viewModel = new ProductListVM
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages,
                DisplayNumber = displayNumber,
                Keyword = keyword
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return Redirect("/404");

            var product = _context.Products
               .Include(p => p.Category)
               .Include(p => p.Supplier)
               .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return Redirect("/404");

            var viewModel = new ProductDetailsVM
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.CategoryName,
                UnitDescription = product.UnitDescription,
                UnitPrice = product.UnitPrice ?? 0,
                Image = product.Image,
                ManufactureDate = product.ManufactureDate,
                Discount = product.Discount,
                ViewCount = product.ViewCount,
                SupplierId = product.SupplierId,
                SupplierCompanyName = product.Supplier.CompanyName,
                Description = product.Description
            };

            var relatedProducts = _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != product.ProductId)
                .OrderBy(p => p.ProductName)
                .Select(p => new ProductCardVM
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryId = p.Category.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    Image = p.Image,
                    UnitPrice = (double)p.UnitPrice,
                    Discount = p.Discount,
                })
                .ToList();

            ViewBag.RelatedProducts = relatedProducts;

            return View(viewModel);
        }
    }
}
