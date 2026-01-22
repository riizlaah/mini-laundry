using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace testWPF.Models
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
        public string DateOfBirth { get; set; }
        [Column(TypeName = "money")]
        public decimal Salary;
        public Job Job = null!;

    }
}
