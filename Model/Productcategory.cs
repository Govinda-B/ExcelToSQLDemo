using System;
using System.Collections.Generic;

#nullable disable

namespace ExcelToSQLDemo.Model
{
    public partial class Productcategory
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CategoryName { get; set; }
        public string Descripriction { get; set; }
    }
}
