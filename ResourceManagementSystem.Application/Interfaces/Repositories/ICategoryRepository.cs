using ResourceManagementSystem.Application.Interfaces.Base;
using ResourceManagementSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Application.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<int> Update(Category category);

        Task<int> Remove(int ID);
    }
}
