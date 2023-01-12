using ResourceManagementSystem.Application.Interfaces.Base;
using ResourceManagementSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Application.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<int> Update(Product product);

        Task<int> Remove(int ID);
    }
}
