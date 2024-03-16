using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult List(int id, int page = 1)
        {
            if(id == 0)
            {
                var values = _productService.GetAll();
                return View(values.ToPagedList(page, 2));
            }
            else
            {
                var values = _productService.GetProductsByCategory(id);
                return View(values.ToPagedList(page, 2));
            }
        }

        public IActionResult Details(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            var value = _productService.GetProductDetails(id);

            if(value == null)
            {
                return NotFound();
            }

            return View(value);
        }


    }
}
