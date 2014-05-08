using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace ExpenseTrackingSystem.Model
{
    public class UserModel : ViewModelBase
    {
        public const string UserIDPropertyName = "UserID";

        private int _userID;

        public int UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                if (_userID == value)
                {
                    return;
                }

                RaisePropertyChanging(UserIDPropertyName);
                _userID = value;
                RaisePropertyChanged(UserIDPropertyName);
            }
        }



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



        public const string TagsPropertyName = "Tags";

        private ObservableCollection<TagModel> _tags;

        public ObservableCollection<TagModel> Tags
        {
            get
            {
                return _tags;
            }

            set
            {
                if (_tags == value)
                {
                    return;
                }

                RaisePropertyChanging(TagsPropertyName);
                _tags = value;
                RaisePropertyChanged(TagsPropertyName);
            }
        }

    }
}
