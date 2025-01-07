using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(UserId != null)
            {
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == UserId.Value).Count());
            }

            IEnumerable<Product> Products = _unitOfWork.Product.GetAll(null, IncludeProperties: "Category");
            return View(Products);
        }

        public IActionResult Details(int Id)
        {
            ShoppingCart shoppingcart = new ShoppingCart()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == Id, IncludeProperties: "Category"),
                ProductId = Id,
                Count = 1
            };

            return View(shoppingcart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var UserId = claimsidentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCart.ApplicationUserId = UserId;
            shoppingCart.Id = 0;

            ShoppingCart CartFormDataBase = _unitOfWork.ShoppingCart
                .Get(s => s.ApplicationUserId == UserId && s.ProductId == shoppingCart.ProductId);
            if(CartFormDataBase != null)
            {
                CartFormDataBase.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(CartFormDataBase);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == UserId).Count());
            }

            TempData["Success"] = "Cart Created Successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
