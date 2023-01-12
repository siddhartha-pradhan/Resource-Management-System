using ResourceManagementSystem.Application.Interfaces.Base;
using ResourceManagementSystem.Application.Interfaces;
using ResourceManagementSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Category = new CategoryRepository(_dbContext);
            Order = new OrderRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
            Staff = new StaffRepository(_dbContext);
        }

        public ICategoryRepository Category { get; private set; }

        public IOrderRepository Order { get; private set; }

        public IProductRepository Product { get; private set; }

        public IStaffRepository Staff { get; private set; }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
