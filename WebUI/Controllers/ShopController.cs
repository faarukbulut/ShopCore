using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult List(int id, int page = 1)
        {
            ViewBag.categoryID = id;
            if(id == 0)
            {
                var values = _productService.GetAll();
                return View(values.ToPagedList(page, 10));
            }
            else
            {
                var values = _productService.GetProductsByCategory(id);
                return View(values.ToPagedList(page, 10));
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
