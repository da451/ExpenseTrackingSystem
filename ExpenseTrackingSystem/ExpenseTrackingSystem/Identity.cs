using System.Security.Principal;

namespace ExpenseTrackingSystem
{
    public class Identity :IIdentity
    {
        public Identity(int userId, string login)
        {
            UserID = userId;
            Login = login;
        }

        public int UserID { private set; get; }

        public string Login { private set; get; }

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "custom"; }
        }

        public bool IsAuthenticated
        {
            get { return UserID > 0 && !string.IsNullOrEmpty(Login); }
        }

        public string Name
        {
            get { return Login; }
        }

        #endregion
    }
}
