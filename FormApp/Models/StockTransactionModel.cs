using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Models
{
    public class StockTransactionModel
    {
        public Guid Transaction_ID { get; set; }
        public Guid User_ID { get; set; }
        public Guid Item_ID { get; set; }
        // Values: Purchase/Sale/Transfer/Adjustment
        public string Transaction_Type {  get; set; }
        public decimal Quantity { get; set; }
        public Guid? Zone_ID { get; set; }
        public DateTime Transaction_Date { get; set; }
        // Joined from Item — display only
        public string Item_Name { get; set; }

        // Joined from Location — display only
        public string Zone_Name { get; set; }

        // Joined from [User] — display only
        public string Username { get; set; }
    }
}
