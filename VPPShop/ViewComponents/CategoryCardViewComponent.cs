using Microsoft.AspNetCore.Mvc;
using VPPShop.Entities;
using VPPShop.Models;

namespace VPPShop.ViewComponents
{
    public class CategoryCardViewComponent : ViewComponent
    {
        private readonly TmdtContext _context;

        public CategoryCardViewComponent(TmdtContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var productCounts = _context.Products
                .GroupBy(p => p.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    Count = g.Count()
                })
                .ToList();

            var categories = _context.Categories
                .ToList()
                .Join(productCounts,
                      c => c.CategoryId,
                      pc => pc.CategoryId,
                      (c, pc) => new CategoryCardViewModel
                      {
                          CategoryId = c.CategoryId,
                          CategoryName = c.CategoryName,
                          Count = pc.Count,
                          Image = c.Image
                      })
                .OrderBy(f => f.CategoryName)
                .ToList();

            return View(categories);
        }
    }
}