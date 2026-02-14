using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLaundry.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    }
}
