using benais_jWPF_Medecin.Common.Exceptions;
using benais_jWPF_Medecin.DataAccess;

namespace benais_jWPF_Medecin.BusinessManagement
{
    public class LoginBM
    {
        public LoginBM()
        { }

        /// <summary>
        /// Call data access layer connect
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Connect(string login, string password)
        {
            try
            {
                return new UserDA().Connect(login, password);
            }
            catch (System.Exception e)
            {
                if (e is System.ServiceModel.EndpointNotFoundException)
                    throw new CustomServerException();
                else
                    return false;
            }
        }
    }
}
