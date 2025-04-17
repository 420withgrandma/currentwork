using System;
using Microsoft.EntityFrameworkCore;

namespace GoTimelyAPI
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Constructor for initializing values, EF will handle the database operations
        public Admin(int adminID, string username, string password)
        {
            AdminID = adminID;
            Username = username;
            Password = password;
        }
    }
}
