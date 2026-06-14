using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Models
{
    public class UserModel
    {
        public Guid User_ID { get; set; }
        public Guid Role_ID { get; set; }
        public string Full_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid? Created_By { get; set; }
        public DateTime Created_On { get; set; }
        // ── Display Only — NOT a DB column ──
        // Joined from Role table using Role_ID FK
        public string Role_Name { get; set; }
    }
}
