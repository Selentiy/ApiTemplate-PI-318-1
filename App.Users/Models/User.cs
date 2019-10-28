using System;
using System.Collections.Generic;
using System.Text;

namespace App.Users.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        
        public User(string login,string pass)
        {
            this.IsBlocked = false;
            this.Login = login;
            this.Password = pass;
        }
        public override string ToString()
        {
            return $"User Login: {Login}" + (IsBlocked ? "User is blocked." : "Active user");
        }
    }
}
