using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VPPShop.Entities;
using VPPShop.Helpers;


namespace VPPShop.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly TmdtContext _context;

        public InvoiceController(TmdtContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var customerId = HttpContext.User.Claims
            .SingleOrDefault(c => c.Type == MySetttings.CLAIM_CUSTOMERID)?.Value;

            if (string.IsNullOrEmpty(customerId))
                return Unauthorized();

            var orders = _context.Invoices
            .Include(i => i.Status)
            .Where(i => i.CustomerId == customerId)
            .OrderByDescending(i => i.OrderDate)
            .ToList();

            return View(orders);
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return Redirect("/404");

            var invoice = _context.Invoices
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Product) // nếu muốn hiện tên sản phẩm
                .Include(i => i.Status)
                .SingleOrDefault(i => i.InvoiceId == id);

            if (invoice == null) return Redirect("/404");

            return View(invoice);
        }
    }
}
