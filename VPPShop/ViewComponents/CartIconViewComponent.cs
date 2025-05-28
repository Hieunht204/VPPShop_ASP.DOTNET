using Microsoft.AspNetCore.Mvc;
using VPPShop.Helpers;
using VPPShop.Models;

namespace VPPShop.ViewComponents
{
    public class CartIconViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string size = "default")
        {
            var cart = HttpContext.Session.Get<List<CartViewModel>>(MySetttings.CART_KEY) ?? new List<CartViewModel>();
            var result = new CartIconViewModel
            {
                Quantity = cart.Sum(x => x.Quantity),
                Total = cart.Sum(x => x.Total ?? 0)
            };
            ViewBag.Size = size;
            return View(result);
        }
    }
}
