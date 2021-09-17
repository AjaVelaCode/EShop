using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Entities
{
    public class Computer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public string Procesor { get; set; }
        public string Ram { get; set; }
    }
}
