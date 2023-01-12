using ResourceManagementSystem.Core.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Application.Interfaces
{
    public interface ISalesService
    {
        Task<List<ProductSales>> GetProductSales();

        Task<List<StaffSales>> GetStaffSales();
    }
}
