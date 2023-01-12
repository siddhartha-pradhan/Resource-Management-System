using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ResourceManagementSystem.Application.Interfaces;

namespace ResourcesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        #region API Calls
        [HttpGet("GetAllStaffSales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public JsonResult StaffSalesChart()
        {
            var chart = _salesService.GetStaffSales();

            return new JsonResult(chart);
        }

        [HttpGet("GetAllProductSales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public JsonResult ProductSalesChart()
        {
            var chart = _salesService.GetProductSales();

            return new JsonResult(chart);
        }
        #endregion
    }
}
