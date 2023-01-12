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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Remove(int ID)
        {
            var item = await _dbContext.Products.FirstOrDefaultAsync(x => x.ID == ID);

            if (item != null)
            {
                item.IsDeleted = true;
                _dbContext.Products.Update(item);
                return 1;
            }
            return -1;
        }

        public async Task<int> Update(Product product)
        {
            var item = await _dbContext.Products.FirstOrDefaultAsync(x => x.ID == product.ID);

            if (item != null)
            {
                item.Name = product.Name;
                item.Price = product.Price;
                item.Quantity = product.Quantity;
                item.CategoryID = product.CategoryID;
                item.LastModifiedAt = DateTime.Now;
                return 1;
            }
            return -1;
        }
    }
}
