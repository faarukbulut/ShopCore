using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Products()
        {
            var values = _productService.GetAll();
            return View(values);
        }

        public IActionResult CreateProduct()
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(Product p)
        {
            _productService.Create(p);
            return RedirectToAction("Products");
        }

        public IActionResult UpdateProduct(int id)
        {
            ViewBag.Categories = _categoryService.GetAll();
            var value = _productService.GetByID(id);
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(Product p)
        {
            _productService.Update(p);
            return RedirectToAction("Products");
        }

        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.GetByID(id);
            _productService.Delete(value);
            return RedirectToAction("Products");
        }

    }
}
