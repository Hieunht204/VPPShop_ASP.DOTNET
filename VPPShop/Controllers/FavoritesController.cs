using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPPShop.Entities;
using VPPShop.Helpers;
using VPPShop.Models;

namespace VPPShop.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly TmdtContext _context;

        public FavoritesController(TmdtContext context)
        {
            _context = context;
        }

        public List<FavoriteViewModel> Favorites => HttpContext.Session.Get<List<FavoriteViewModel>>(MySetttings.FAVORITES_KEY) ?? new List<FavoriteViewModel>();

        [Authorize]
        public IActionResult Index()
        {
            return View(Favorites);
        }

        [Authorize]
        public IActionResult AddToFavorites(string id)
        {
            var myFavorites = Favorites;
            var favoriteItem = myFavorites.FirstOrDefault(p => p.ProductId == id);

            if (favoriteItem == null)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
                if (product == null)
                {
                    return Redirect("/404");
                }

                favoriteItem = new FavoriteViewModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = (double)product.UnitPrice,
                    Image = product.Image ?? string.Empty,
                    Discount = product.Discount
                };
                myFavorites.Add(favoriteItem);
                HttpContext.Session.Set(MySetttings.FAVORITES_KEY, myFavorites);
            }

            return Json(new
            {
                success = true,
                message = "Sản phẩm đã được thêm vào danh sách yêu thích.",
                totalItems = myFavorites.Count
            });
        }

        [Authorize]
        public IActionResult RemoveFavorite(string id)
        {
            var myFavorites = Favorites;
            var favoriteItem = myFavorites.FirstOrDefault(p => p.ProductId == id);

            if (favoriteItem != null)
            {
                myFavorites.Remove(favoriteItem);
                HttpContext.Session.Set(MySetttings.FAVORITES_KEY, myFavorites);
            }
            return RedirectToAction("Index");
        }
    }
}