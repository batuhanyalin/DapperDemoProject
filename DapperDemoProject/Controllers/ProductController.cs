using DapperDemoProject.Dtos.ProductDtos;
using DapperDemoProject.Services.CategoryServices;
using DapperDemoProject.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DapperDemoProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;
        private readonly ICategoryService _CategoryService;
        public ProductController(IProductService ProductService, ICategoryService categoryService)
        {
            _ProductService = ProductService;
            _CategoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _ProductService.GetAllProductAsync();
            return View(values);
        }
        public async Task<IActionResult> CreateProduct()
        {
            var values= await _CategoryService.GetAllCategoryAsync();
            List<SelectListItem> category= (from x in values
                                            select new SelectListItem
                                            {
                                                Text=x.CategoryName,
                                                Value=x.CategoryId.ToString()   
                                            }).ToList();    
            ViewBag.categoryList=category;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            await _ProductService.CreateProductAsync(dto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _ProductService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var values = await _CategoryService.GetAllCategoryAsync();
            List<SelectListItem> category = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryId.ToString()
                                             }).ToList();
            ViewBag.categoryList = category;
            var value = await _ProductService.GetByIdProductAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            await _ProductService.UpdateProductAsync(dto);
            return RedirectToAction("Index");
        }
    }
}
