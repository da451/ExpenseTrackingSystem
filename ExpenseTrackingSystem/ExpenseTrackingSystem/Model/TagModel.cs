using System;
using DAL.Repository.Imp;
using ExpenseTrackingSystem.Extensions;
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

        public void Update()
        {
            UnitOfWork uow = new UnitOfWork();
            
            RepositoryTag repositoryTag = new RepositoryTag(uow);
            
            try
            {
                uow.BeginTransaction();

                repositoryTag.Update(this.ToEntity());

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

            RepositoryTag repositoryTag = new RepositoryTag(uow);

            try
            {
                uow.BeginTransaction();

                repositoryTag.Delete(this.ToEntity());

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

            RepositoryTag repositoryTag = new RepositoryTag(uow);

            RepositoryUser repositoryUser = new RepositoryUser(uow);

            int tagID = -1;

            int userID = AuthorizationService.GetUserID();

            try
            {

                uow.BeginTransaction();

                UserModel user = repositoryUser.Get(userID).ToModel();

                User = user;

                tagID = repositoryTag.Save(this.ToEntity());

                uow.Commit();

                this.TagID = tagID;

                return tagID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                uow.RollBack();

                throw;
            }

        }

        protected bool Equals(TagModel other)
        {
            return _tagID == other._tagID && string.Equals(_name, other._name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TagModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_tagID*397) ^ (_name != null ? _name.GetHashCode() : 0);
            }
        }

        public static bool operator ==(TagModel left, TagModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TagModel left, TagModel right)
        {
            return !Equals(left, right);
        }
    }
}
