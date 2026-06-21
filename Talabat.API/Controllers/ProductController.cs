using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.API.Controllers
{
    //[Authorize]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


      
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllAsync([FromQuery] ProductParamsSpec productParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(productParams);
            var products = await _unitOfWork.Repository<Product>().GettAllWithSpecAsync(spec);
            var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            var specCount = new ProductPaginationCount(productParams);
            var productPaginationCount = await _unitOfWork.Repository<Product>().CountAsync(specCount);

            //var products = await _productRepositort.GetAllAsync();
            var productPadination = new ProductPagination<ProductDto>()
            {
                PageIndex = productParams.PageIndex,
                PageSize = productParams.PageSize,
                Data = productsDto,
                Count = productPaginationCount
            };

            return Ok(productPadination);
        }




        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            if (product is null) return NotFound(new ApiResponse( 404 , "Product not found"));
            var productDto = _mapper.Map<Product, ProductDto>(product);
            //var product = await _productRepositort.GetAsync(id);
            return Ok(productDto);
        }





        [HttpGet("Brands")] // GET : api/Product/Brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            if (brands is null) return NotFound(new ApiResponse(404));
            return Ok(brands);
        }


        [HttpGet("Brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetById(int id)
        {
            var brand = await _unitOfWork.Repository<ProductBrand>().GetAsync(id);
            if (brand is null) return NotFound(new ApiResponse(404));
            return Ok(brand);
        }



        [HttpGet("Category")] // GET : api/product/Category
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategory()
        {
            var categories = await _unitOfWork.Repository<ProductCategory>().GetAllAsync();
            if (categories is null) return NotFound(new ApiResponse(404));
            return Ok(categories);
        }







    }
}
