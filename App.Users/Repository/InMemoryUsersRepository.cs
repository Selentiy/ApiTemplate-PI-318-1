using System.Collections.Generic;
using App.Configuration;
using App.Users.Models;

namespace App.Users.Repository
{   public interface IUsersRepository
    {
        IEnumerable<string> GetActiveUsers();
        User Get(int id);
    }

    public class InMemoryUsersRepository : IUsersRepository, ISingletoneDependency
    {
        public User[] users = new User[5];
        public InMemoryUsersRepository()
        {
            users[0] = new User("docent", "1212");
            users[1] = new User("webterror", "vikaanik");
            users[2] = new User("moki", "marmok");
            users[3] = new User("AntonBeetroot", "GoodParol123");
            users[4] = new User("Vitor", "parol");
        }
        public User Get(int id)
        {
            return users[id];
        }

        public IEnumerable<string> GetActiveUsers()
        {
            string[] str = new string[5];
            str[0] = users[0].ToString();
            str[1] = users[1].ToString();
            str[2] = users[2].ToString();
            str[3] = users[3].ToString();
            str[4] = users[4].ToString();
            return str;
        }
    }
}
