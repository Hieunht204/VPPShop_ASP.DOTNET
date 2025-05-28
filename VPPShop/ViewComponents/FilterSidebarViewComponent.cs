using Microsoft.AspNetCore.Mvc;
using VPPShop.Entities;
using VPPShop.Models;
using System.Linq;

namespace VPPShop.ViewComponents
{
    public class FilterSidebarViewComponent : ViewComponent
    {
        private readonly TmdtContext _context;
        public FilterSidebarViewComponent(TmdtContext context)
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
                      (c, pc) => new FilterSidebarViewModel
                      {
                          CategoryId = c.CategoryId,
                          CategoryName = c.CategoryName,
                          Count = pc.Count
                      })
                .OrderBy(f => f.CategoryName)
                .ToList();

            return View(categories);
        }
    }
}
