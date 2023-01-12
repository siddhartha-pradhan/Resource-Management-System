using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Application.DTOs
{
    public class OrderPostDto
    {
        public List<OrderLinePostDto> OrderLines { get; set; }
    }
}
