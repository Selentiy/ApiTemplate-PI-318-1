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
        public bool BlockStatus { get; set; }
        
        public User(string login,string pass)
        {
            this.BlockStatus = true;
            this.Login = login;
            this.Password = pass;
        }
        public override string ToString()
        {
            return $"User Login: {Login}" + (BlockStatus == true? " Active user " : " User are blocked. ");
        }
    }
}
