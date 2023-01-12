using Microsoft.EntityFrameworkCore;
using ResourceManagementSystem.Application.Interfaces;
using ResourceManagementSystem.Core.Models;
using ResourceManagementSystem.Infrastructure.Persistence;
using ResourceManagementSystem.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> Transaction(int ID, int quantity)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ID == ID);

            var lineTotal = 0.0m;

            if (product != null)
            {
                if (quantity <= product.Quantity)
                {
                    product.Quantity -= quantity;
                    lineTotal = quantity * product.Price;
                    return lineTotal;
                }
                else
                {
                    return 0;
                }
            }

            return -1;
        }
    }
}
