using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MiniLaundry.Models
{
    public class Discount
    {
        [Key] public string Token { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
