using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VPPShop.Models;
using VPPShop.Entities;
using System.Runtime.CompilerServices;
using AutoMapper;
using VPPShop.Helpers;
using VPPShop.Services;
using System.Text.RegularExpressions;

namespace VPPShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly TmdtContext _context;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;

        public AccountController(TmdtContext context, IMapper mapper, EmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        #region Login(Đăng nhập)

        [HttpGet]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return Redirect("/404");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var customer = _context.Customers
                .FirstOrDefault(c => c.Email == model.Email && c.PassWord == model.Password);

            if (customer == null)
            {
                ModelState.AddModelError("", "Sai email hoặc mật khẩu.");
                return View(model);
            }

            //if (!customer.Validity)
            //{
            //    ModelState.AddModelError("", "Tài khoản chưa được xác minh qua email. Vui lòng kiểm tra hộp thư.");
            //    return View(model);
            //}
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.Fullname),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(MySetttings.CLAIM_CUSTOMERID, customer.CustomerId),
                new Claim(ClaimTypes.Role, customer.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register(Đăng ký)
        [HttpGet]
        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return Redirect("/404");
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customerIds = _context.Customers
                        .Where(c => c.CustomerId.StartsWith("cus"))
                        .Select(c => c.CustomerId)
                        .ToList();

                    int maxNumber = customerIds
                        .Select(id =>
                        {
                            var match = Regex.Match(id, @"cus(\d+)$");
                            return match.Success && int.TryParse(match.Groups[1].Value, out int n) ? n : 0;
                        })
                        .Max();

                    string newCustomerId = "cus" + (maxNumber + 1);

                    var customer = _mapper.Map<Customer>(model);
                    customer.CustomerId = newCustomerId;
                    customer.Validity = false;
                    customer.Role = 0;

                    if (img != null)
                    {
                        customer.Image = MySetttings.UploadImage(img, "customerAvatar");
                    }
                    _context.Add(customer);
                    _context.SaveChanges();

                    //string token = Guid.NewGuid().ToString();

                    //var emailVerification = new EmailVerification
                    //{
                    //    CustomerId = newCustomerId,
                    //    Token = token,
                    //    ExpiresAt = DateTime.Now.AddHours(24),
                    //    IsUsed = false
                    //};
                    //_context.EmailVerifications.Add(emailVerification);
                    //_context.SaveChanges();

                    //_emailService.SendVerificationEmail(customer.Email, token);

                    return RedirectToAction("Login", "Account");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex.Message);
                }
            }
            return View();
        }

        public IActionResult VerifyEmail(string token)
        {
            var verification = _context.EmailVerifications
                .FirstOrDefault(v => v.Token == token && !v.IsUsed && v.ExpiresAt > DateTime.Now);

            if (verification == null)
            {
                return View("InvalidOrExpiredToken");
            }

            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == verification.CustomerId);
            if (customer == null)
            {
                return View("Error");
            }

            customer.Validity = true;
            verification.IsUsed = true;
            _context.SaveChanges();

            return View("EmailVerified");
        }
        #endregion

        #region Logout(Đăng xuất)
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
