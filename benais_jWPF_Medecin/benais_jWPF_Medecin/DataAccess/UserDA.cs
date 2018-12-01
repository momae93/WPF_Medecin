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

        /// <summary>
        /// Call WCF Connect function
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Connect(string login, string password)
        {
            return _service.Connect(login, password);
        }

        /// <summary>
        /// Call WCF Disconnect function
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Call WCF GetListUser function
        /// </summary>
        /// <returns></returns>
        public User[] GetListUser()
        {
            return _service.GetListUser();
        }

        /// <summary>
        /// Call WCF GetUser function
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public User GetUser(string login)
        {
            return _service.GetUser(login);
        }

        /// <summary>
        /// Call WCF AddUser function
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(User user)
        {
            return _service.AddUser(user);
        }

        /// <summary>
        /// Call WCF DeleteUser function
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool DeleteUser(string login)
        {
            return _service.DeleteUser(login);
        }

        /// <summary>
        /// Call WCF GetRole function
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public string GetRole(string login)
        {
            return _service.GetRole(login);
        }
    }
}