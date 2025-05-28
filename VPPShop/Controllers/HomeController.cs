using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using VPPShop.Models;
using VPPShop.Entities;

namespace VPPShop.Controllers;

[Authorize(Roles = "0")]
public class HomeController : Controller
{
    private readonly TmdtContext _context;

    public HomeController(TmdtContext context)
    {
        _context = context;
    }

    [Authorize]
    public IActionResult Index()
    {
        var featured = _context.Products
        .OrderByDescending(p => p.ViewCount)
        .Take(8)
        .Select(p => new ProductCardVM
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Image = p.Image,
            UnitPrice = (double)(p.UnitPrice ?? 0),
            Discount = p.Discount
        }).ToList();

        var newest = _context.Products
        .OrderByDescending(p => p.ManufactureDate)
        .Take(8)
        .Select(p => new ProductCardVM
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Image = p.Image,
            UnitPrice = (double)(p.UnitPrice ?? 0),
            Discount = p.Discount
        }).ToList();

        var offers = _context.Products
        .OrderByDescending(p => p.Discount)
        .Take(2)
        .Select(p => new ProductCardVM
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Image = p.Image,
            UnitPrice = (double)(p.UnitPrice ?? 0),
            Discount = p.Discount
        }).ToList();

        var vm = new HomePageViewModel
        {
            FeaturedProducts = featured,
            NewestProducts = newest,
            OfferProducts = offers
        };

        return View(vm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    [Route("/404")]
    public IActionResult PageNotFound()
    {
        return View();
    }


}
