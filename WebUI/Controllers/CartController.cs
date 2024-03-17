using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Configuration;

namespace WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, UserManager<AppUser> userManager, IOrderService orderService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var values = _cartService.GetCartByUserId(userId);
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productID, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.AddToCart(userId, productID, quantity);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFromCart(int productID)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.DeleteFromCart(userId, productID);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var values = _cartService.GetCartByUserId(userId);
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(string? orderNote, int paymentMethod, string? senderName, string? sendDate)
        {
            var userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCartByUserId(userId);

            SaveOrder(cart, paymentMethod, orderNote, senderName, sendDate);
            ClearCart(cart.UserId);

            if (paymentMethod == 0){
                // kredi kartı ödemesi için aracı ödeme şirketlerine yönelik işlemler yaptırılabilir.
                return RedirectToAction("PaymentSuccess");
            }

            return RedirectToAction("PaymentSuccess");
        }

        private void SaveOrder(Cart cart, int paymentMethod, string? orderNote, string? senderName, string? sendDate)
        {
            var order = new Order();

            order.OrderNumber = Guid.NewGuid().ToString().Substring(0, 12).ToUpper().Replace("-", "");
            order.OrderState = EnumOrderState.Unpaid;
            order.PaymentTypes = paymentMethod == 0 ? EnumPaymentTypes.CreditCart : EnumPaymentTypes.Eft;
            order.OrderDate = DateTime.Now;
            order.UserId = cart.UserId;
            order.OrderNote = orderNote;
            order.SenderName = senderName;
            order.SendDate = sendDate;

            foreach(var item in cart.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    Price = item.Product.Price,
                    Quantity = item.Quantity,
                    ProductID = item.ProductID
                };
                order.OrderItems.Add(orderItem);
            }

            _orderService.Create(order);
        }

        private void ClearCart(string userId)
        {

        }

        public IActionResult PaymentSuccess()
        {
            return View();
        }

    }
}
