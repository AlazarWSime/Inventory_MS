using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Models
{
    public class SupplierModel
    {
        public Guid Supplier_ID { get; set; }
        public string Supplier_Name { get; set; }
        public string Contact_Email { get; set; }
        public string Contact_Phone { get; set; }
        public string Address { get; set; }
        
    }
}
