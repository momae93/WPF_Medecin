using benais_jWPF_Medecin.Common.Exceptions;
using benais_jWPF_Medecin.DataAccess;
using benais_jWPF_Medecin.ServiceUserReference;
using System.Diagnostics;

namespace benais_jWPF_Medecin.BusinessManagement
{
    public class UserBM
    {
        private string _currentLogin;

        public UserBM(string login)
        {
            this._currentLogin = login;
        }

        /// <summary>
        /// Call data access layer disconnect function
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool Disconnect()
        {
            try
            {
                return new UserDA().Disconnect(_currentLogin);
            }
            catch (System.Exception e)
            {
                string header = "[DEBUG][UserBM.Disconnect] ";
                Debug.WriteLine(header + e.Message);

                throw new CustomServerException();
            }
        }

        /// <summary>
        /// Call data access layer getUser function
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            try
            {
                return new UserDA().GetUser(_currentLogin);
            }
            catch (System.Exception e)
            {
                string header = "[DEBUG][UserBM.GetUser] ";
                Debug.WriteLine(header + e.Message);

                throw new CustomServerException();
            }
        }

        /// <summary>
        /// Call data access layer getListUser function
        /// </summary>
        /// <returns></returns>
        public User[] GetListUser()
        {
            try
            {
                return new UserDA().GetListUser();
            }
            catch (System.Exception e)
            {
                string header = "[DEBUG][UserBM.GetListUser] ";
                Debug.WriteLine(header + e.Message);

                throw new CustomServerException();
            }
        }

        /// <summary>
        /// Call data access layer deleteUser function and handle self delete case
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool DeleteUser(string login)
        {
            try
            {
                if (login != _currentLogin)
                    return new UserDA().DeleteUser(login);
                return false;
            }
            catch (System.Exception e)
            {
                string header = "[DEBUG][UserBM.DeleteUser] ";
                Debug.WriteLine(header + e.Message);

                throw new CustomServerException();
            }
        }

        /// <summary>
        /// Call data access layer getRole function return true if matching nurse
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool IsUserReadOnly(string login)
        {
            try
            {
                string role = new UserDA().GetRole(login);
                return role == "Infirmière";
            }
            catch (System.Exception e)
            {
                string header = "[DEBUG][UserBM.IsUserReadOnly] ";
                Debug.WriteLine(header + e.Message);

                throw new CustomServerException();
            }
        }

        /// <summary>
        /// Call data access layer addUser function handles big picture exception
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(User user)
        {
            try
            {
                return new UserDA().AddUser(user);
            }
            catch (System.Exception e)
            {
                string header = "[DEBUG][UserBM.AddUser] ";
                Debug.WriteLine(header + e.Message);

                if (e is System.ServiceModel.ProtocolException)
                    throw new CustomLargePictureException();
                throw new CustomServerException();
            }
        }
    }
}
