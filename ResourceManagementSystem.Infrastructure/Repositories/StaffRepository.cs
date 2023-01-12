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
    public class StaffRepository : Repository<Staff>, IStaffRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StaffRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
