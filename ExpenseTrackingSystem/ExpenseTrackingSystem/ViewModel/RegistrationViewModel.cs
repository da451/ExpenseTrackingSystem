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
    public class RegistrationViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly string[] _validationProperties = new string[] { FIOPropertyName, LoginPropertyName, PasswordPropertyName, ConfirmPasswordPropertyName};

        public const string FIOPropertyName = "FIO";

        private string _fio;

        public string FIO
        {
            get
            {
                return _fio;
            }

            set
            {
                if (_fio == value)
                {
                    return;
                }

                RaisePropertyChanging(FIOPropertyName);
                _fio = value;
                RaisePropertyChanged(FIOPropertyName);
            }
        }



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



        public const string ConfirmPasswordPropertyName = "ConfirmPassword";

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }

            set
            {
                if (_confirmPassword == value)
                {
                    return;
                }

                RaisePropertyChanging(ConfirmPasswordPropertyName);
                _confirmPassword = value;
                RaisePropertyChanged(ConfirmPasswordPropertyName);
            }
        }



        private RelayCommand _closeRegistrationFormCommand;

        public RelayCommand CloseRegistrationFormCommand
        {
            get
            {
                return _closeRegistrationFormCommand
                    ?? (_closeRegistrationFormCommand = new RelayCommand(
                                          () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage(MessengerMessage.CLOSE_REGISTRATION_FORM))));
            }
        }


        private RelayCommand _userRegistrationCommand;

        public RelayCommand UserRegistrationCommand
        {
            get
            {
                return _userRegistrationCommand
                    ?? (_userRegistrationCommand = new RelayCommand(
                                          () =>
                                          {
                                              try
                                              {
                                                  bool isRegistrated = AuthorizationService.RegisterUser(FIO, Login, Password);

                                                  if (isRegistrated)
                                                          Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>(Login, MessengerMessage.SUCCESSFUL_REGISTRATION));
                                                      else
                                                          Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>(Login, MessengerMessage.UNSUCCESSFUL_REGISTRATION));

                                              }
                                              catch (Exception ex)
                                              {
                                                  Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>(ex.ToString(), MessengerMessage.ERROR_MESSAGE_REGISTRATION));
                                              }
                                              

                                              
                                          },
                                          IsAllFieldValid));
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

                if (columnName == FIOPropertyName)
                {
                    if (string.IsNullOrEmpty(FIO))
                        result = "Enter FIO";
                }

                if (columnName == LoginPropertyName)
                {
                    if (string.IsNullOrEmpty(Login))
                        result = "Enter Login";
                    else if (Login.Contains(" "))
                        result = "Login contains spaces";
                    else if(!AuthorizationService.IsValidLogin(Login))
                        result = "Such login already exist";
                }

                if (columnName == PasswordPropertyName)
                {
                    if (string.IsNullOrEmpty(Password))
                        result = "Enter Password";
                    else if(Password.Contains(" "))
                        result = "Password contains spaces";
                }

                if (columnName == ConfirmPasswordPropertyName)
                {
                    if (string.IsNullOrEmpty(ConfirmPassword))
                        result = "Enter Password";
                    else if (ConfirmPassword.Contains(" "))
                        result = "Password contains spaces";
                    else if (ConfirmPassword!=Password)
                        result = "Passwords not equal";
                }

                return result;
            }

        }

        #endregion
    }
}