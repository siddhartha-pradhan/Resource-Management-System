using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Core.QueryModels
{
    public class ProductSales
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public int Quantity { get; set; }

        public decimal Sales { get; set; }
    }
}
