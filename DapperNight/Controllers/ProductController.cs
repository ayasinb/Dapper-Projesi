using DapperNight.Dtos.CategoryDtos;
using DapperNight.Dtos.ProductDtos;
using DapperNight.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace DapperNight.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productservice;

        public ProductController(IProductService productservice)
        {
            _productservice = productservice;
        }

        public async Task<IActionResult> ProductList()
        {
            var values = await _productservice.GetAllProductAsync();
            return View(values);
        }
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productservice.CreateProductAsync(createProductDto);
            return RedirectToAction("ProductList");
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productservice.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var value = await _productservice.GetByIdProductAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productservice.UpdateProductAsync(updateProductDto);
            return RedirectToAction("ProductList");
        }
    }
}
