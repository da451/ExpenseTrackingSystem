using System;
using DAL.Entities;
using DAL.Repository.Imp;
using ExpenseTrackingSystem.Extensions;
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

        private DateTime _date = DateTime.Now;

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



        public void Load(int expenseID)
        {
            UnitOfWork uow = new UnitOfWork();

            RepositoryExpense repositoryExpense = new RepositoryExpense(uow);

            try
            {

                uow.BeginTransaction();

                Expense expense = repositoryExpense.Get(expenseID);

                var buf = expense.ToModel();

                ExpenseID = buf.ExpenseID;

                Date = buf.Date;

                Comment = buf.Comment;

                Spend = buf.Spend;

                Tag = buf.Tag;

                User = buf.User;

                uow.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                uow.RollBack();

                throw;
            }
        }

        public int Save()
        {
            UnitOfWork uow = new UnitOfWork();

            RepositoryExpense repositoryExpense = new RepositoryExpense(uow);

            RepositoryUser repositoryUser = new RepositoryUser(uow);

            int expenseID = -1;

            int userID = AuthorizationService.GetUserID();

            try
            {

                uow.BeginTransaction();

                UserModel user = repositoryUser.Get(userID).ToModel();

                User = user;

                expenseID = repositoryExpense.Save(this.ToEntity());

                uow.Commit();

                this.ExpenseID = expenseID;

                return expenseID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                uow.RollBack();

                throw;
            }
        }

        public void Update()
        {
            UnitOfWork uow = new UnitOfWork();

            RepositoryExpense repositoryExpense = new RepositoryExpense(uow);

            try
            {

                uow.BeginTransaction();

                repositoryExpense.Update(this.ToEntity());

                uow.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                uow.RollBack();

                throw;
            }
        }

        public void Delete()
        {
            UnitOfWork uow = new UnitOfWork();

            RepositoryExpense repositoryExpense = new RepositoryExpense(uow);

            try
            {

                uow.BeginTransaction();

                repositoryExpense.Delete(this.ToEntity());

                uow.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                uow.RollBack();

                throw;
            }
        }
    }
}
