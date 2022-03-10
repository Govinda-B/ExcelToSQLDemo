using System;
using System.Collections.Generic;

#nullable disable

namespace ExcelToSQLDemo.Model
{
    public partial class ProductDatum
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string Category { get; set; }
        public decimal? Mrp { get; set; }
    }
}
