using ExpenseTrackingSystem.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ExpenseTrackingSystem.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LogInViewModel : ViewModelBase
    {
        public const string LoginUserPropertyName = "LoginUser";

        private UserModel _loginUser;

        public UserModel LoginUser
        {
            get
            {
                return _loginUser;
            }

            set
            {
                if (_loginUser == value)
                {
                    return;
                }

                RaisePropertyChanging(LoginUserPropertyName);
                _loginUser = value;
                RaisePropertyChanged(LoginUserPropertyName);
            }
        }


        private RelayCommand _loginCommand;

        /// <summary>
        /// Gets the LoginCommand.
        /// </summary>
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand
                    ?? (_loginCommand = new RelayCommand(
                                          () =>
                                          {
                                              
                                          },
                                          () => true));
            }
        }
        
    }
}