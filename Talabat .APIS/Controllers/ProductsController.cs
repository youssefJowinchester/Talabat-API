using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications.productSpec;
using Talabat_.APIS.DTOS;
using Talabat_.APIS.Errors;
using Talabat_.APIS.Helpers;

namespace Talabat_.APIS.Controllers
{

    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;

        public ProductsController(IGenericRepository<Product> ProductRepository, IMapper mapper, IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductCategory> categoryRepo)
        {
            _productRepository = ProductRepository;
            _mapper = mapper;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet] //www.ghg/api/peoducts/GetProducts
        public async Task<ActionResult<IReadOnlyList<productToReturnDTO>>> GetProducts([FromQuery] productSpecParams productSpec)
        {
            var spec = new productWithBrandAndCategorySpecifications(productSpec);

            var products = await _productRepository.GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<productToReturnDTO>>(products);

            var countSpec = new productWithFilterationForCountSpecification(productSpec);

            int count = await _productRepository.GetCountAsync(countSpec);

            return Ok(new pagination<productToReturnDTO>(productSpec.PageSize, productSpec.PageIndex, count, data));
        }
        [ProducesResponseType(typeof(productToReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<productToReturnDTO>> GetProductsByIdAsync(int id)
        {
            //var product = await _productRepository.GetAsync(id);
            var spec = new productWithBrandAndCategorySpecifications(id);
            var product = await _productRepository.GetWithSpecAsync(spec);

            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var result = _mapper.Map<Product, productToReturnDTO>(product);

            return Ok(result);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }


        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
