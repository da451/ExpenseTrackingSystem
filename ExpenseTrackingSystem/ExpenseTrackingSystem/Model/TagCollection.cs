using System.Collections.Generic;
using System.Collections.ObjectModel;
using DAL.Entities;
using DAL.Repository.Imp;
using ExpenseTrackingSystem.Extensions;
using GalaSoft.MvvmLight.Command;

namespace ExpenseTrackingSystem.Model
{
    public class TagCollection : ObservableCollection<TagModel>
    {
        private RelayCommand _loadTagsCommand;

        public RelayCommand LoadTagsCommand
        {
            get
            {
                return _loadTagsCommand
                    ?? (_loadTagsCommand = new RelayCommand(LoadTags));
            }
        }

        private void LoadTags()
        {
            UnitOfWork uow = new UnitOfWork();

            this.Clear();

            try
            {
                RepositoryTag repositoryTag = new RepositoryTag(uow);

                uow.BeginTransaction();

                int userID = AuthorizationService.GetUserID();

                IEnumerable<Tag> tags = repositoryTag.LoadUserTags(userID);

                foreach (var tag in tags)
                {
                    this.Add(tag.ToModel());
                }

                uow.Commit();
            }
            catch
            {
                uow.RollBack();
                throw;
            }
        }
    }
}
