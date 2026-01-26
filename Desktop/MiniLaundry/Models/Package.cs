using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MiniLaundry.Models
{
    public class Package
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int Price { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int Duration { get; set; }
        public ICollection<DetailPackage> detailPackages { get; set; }
    }
}
