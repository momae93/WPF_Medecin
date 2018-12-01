using benais_jWPF_Medecin.Common.Exceptions;
using benais_jWPF_Medecin.DataAccess;
using benais_jWPF_Medecin.ServiceUserReference;
using System.Collections.Generic;

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
            try
            {
                return new UserDA().Disconnect(_login);
            }
            catch (System.Exception e)
            {
                if (e is System.ServiceModel.EndpointNotFoundException)
                    throw new CustomServerException();
                return false;
            }
        }

        public User GetUser()
        {
            try
            {
                return new UserDA().GetUser(_login);
            }
            catch (System.Exception e)
            {
                if (e is System.ServiceModel.EndpointNotFoundException)
                    throw new CustomServerException();
                return null;
            }
        }

        public User[] GetListUser()
        {
            try
            {
                return new UserDA().GetListUser();
            }
            catch (System.Exception e)
            {
                if (e is System.ServiceModel.EndpointNotFoundException)
                    throw new CustomServerException();
                return null;
            }
        }

        public bool DeleteUser(string login)
        {
            try
            {
                return new UserDA().DeleteUser(login);
            }
            catch (System.Exception e)
            {
                if (e is System.ServiceModel.EndpointNotFoundException)
                    throw new CustomServerException();
                return false;
            }
        }

        public bool IsUserReadOnly(string login)
        {
            try
            {
                string role = new UserDA().GetRole(login);
                return role == "Infirmière";
            }
            catch (System.Exception e)
            {
                if (e is System.ServiceModel.EndpointNotFoundException)
                    throw new CustomServerException();
                return false;
            }

        }

        public bool AddUser(User user)
        {
            try
            {
                return new UserDA().AddUser(user);
            }
            catch (System.Exception e)
            {
                if (e is System.ServiceModel.ProtocolException)
                    throw new CustomLargePictureException();
                if (e is System.ServiceModel.EndpointNotFoundException)
                    throw new CustomServerException();
                return false;
            }
        }
    }
}
