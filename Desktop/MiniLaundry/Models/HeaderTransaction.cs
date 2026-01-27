using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLaundry.Models
{
    public class HeaderTransaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public string? DiscountToken { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime CompleteEstDate { get; set; }
        public Customer Customer { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public Discount? Discount { get; set; } = null!;
        public ICollection<DetailTransaction> DetailTransactions { get; set; }
    }
}
