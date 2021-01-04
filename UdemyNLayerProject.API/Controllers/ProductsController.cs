using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOs;
using UdemyNLayerProject.API.Filters;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(await _productService.GetAllAsync()));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<ProductDto>(await _productService.GetByIdAsync(id)));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryByIdAsync(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);

            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }

        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));

            return Created(string.Empty, _mapper.Map<ProductDto>(product));
        }

        [HttpPut]
        public IActionResult Update(ProductDto productDto)
        {
            _productService.Update(_mapper.Map<Product>(productDto));

            return NoContent();
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //_productService.Remove(new Product { Id = id });

            var product = _productService.GetByIdAsync(id).Result;

            _productService.Remove(product);

            return NoContent();
        }
    }
}
