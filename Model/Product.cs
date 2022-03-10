using System;
using System.Collections.Generic;

#nullable disable

namespace ExcelToSQLDemo.Model
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? Categoryid { get; set; }

        public virtual Category Category { get; set; }
    }
}
