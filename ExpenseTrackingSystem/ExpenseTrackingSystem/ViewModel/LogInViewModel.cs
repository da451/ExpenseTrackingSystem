using System;
using System.ComponentModel;
using ExpenseTrackingSystem.Notifications;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LogInViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly string[] _validationProperties = new string[] {LoginPropertyName, PasswordPropertyName};

        public const string LoginPropertyName = "Login";
        
        private string _login;

        public string Login
        {
            get
            {
                return _login;
            }

            set
            {
                if (_login == value)
                {
                    return;
                }

                RaisePropertyChanging(LoginPropertyName);
                _login = value;
                RaisePropertyChanged(LoginPropertyName);
            }
        }



        public const string PasswordPropertyName = "Password";

        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                if (_password == value)
                {
                    return;
                }

                RaisePropertyChanging(PasswordPropertyName);
                _password = value;
                RaisePropertyChanged(PasswordPropertyName);
            }
        }

        

        private RelayCommand _loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand
                    ?? (_loginCommand = new RelayCommand(
                                          () =>
                                          {
                                              int userID = -1;
                                              try
                                              {
                                                  userID = AuthorizationService.LogIn(Login, Password);

                                                  if (userID > 0)
                                                      Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>(Login,MessengerMessage.SUCCESSFUL_LOGIN));
                                                  else
                                                      Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>(Login, MessengerMessage.UNSUCCESSFUL_LOGIN));

                                              }
                                              catch (Exception ex)
                                              {
                                                  Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>(ex.ToString(), MessengerMessage.ERROR_MESSAGE_LOGIN));
                                              }
                                          }, 
                                          IsAllFieldValid));
            }
        }



        private RelayCommand _openRegistrationFormCommand;

        public RelayCommand OpemRegistrationFormCommand
        {
            get
            {
                return _openRegistrationFormCommand
                    ?? (_openRegistrationFormCommand = new RelayCommand(
                                          () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage(MessengerMessage.OPEN_REGISTRATION_FORM))));
            }
        }



        private RelayCommand _closeLoginFormCommand;

        public RelayCommand CloseLoginFormCommand
        {
            get
            {
                return _closeLoginFormCommand
                    ?? (_closeLoginFormCommand = new RelayCommand(
                                          () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage(MessengerMessage.CLOSE_LOGIN_FORM))));
            }
        }


        private bool IsAllFieldValid()
        {
            bool isValid = true;

            foreach (var validationProperty in _validationProperties)
            {
                isValid &= string.IsNullOrEmpty(this[validationProperty]);

                if (!isValid)
                    break;
            }

            return isValid;
        }


        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new System.NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = "";

                if (columnName == LoginPropertyName)
                {
                    if (string.IsNullOrEmpty(Login))
                        result = "Enter Login";
                }

                if (columnName == PasswordPropertyName)
                {
                    if (string.IsNullOrEmpty(Password))
                        result = "Enter Password";
                }

                return result;

            }
        }

        #endregion
    }
}