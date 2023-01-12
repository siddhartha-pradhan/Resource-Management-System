using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ResourceManagementSystem.Core.Models;
using ResourceManagementSystem.Application.DTOs;
using ResourceManagementSystem.Application.Interfaces.Base;

namespace ResourcesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region API Calls
        [HttpGet("GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _unitOfWork.Product.GetAll(filter: x => !x.IsDeleted, includeProperties: "Category");

            if (products.Count() != 0)
            {
                return Ok(products.Select(product => _mapper.Map<ProductViewDto>(product)));
            }

            return BadRequest("No any products added to the list yet.");
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductByID(int ID)
        {
            var product = await _unitOfWork.Product.Get(ID);

            if (product != null && !product.IsDeleted)
            {
                var productDTO = _mapper.Map<ProductViewDto>(product);
                return Ok(productDTO);
            }

            return BadRequest("Product not found.");
        }

        [HttpPost("AddProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> AddProduct(ProductInsertDto productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);

            product.CreatedAt = DateTime.Now;

            await _unitOfWork.Product.Add(product);

            await _unitOfWork.Save();

            return Ok("Product Successfully Added.");
        }
        
        [HttpPut("UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> UpdateProduct(ProductUpdateDto productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);

            var productObject = await _unitOfWork.Product.Update(product);

            if (productObject == -1)
            {
                return BadRequest("Product not found.");
            }

            await _unitOfWork.Save();

            return Ok("Product Successfully Updated.");
        }

        [HttpDelete("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> DeleteProduct(int ID)
        {
            var productObject = await _unitOfWork.Product.Remove(ID);

            if (productObject == -1)
            {
                return BadRequest("Product not found.");
            }

            await _unitOfWork.Save();

            return Ok("Product successfully deleted.");
        }
        #endregion
    }
}
