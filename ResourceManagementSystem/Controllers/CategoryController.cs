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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region API Calls
        [HttpGet("GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _unitOfWork.Category.GetAll(x => !x.IsDeleted);

            if (categories.Count() == 0)
            {
                return BadRequest("No any categories have been added yet.");
            }

            return Ok(categories.Select(category => _mapper.Map<CategoryViewDto>(category)));
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> GetCategoryByID(int ID)
        {
            var category = await _unitOfWork.Category.Get(ID);

            if (category == null || category.IsDeleted)
            {
                return BadRequest("Category not found.");
            }
            
            var categoryDTO = _mapper.Map<CategoryViewDto>(category);

            return Ok(categoryDTO);
        }

        [HttpPost("AddCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> AddCategory(CategoryPostDto categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);

            category.CreatedAt = DateTime.Now;

            await _unitOfWork.Category.Add(category);

            await _unitOfWork.Save();

            return Ok("Category successfully added.");
        }

        [HttpPut("UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> UpdateCategory(CategoryViewDto categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);

            var categoryObject = await _unitOfWork.Category.Update(category);

            if (categoryObject == -1)
            {
                return BadRequest("Category not found.");
            }

            await _unitOfWork.Save();

            return Ok("Category successfully updated.");
        }

        [HttpDelete("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> DeleteCategory(int ID)
        {
            var categoryObject = await _unitOfWork.Category.Remove(ID);

            if (categoryObject == -1)
            {
                return BadRequest("Category not found.");
            }

            await _unitOfWork.Save();

            return Ok("Category successfully deleted.");
        }
        #endregion

    }
}
