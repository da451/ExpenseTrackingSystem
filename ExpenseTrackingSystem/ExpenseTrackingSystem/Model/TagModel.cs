using GalaSoft.MvvmLight;

namespace ExpenseTrackingSystem.Model
{
    public class TagModel : ViewModelBase
    {
        public const string TagIDPropertyName = "TagID";

        private int _tagID;

        public int TagID
        {
            get
            {
                return _tagID;
            }

            set
            {
                if (_tagID == value)
                {
                    return;
                }

                RaisePropertyChanging(TagIDPropertyName);
                _tagID = value;
                RaisePropertyChanged(TagIDPropertyName);
            }
        }



        public const string NamePropertyName = "Name";

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }

                RaisePropertyChanging(NamePropertyName);
                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }



        public const string UserPropertyName = "User";

        private UserModel _user;

        public UserModel User
        {
            get
            {
                return _user;
            }

            set
            {
                if (_user == value)
                {
                    return;
                }

                RaisePropertyChanging(UserPropertyName);
                _user = value;
                RaisePropertyChanged(UserPropertyName);
            }
        }
    }
}
