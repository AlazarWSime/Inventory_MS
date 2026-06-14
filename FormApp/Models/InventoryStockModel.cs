using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Models
{
    public class InventoryStockModel
    {
        public Guid Stock_ID { get; set; }
        public Guid Item_ID { get; set; } 
        public Guid Zone_ID { get; set; }
        public decimal Quantity_On_Hand { get; set; }
        public int Reorder_Level { get; set; }
        // ── Display Only — NOT a DB column ──
        // Joined from Item table using Item_ID FK
        public string Item_Name { get; set; }
        // ── Display Only — NOT a DB column ──
        // Joined from Zone table using Zone_ID FK
        public string Zone_Name { get; set; }

    }
}
