using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.DTOs;
using UdemyNLayerProject.Web.ApiServices;

namespace UdemyNLayerProject.Web.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        private readonly CategoryApiService _categoryApiService;

        public NotFoundFilter(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var category = await _categoryApiService.GetByIdAsync(id);

            if (category != null)
            {
                await next();
            }

            ErrorDto errorDto = new ErrorDto();

            errorDto.Errors.Add($"id'si {id} olan kategori bulunamadı");

            context.Result = new RedirectToActionResult("Error", "Home", errorDto);
        }
    }
}