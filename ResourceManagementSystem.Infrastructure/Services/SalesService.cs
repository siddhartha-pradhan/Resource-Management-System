using Microsoft.EntityFrameworkCore;
using ResourceManagementSystem.Application.Interfaces;
using ResourceManagementSystem.Core.QueryModels;
using ResourceManagementSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Infrastructure.Services
{
    public class SalesService : ISalesService
    {
        private readonly ApplicationDbContext _dbContext;

        public SalesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductSales>> GetProductSales()
        {
            var productCharts = await
                (from order in _dbContext.OrderLines
                 join product in _dbContext.Products on order.ProductID equals product.ID
                 join category in _dbContext.Categories on product.CategoryID equals category.ID
                 group order by new
                 {
                     product.ID,
                     product.Name,
                     category.Title
                 } into salesGroup
                 select new ProductSales()
                 {
                     Name = salesGroup.Key.Name,
                     Category = salesGroup.Key.Title,
                     Quantity = salesGroup.Sum(p => p.Quantity),
                     Sales = salesGroup.Sum(p => p.LineTotal)
                 }).OrderByDescending(s => s.Sales).ToListAsync();

            return productCharts;
        }

        public async Task<List<StaffSales>> GetStaffSales()
        {
            var staffCharts = await
                (from order in _dbContext.Orders
                 join staff in _dbContext.Staffs
                 on order.StaffID equals staff.Id
                 group order by new
                 {
                     order.StaffID,
                     staff.Name
                 } into salesGroup
                 select new StaffSales()
                 {
                     Name = salesGroup.Key.Name,
                     Sales = salesGroup.Sum(c => c.TotalAmount)
                 }).OrderByDescending(s => s.Sales).ToListAsync();

            return staffCharts;
        }
    }
}
