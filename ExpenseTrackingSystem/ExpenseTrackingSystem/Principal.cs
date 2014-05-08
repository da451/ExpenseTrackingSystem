using System.Security.Principal;

namespace ExpenseTrackingSystem
{
    public class Principal : IPrincipal
    {
        private Identity _identity;

        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return _identity; }
            set { _identity = (Identity) value; }
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        #endregion
    }
}
