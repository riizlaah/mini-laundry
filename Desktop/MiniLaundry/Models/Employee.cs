using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MiniLaundry.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public int JobId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNum { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal Salary { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; } = null!;

    }
}
