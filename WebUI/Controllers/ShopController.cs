using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult List()
        {
            var values = _productService.GetAll();
            return View(values);
        }

        public IActionResult Details(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            var value = _productService.GetByID(id);

            if(value == null)
            {
                return NotFound();
            }

            return View(value);
        }


    }
}
