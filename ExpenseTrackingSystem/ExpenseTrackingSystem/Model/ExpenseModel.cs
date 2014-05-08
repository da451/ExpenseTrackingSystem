using System;
using GalaSoft.MvvmLight;

namespace ExpenseTrackingSystem.Model
{
    public class ExpenseModel : ViewModelBase
    {
        public const string ExpenseIDPropertyName = "ExpenseID";

        private int _expenseID;

        public int ExpenseID
        {
            get
            {
                return _expenseID;
            }

            set
            {
                if (_expenseID == value)
                {
                    return;
                }

                RaisePropertyChanging(ExpenseIDPropertyName);
                _expenseID = value;
                RaisePropertyChanged(ExpenseIDPropertyName);
            }
        }



        public const string DatePropertyName = "Date";

        private DateTime _date;

        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                if (_date == value)
                {
                    return;
                }

                RaisePropertyChanging(DatePropertyName);
                _date = value;
                RaisePropertyChanged(DatePropertyName);
            }
        }



        public const string SpendPropertyName = "Spend";

        private float _spend;

        public float Spend
        {
            get
            {
                return _spend;
            }

            set
            {
                if (_spend == value)
                {
                    return;
                }

                RaisePropertyChanging(SpendPropertyName);
                _spend = value;
                RaisePropertyChanged(SpendPropertyName);
            }
        }



        public const string CommentPropertyName = "Comment";

        private string _comment;

        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                if (_comment == value)
                {
                    return;
                }

                RaisePropertyChanging(CommentPropertyName);
                _comment = value;
                RaisePropertyChanged(CommentPropertyName);
            }
        }



        public const string TagPropertyName = "Tag";

        private TagModel _tag;

        public TagModel Tag
        {
            get
            {
                return _tag;
            }

            set
            {
                if (_tag == value)
                {
                    return;
                }

                RaisePropertyChanging(TagPropertyName);
                _tag = value;
                RaisePropertyChanged(TagPropertyName);
            }
        }



        public const string UserPropertyName = "User";

        private UserModel _userModel;

        public UserModel User
        {
            get
            {
                return _userModel;
            }

            set
            {
                if (_userModel == value)
                {
                    return;
                }

                RaisePropertyChanging(UserPropertyName);
                _userModel = value;
                RaisePropertyChanged(UserPropertyName);
            }
        }
    }
}
