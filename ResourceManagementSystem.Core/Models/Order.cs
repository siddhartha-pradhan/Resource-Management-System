using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagementSystem.Core.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        public DateTime OrderedDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmount { get; set; }

        public string StaffID { get; set; }

        [ForeignKey("StaffID")]
        public Staff? Staff { get; set; }

        public virtual List<OrderLine> OrderLines { get; set; }
    }
}
