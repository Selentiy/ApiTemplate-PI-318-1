using App.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using App.Users.Repository;

namespace App.Users.Services
{
    public interface IUsersManager
    {
        void BlockUserById(int id);
        void UnblockUserById(int id);

        IEnumerable<string> GetActiveUsers();
        void ResetPassword(int id,string pass);
    }
    public class UsersManager : IUsersManager, ITransientDependency
    {
        readonly IUsersRepository _repository;

        public UsersManager(IUsersRepository repository)
        {
            _repository = repository;
        }
        public void BlockUserById(int id)
        {
                _repository.Get(id).IsBlocked = true;
        }
        public void UnblockUserById(int id)
        {
            _repository.Get(id).IsBlocked = false;
        }
        public IEnumerable<string> GetActiveUsers()
        {
           return _repository.GetActiveUsers();
        }
        public void ResetPassword(int id, string pass)
        {
            _repository.Get(id).Password = pass;
        }
    }
}
