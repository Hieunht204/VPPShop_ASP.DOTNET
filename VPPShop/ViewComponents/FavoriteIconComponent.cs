using Microsoft.AspNetCore.Mvc;
using VPPShop.Helpers;
using VPPShop.Models;

namespace VPPShop.ViewComponents
{
    public class FavoriteIconViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string size = "default")
        {
            var favorites = HttpContext.Session.Get<List<FavoriteViewModel>>(MySetttings.FAVORITES_KEY) ?? new List<FavoriteViewModel>();
            var result = new FavoriteIconViewModel
            {
                Count = favorites.Count
            };
            ViewBag.Size = size;
            return View(result);
        }
    }
}