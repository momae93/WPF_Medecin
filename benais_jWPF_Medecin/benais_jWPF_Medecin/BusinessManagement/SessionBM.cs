using benais_jWPF_Medecin.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benais_jWPF_Medecin.BusinessManagement
{
    public class SessionBM
    {
        private string _login;

        public SessionBM(string login)
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
    }
}
