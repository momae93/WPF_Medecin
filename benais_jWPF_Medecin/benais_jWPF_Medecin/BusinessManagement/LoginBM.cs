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
            return new UserDA().Connect(login, password);
        }
    }
}
