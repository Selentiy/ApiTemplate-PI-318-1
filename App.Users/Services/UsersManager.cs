using App.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using App.Users.Repository;

namespace App.Users.Services
{
    public interface IUsersManager
    {
        void BlockOrUnblockUserById(int id);
        IEnumerable<string> GetListActiveUsers();
        void ResetPassword(int id,string pass);
    }
    public class UsersManager : IUsersManager, ITransientDependency
    {
        InMemoryUsersRepository repository = new InMemoryUsersRepository();

        public void BlockOrUnblockUserById(int id)
        {
            if (repository.Get(id).BlockStatus == true)
                repository.users[id].BlockStatus = false;
            else repository.users[id].BlockStatus = true;
        }

        public IEnumerable<string> GetListActiveUsers()
        {
           return repository.GetStringListActiveUsers();
        }

        public void ResetPassword(int id, string pass)
        {
            repository.users[id].Password = pass;
        }
    }
}
