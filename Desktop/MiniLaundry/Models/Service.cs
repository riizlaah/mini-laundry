using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MiniLaundry.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int EstimationDuration { get; set; }
        public Category Category { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
    }
}
