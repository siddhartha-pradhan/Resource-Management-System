using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Application.Interfaces.Base
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        IOrderRepository Order { get; }

        IProductRepository Product { get; }

        IStaffRepository Staff { get; }

        Task Save();
    }
}
