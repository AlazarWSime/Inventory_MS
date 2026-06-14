using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Models
{
    public class ItemModel
    {
        public Guid Item_ID { get; set; }
        public Guid Category_ID { get; set; }
        public Guid Supplier_ID { get; set; }
        public string Item_Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Reorder_Level { get; set; }
        // ── Display Only — NOT a DB column ──
        // Joined from Category table using Category_ID FK
        public string Category_Name { get; set; }
        // ── Display Only — NOT a DB column ──
        // Joined from Supplier table using Supplier_ID FK
        public string Supplier_Name { get; set; }
        public decimal Total_Quantity { get; set; }   // or int if you used INT
    }
}
