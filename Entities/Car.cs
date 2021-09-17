using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Manufacturer { get; set; }
        [Column(TypeName = "decimal(18,1)")]
        public decimal Price { get; set; }
        public int EngineNumber { get; set; }
        public string EngineType { get; set; }
        public string FuelType { get; set; }
    }
}
