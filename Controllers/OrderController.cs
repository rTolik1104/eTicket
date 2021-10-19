using eTicket_Demo.Data.Cart;
using eTicket_Demo.Data.Interfaces;
using eTicket_Demo.Models;
using eTicket_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace eTicket_Demo.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMovieServices _movieServices;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderServices _orderServices;
        private readonly UserManager<AppUser> _userManager;


        public OrderController(IMovieServices movieServices,IOrderServices orderServices,ShoppingCart shoppingCart, UserManager<AppUser> userManager)
        {
            _movieServices = movieServices;
            _orderServices = orderServices;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);

            var orders = await _orderServices.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        [Authorize]
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var movie = await _movieServices.GetMovieByIdAsync(id);

            if (movie != null)
            {
                _shoppingCart.AddItemToCart(movie);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _movieServices.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }


        public IActionResult GenerateQRCode()
        {
            string userId = _userManager.GetUserId(User);
            string code = _orderServices.GetUniqCode(userId);

            QRCodeGenerator qRCodeGenerator = new();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new(qRCodeData);
            Bitmap bitmap = qRCode.GetGraphic(8);
            var bitmapBytes = ConvertBitmapToBytes(bitmap);

            return File(bitmapBytes, "image/jpeg");
        }
        public async Task<IActionResult> CompleteOrder()
        {
            
            var items = _shoppingCart.GetShoppingCartItems();

            string userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            string userEmailAddress = user.Email;
            
            await _orderServices.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

        

        private byte[] ConvertBitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream ms = new())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

    }
}
