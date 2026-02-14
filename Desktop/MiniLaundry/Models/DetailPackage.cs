using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLaundry.Models
{
    public class DetailPackage
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int ServiceId { get; set; }
        public int TotalUnitService {  get; set; }
        public Service Service { get; set; } = null!;
        public Package Package { get; set; } = null!;
    }
}
