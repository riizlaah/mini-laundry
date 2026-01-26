using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLaundry.Models
{
    public class DetailTransaction
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int HeaderTransactionId { get; set; }
        public int PackageId { get; set; }
        public int Price { get; set; }
        public float TotalUnit { get; set; }
        public DateTime CompletedAt { get; set; }
        public Service Service { get; set; } = null!;
        public HeaderTransaction HeaderTransaction { get; set; } = null!;
        public Package Package { get; set; } = null!;

    }
}
