using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourceManagementSystem.Core.Shared;

namespace ResourceManagementSystem.Core.Models
{
    public class Product : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }

        public virtual List<OrderLine>? OrderLines { get; set; }

        public bool IsDeleted { get; set; }
    }
}
