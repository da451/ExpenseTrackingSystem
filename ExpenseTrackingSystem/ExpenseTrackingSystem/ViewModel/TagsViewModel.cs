﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using DAL.Entities;
using DAL.Repository.Imp;
using ExpenseTrackingSystem.Extensions;
using ExpenseTrackingSystem.Model;
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
    public class TagsViewModel : ViewModelBase
    {
        public TagsViewModel()
        {
            Tags = new ObservableCollection<TagModel>();
            Load();
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



        public const string SelectedTagPropertyName = "SelectedTag";

        private TagModel _selectedTag;

        public TagModel SelectedTag
        {
            get
            {
                return _selectedTag;
            }

            set
            {
                if (_selectedTag == value)
                {
                    return;
                }

                //We check state of selected item. If it was changed in UI, and wasn't saved, we will return previouse state
                if (_stateSelectedTag!=null
                    && _selectedTag != null
                    && _stateSelectedTag.TagID == _selectedTag.TagID
                    && _stateSelectedTag.Name != _selectedTag.Name)
                {
                    _selectedTag.Name = _stateSelectedTag.Name;
                }

                RaisePropertyChanging(SelectedTagPropertyName);
                _selectedTag = value;
                RaisePropertyChanged(SelectedTagPropertyName);

                if (_stateSelectedTag==null 
                    || (_selectedTag!=null && _stateSelectedTag.TagID != _selectedTag.TagID))
                {
                    _stateSelectedTag = new TagModel()
                    {
                        TagID = value.TagID,
                        Name = value.Name
                    };

                    TagName = value.Name;

                    _isNeedUpdating = false;
                }

            }
        }



        /// <summary>
        /// Store curent selected Tag(not changed state)
        /// </summary>
        private TagModel _stateSelectedTag;



        /// <summary>
        /// TagName is need for enabling Save Button.
        /// If Property TagName changed, then button Save will be enabled
        /// </summary>
        public const string TagNamePropertyName = "TagName";

        private string _tagName;

        public string TagName
        {
            get
            {
                return _tagName;
            }

            set
            {
                if (_tagName == value)
                {
                    return;
                }
                else
                {
                    _isNeedUpdating = true;
                }

                RaisePropertyChanging(TagNamePropertyName);
                _tagName = value;
                _selectedTag.Name = _tagName;
                RaisePropertyChanged(TagNamePropertyName);
            }
        }



        private void Load()
        {
            UnitOfWork uow = new UnitOfWork();

            _tags.Clear();

            try
            {
                RepositoryTag repositoryTag = new RepositoryTag(uow);

                uow.BeginTransaction();

                int userID = AuthorizationService.GetUserID();

                IEnumerable<Tag> tags = repositoryTag.LoadUserTags(userID);

                foreach (var tag in tags)
                {
                    _tags.Add(tag.ToModel());
                }

                uow.Commit();
            }
            catch
            {
                uow.RollBack();
                throw;
            }

        }



        public const string NewTagPropertyName = "NewTag";

        private TagModel _newTag;

        public TagModel NewTag
        {
            get
            {
                return _newTag;
            }

            set
            {
                if (_newTag == value)
                {
                    return;
                }

                RaisePropertyChanging(NewTagPropertyName);
                _newTag = value;
                RaisePropertyChanged(NewTagPropertyName);
            }
        }

        private RelayCommand _saveTagCommand;

        public RelayCommand SaveTagCommand
        {
            get
            {
                return _saveTagCommand
                    ?? (_saveTagCommand = new RelayCommand(
                                          () =>
                                          {
                                              NewTag.Save();

                                              Tags.Add(NewTag);

                                              NewTag = null;
                                          }));
            }
        }



        private bool _isNeedUpdating = false;

        private RelayCommand _updateTagCommand;

        public RelayCommand UpdateTagCommand
        {
            get
            {
                return _updateTagCommand
                    ?? (_updateTagCommand = new RelayCommand(
                                          () =>
                                          {
                                              _selectedTag.Update();

                                              _stateSelectedTag.Name = _selectedTag.Name;

                                              _isNeedUpdating = false;
                                          },
                                          () => _isNeedUpdating));
            }
        }



        private RelayCommand _deleteTagCommand;

        public RelayCommand DeleteTagCommand
        {
            get
            {
                return _deleteTagCommand
                    ?? (_deleteTagCommand = new RelayCommand(
                                          () =>
                                          {
                                              _selectedTag.Delete();

                                              _tags.Remove(_selectedTag);

                                              _tagName = "";

                                              _selectedTag = null;

                                              _stateSelectedTag = null;
                                          }));
            }
        }


        private RelayCommand _closeFormCommand;

        public RelayCommand CloseFormCommand
        {
            get
            {
                return _closeFormCommand
                    ?? (_closeFormCommand = new RelayCommand(
                                          () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage(MessengerMessage.CLOSE_TAGS_FORM))));
            }
        }


    }
}