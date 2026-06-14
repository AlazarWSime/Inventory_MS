using System;

namespace FormApp.Session
{
    public static class UserSession
    {
        public static Guid User_ID { get; set; }
        public static string Username { get; set; }
        public static string Full_Name { get; set; }
        public static string Role_Name { get; set; }
        public static Guid Role_ID { get; set; }

        public static bool IsLoggedIn => User_ID != Guid.Empty;

        public static void Clear()
        {
            User_ID = Guid.Empty;
            Username = "";
            Full_Name = "";
            Role_Name = "";
            Role_ID = Guid.Empty;
        }
    }
}