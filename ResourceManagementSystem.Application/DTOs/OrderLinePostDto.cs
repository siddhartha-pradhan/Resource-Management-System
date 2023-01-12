using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Application.DTOs
{
    public class OrderLinePostDto
    {
        public int ProductID { get; set; }

        public int Quantity { get; set; }
    }
}
