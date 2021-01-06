using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.ApiServices;
using UdemyNLayerProject.Web.DTOs;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryApiService _categoryApiService;

        public CategoryController(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryApiService.GetAllAsync();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.AddAsync(categoryDto);

                return RedirectToAction("Index");
            }

            return View(categoryDto);
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryApiService.GetByIdAsync(id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.Update(categoryDto);

                return RedirectToAction("Index");
            }

            return View(categoryDto);
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryApiService.Remove(id);

            return RedirectToAction("Index");
        }
    }
}