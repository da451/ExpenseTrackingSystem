using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ExpenseTrackingSystem.Model;
using ExpenseTrackingSystem.Notifications;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.View
{
    public class StaticticsViewModel : ViewModelBase
    {
        public StaticticsViewModel()
        {
            ExpenseCollaction = new ExpenseCollection();



            DateFrom = DateTime.Now.AddDays(-7);

            DateTo = DateTime.Now;


        }



        public const string ExpenseCollactionPropertyName = "ExpenseCollaction";

        private ExpenseCollection _expenseCollaction;

        public ExpenseCollection ExpenseCollaction
        {
            get
            {
                return _expenseCollaction;
            }

            set
            {
                if (_expenseCollaction == value)
                {
                    return;
                }

                RaisePropertyChanging(ExpenseCollactionPropertyName);
                _expenseCollaction = value;
                RaisePropertyChanged(ExpenseCollactionPropertyName);
            }
        }




        public const string TotalSpendPropertyName = "TotalSpend";

        private float _totalSpend;

        public float TotalSpend
        {
            get
            {
                return _totalSpend;
            }

            set
            {
                if (_totalSpend == value)
                {
                    return;
                }

                RaisePropertyChanging(TotalSpendPropertyName);
                _totalSpend = value;
                RaisePropertyChanged(TotalSpendPropertyName);
            }
        }

        private float CountTotalSpend()
        {
            float sum = 0;

            if (ExpenseCollaction != null)
            {
                sum = ExpenseCollaction.Sum(o => o.Spend);
            }

            return sum;
        }



        private RelayCommand _loadStatisticCommand;
        
        public RelayCommand LoadStatisticCommand
        {
            get
            {
                return _loadStatisticCommand
                    ?? (_loadStatisticCommand = new RelayCommand(
                                          () =>
                                          {
                                              ExpenseCollaction.LoadGroupByTag(DateFrom,DateTo);

                                              TotalSpend = CountTotalSpend();

                                              CollectionForDiagram = new ObservableCollection<KeyValuePair<string, float>>();

                                              foreach (var i in ExpenseCollaction)
                                              {
                                                  CollectionForDiagram.Add(new KeyValuePair<string, float>(i.Tag.Name, i.Spend));
                                              }
                                          },
                                          () => DateFrom != DateTime.MinValue && DateTo != DateTime.MinValue));
            }
        }



        public const string CollectionForDiagramPropertyName = "CollectionForDiagram";

        private ObservableCollection<KeyValuePair<string, float>> _collectionForDiagram;

        public ObservableCollection<KeyValuePair<string, float>> CollectionForDiagram
        {
            get
            {
                return _collectionForDiagram;
            }

            set
            {
                if (_collectionForDiagram == value)
                {
                    return;
                }

                RaisePropertyChanging(CollectionForDiagramPropertyName);
                _collectionForDiagram = value;
                RaisePropertyChanged(CollectionForDiagramPropertyName);
            }
        }


        private RelayCommand _closeFormCommand;

        public RelayCommand CloseFormCommand
        {
            get
            {
                return _closeFormCommand
                    ?? (_closeFormCommand = new RelayCommand(
                                          () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage(MessengerMessage.CLOSE_STATISTIC_FORM))));
            }
        }


        public const string DateFromPropertyName = "DateFrom";

        private DateTime _dateFrom;

        public DateTime DateFrom
        {
            get
            {
                return _dateFrom;
            }

            set
            {
                if (_dateFrom == value)
                {
                    return;
                }

                RaisePropertyChanging(DateFromPropertyName);
                _dateFrom = value;
                RaisePropertyChanged(DateFromPropertyName);
            }
        }




        public const string DateToPropertyName = "DateTo";

        private DateTime _dateTo;

        public DateTime DateTo
        {
            get
            {
                return _dateTo;
            }

            set
            {
                if (_dateTo == value)
                {
                    return;
                }

                RaisePropertyChanging(DateToPropertyName);
                _dateTo = value;
                RaisePropertyChanged(DateToPropertyName);
            }
        }
    }
}