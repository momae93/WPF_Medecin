using benais_jWPF_Medecin.DataAccess;
using benais_jWPF_Medecin.ServiceUserReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benais_jWPF_Medecin.BusinessManagement
{
    public class UserBM
    {
        private string _login;

        public UserBM(string login)
        {
            this._login = login;
        }

        /// <summary>
        /// Call data access layer disconnect
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool Disconnect()
        {
            return new UserDA().Disconnect(_login);
        }

        public User GetUser()
        {
            return new UserDA().GetUser(_login);
        }

        public User[] GetListUser()
        {
            return new UserDA().GetListUser();
        }

        public bool DeleteUser(string login)
        {
            return new UserDA().DeleteUser(login);
        }

        public bool AddUser(User user)
        {
            return new UserDA().AddUser(user);
        }
    }
}
