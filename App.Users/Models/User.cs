using System;
using System.Collections.Generic;
using System.Text;

namespace App.Users.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        //if true-unblocked
        public bool IsBlocked { get; set; }
        
        public User(string login,string pass)
        {
            this.IsBlocked = false;
            this.Login = login;
            this.Password = pass;
        }
        public override string ToString()
        {
            return $"User Login: {Login}" + (IsBlocked == false? " Active user " : " User are blocked. ");
        }
    }
}
