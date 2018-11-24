using benais_jWPF_Medecin.ServiceUserReference;
using System;

namespace benais_jWPF_Medecin.DataAccess
{
    public class UserDA
    {
        private ServiceUserClient _service;

        public UserDA()
        {
            _service = new ServiceUserClient();
        }

        public bool Connect(string login, string password)
        {
            return _service.Connect(login, password);
        }

        public bool Disconnect(string login)
        {
            try
            {
                _service.Disconnect(login);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User[] GetListUser()
        {
            return _service.GetListUser();
        }

        public User GetUser(string login)
        {
            return _service.GetUser(login);
        }

        public bool AddUser(User user)
        {
            return _service.AddUser(user);
        }

        public bool DeleteUser(string login)
        {
            return _service.DeleteUser(login);
        }

        public string GetRole(string login)
        {
            return _service.GetRole(login);
        }
    }
}