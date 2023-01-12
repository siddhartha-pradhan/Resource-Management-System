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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Remove(int ID)
        {
            var item = await _dbContext.Categories.FirstOrDefaultAsync(x => x.ID == ID);

            if (item != null)
            {
                item.IsDeleted = true;
                _dbContext.Categories.Update(item);
                return 1;
            }
            return -1;

        }

        public async Task<int> Update(Category category)
        {
            var item = await _dbContext.Categories.FirstOrDefaultAsync(x => x.ID == category.ID);

            if (item != null)
            {
                item.Title = category.Title;
                item.LastModifiedAt = DateTime.Now;
                return 1;
            }
            return -1;
        }
    }
}
