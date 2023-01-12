using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ResourceManagementSystem.Core.Models;
using ResourceManagementSystem.Application.DTOs;
using ResourceManagementSystem.Application.Interfaces.Base;

namespace ResourcesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region API Calls
        [HttpGet("GetAllOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Product>>> GetAllOrders()
        {
            var products = await _unitOfWork.Product.GetAll(includeProperties: "Category");

            products.Select(product => _mapper.Map<ProductViewDto>(product));

            var staffs = await _unitOfWork.Staff.GetAll();

            staffs.Select(staff => _mapper.Map<StaffDto>(staff));

            var orders = await _unitOfWork.Order.GetAll(includeProperties: "OrderLines");

            if (orders.Count() != 0)
            {
                return Ok(orders.Select(order => _mapper.Map<OrderViewDto>(order)));
            }

            return BadRequest("No any orders have been placed yet.");
        }

        [HttpPost("PostOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> AddOrder(OrderPostDto orderViewModel)
        {
            var order = _mapper.Map<Order>(orderViewModel);

            var authenticated = User.Identity.IsAuthenticated;

            var claims = User.Identities.FirstOrDefault().Claims;

            order.StaffID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            for (int i = 0; i < order.OrderLines.Count; i++)
            {
                order.OrderLines[i].OrderID = order.ID;

                var orderObject = await _unitOfWork.Order.Transaction(order.OrderLines[i].ProductID, order.OrderLines[i].Quantity);

                if (orderObject == -1)
                {
                    return BadRequest("Failure while ordering items.");
                }
                else if(orderObject == 0)
                {
                    return BadRequest("Product out of stock.");
                }

                order.OrderLines[i].LineTotal = orderObject;

                order.TotalAmount += orderObject;
            }

            order.OrderedDate = DateTime.Now;

            await _unitOfWork.Order.Add(order);

            await _unitOfWork.Save();

            return Ok("Order Successfully Registered.");
        }
        #endregion
    }
}
