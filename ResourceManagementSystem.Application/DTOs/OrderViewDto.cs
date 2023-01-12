using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Application.DTOs
{
    public class OrderViewDto
    {
        public string ID { get; set; }

        public DateTime OrderedDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string StaffID { get; set; }

        public StaffDto Staff { get; set; }

        public List<OrderLineViewDto> OrderLines { get; set; }
    }
}
