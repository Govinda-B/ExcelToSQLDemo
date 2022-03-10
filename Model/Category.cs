using System;
using System.Collections.Generic;

#nullable disable

namespace ExcelToSQLDemo.Model
{
    public partial class Category
    {
        public Category()
        {
            Product1s = new HashSet<Product1>();
            Product2s = new HashSet<Product2>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripriction { get; set; }

        public virtual ICollection<Product1> Product1s { get; set; }
        public virtual ICollection<Product2> Product2s { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
