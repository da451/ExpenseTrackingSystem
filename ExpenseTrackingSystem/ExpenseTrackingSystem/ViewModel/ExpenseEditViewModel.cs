using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DAL.Entities;
using DAL.Repository.Imp;
using ExpenseTrackingSystem.Model;
using ExpenseTrackingSystem.Notifications;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.View
{
    public class ExpenseEditViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly string[] _validationProperties = new string[] { DatePropertyName, SpendPropertyName, SelectedTagPropertyName };

        public ExpenseEditViewModel()
        {

            Tags = new TagCollection();
            try
            {
                Tags.LoadTagsCommand.Execute(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Messenger.Default.Send<NotificationMessage<string>>(
                    new NotificationMessage<string>(e.ToString(),MessengerMessage.ERROR_MESSAGE_EXPENSE_EDIT));

                CloseFormCommand.Execute(null);
            }
        }

        public const string TagsPropertyName = "Tags";

        private TagCollection _tags;

        public TagCollection Tags
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

                RaisePropertyChanging(SelectedTagPropertyName);
                _selectedTag = value;
                RaisePropertyChanged(SelectedTagPropertyName);
            }
        }


        public int ExpenseID
        {
            set
            {
                try
                {
                    Expense = new ExpenseModel();

                    Expense.Load(value);

                    Date = Expense.Date;

                    Comment = Expense.Comment;

                    Spend = Expense.Spend;

                    SelectedTag = Expense.Tag;

                }
                catch (Exception e)
                {

                    Console.WriteLine(e);

                    Messenger.Default.Send<NotificationMessage<string>>(
                        new NotificationMessage<string>(e.ToString(), MessengerMessage.ERROR_MESSAGE_EXPENSE_EDIT));

                    CloseFormCommand.Execute(null);
                }

            }
            get
            {
                return Expense != null ? Expense.ExpenseID : -1;
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



        public const string ExpensePropertyName = "Expense";

        private ExpenseModel _expense;

        public ExpenseModel Expense
        {
            get
            {
                return _expense;
            }

            set
            {
                if (_expense == value)
                {
                    return;
                }

                RaisePropertyChanging(ExpensePropertyName);
                _expense = value;
                RaisePropertyChanged(ExpensePropertyName);
            }
        }


        
        private RelayCommand _saveExpenseCommand;

        public RelayCommand SaveExpenseCommand
        {
            get
            {
                return _saveExpenseCommand
                    ?? (_saveExpenseCommand = new RelayCommand(
                                          () =>
                                          {
                                              try
                                              {

                                                  if (Expense != null)
                                                  {
                                                      Expense.Date = Date;

                                                      Expense.Comment = Comment;

                                                      Expense.Spend = Spend;

                                                      Expense.Tag = SelectedTag;

                                                      Expense.Update();
                                                  }
                                                  else
                                                  {
                                                      ExpenseModel expenseModel = new ExpenseModel()
                                                      {
                                                          Comment = Comment,
                                                          Date = Date,
                                                          Spend = Spend,
                                                          Tag = SelectedTag
                                                      };
                                                      expenseModel.Save();
                                                  }

                                              }
                                              catch (Exception e)
                                              {
                                                  Console.WriteLine(e);

                                                  Messenger.Default.Send<NotificationMessage<string>>(
                                                      new NotificationMessage<string>(e.ToString(), MessengerMessage.ERROR_MESSAGE_EXPENSE_EDIT));
                                              }
                                              CloseFormCommand.Execute(null);

                                          },
                                          IsAllFieldValid));
            }
        }

        private RelayCommand _closeFormCommand;

        public RelayCommand CloseFormCommand
        {
            get
            {
                return _closeFormCommand
                    ?? (_closeFormCommand = new RelayCommand(
                                          () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage(MessengerMessage.CLOSE_EXPENSE_EDIT_FORM))));
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

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = "";


                if (columnName == SpendPropertyName)
                {
                    if (Spend <= 0)
                    {
                        result = "Not corect value";
                    }
                }

                if (columnName == DatePropertyName)
                {
                    if (Date == DateTime.MinValue)
                    {
                        result = "Date not selected";
                    }
                }

                if (columnName == SelectedTagPropertyName)
                {
                    if (SelectedTag == null)
                    {
                        result = "Tag not selected";
                    }
                }

                return result;
            }
        }
    }
}