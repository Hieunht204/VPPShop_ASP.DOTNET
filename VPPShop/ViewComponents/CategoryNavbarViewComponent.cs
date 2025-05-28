using Microsoft.AspNetCore.Mvc;
using VPPShop.Entities;
using VPPShop.Models;

namespace VPPShop.ViewComponents
{
    public class CategoryNavbarViewComponent : ViewComponent
    {
        private readonly TmdtContext _context;

        public CategoryNavbarViewComponent(TmdtContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories
                .OrderBy(c => c.CategoryName)
                .ToList();

            return View(categories);
        }
    }
}