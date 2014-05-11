/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:ExpenseTrackingSystem.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System;
using System.IO.Packaging;
using DAL;
using ExpenseTrackingSystem.Model;
using ExpenseTrackingSystem.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ExpenseTrackingSystem.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

                SimpleIoc.Default.Register<ExpensesViewModel>();

                SimpleIoc.Default.Register<LogInViewModel>();

                SimpleIoc.Default.Register<TagsViewModel>();

                SimpleIoc.Default.Register<ExpenseEditViewModel>();

                SimpleIoc.Default.Register<StaticticsViewModel>();
            }

            
            FNHHelper.CreateInstance(Properties.Settings.Default.ConnectionString);
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]

        public LogInViewModel LogIn
        {
            get
            {
                if (ViewModelBase.IsInDesignModeStatic)
                    return ServiceLocator.Current.GetInstance<LogInViewModel>();
                else
                    return null;

            }
        }

        public TagsViewModel Tags
        {
            get
            {
                if (ViewModelBase.IsInDesignModeStatic)
                {
                    TagsViewModel tags = ServiceLocator.Current.GetInstance<TagsViewModel>();

                    if (ViewModelBase.IsInDesignModeStatic)
                    {
                        tags.Tags.Add(new TagModel()
                        {
                            Name = "Food",
                            TagID = 1
                        });

                        tags.Tags.Add(new TagModel()
                        {
                            Name = "Car",
                            TagID = 2
                        });

                        tags.Tags.Add(new TagModel()
                        {
                            Name = "Pet",
                            TagID = 3
                        });
                    }

                    return tags;
                }
                else
                    return null;


            }
        }

        public ExpensesViewModel Expenses
        {
            get
            {
                if (ViewModelBase.IsInDesignModeStatic)
                {
                    ExpensesViewModel e = ServiceLocator.Current.GetInstance<ExpensesViewModel>();

                    if (ViewModelBase.IsInDesignModeStatic)
                    {
                        UserModel user = new UserModel()
                        {
                            FIO = "Z D A",
                            Login = "da451",
                            UserID = 1
                        };

                        TagModel tag = new TagModel() { Name = "Food", TagID = 1, User = user };

                        TagModel tag2 = new TagModel() { Name = "Car", TagID = 2, User = user };

                        e.ListOfExpenses.Add(new ExpenseModel()
                        {
                            Comment = "Milk",
                            Date = DateTime.Now,
                            ExpenseID = 1,
                            Spend = 451,
                            Tag = tag,
                            User = user
                        });

                        e.ListOfExpenses.Add(new ExpenseModel()
                        {
                            Comment = "Bread",
                            Date = DateTime.Now,
                            ExpenseID = 2,
                            Spend = 100,
                            Tag = tag,
                            User = user
                        });

                        e.ListOfExpenses.Add(new ExpenseModel()
                        {
                            Comment = "Gas and alot of letters! And more... and more... and more... a littel more",
                            Date = DateTime.Now,
                            ExpenseID = 3,
                            Spend = 2000,
                            Tag = tag2,
                            User = user
                        });

                    }

                    return e;
                }
                else
                    return null;


            }
        }

        public ExpenseEditViewModel ExpenseEdit
        {
            get
            {
                if (ViewModelBase.IsInDesignModeStatic)
                    return ServiceLocator.Current.GetInstance<ExpenseEditViewModel>();
                else
                    return null;

            }
        }

        public StaticticsViewModel Statictics
        {
            get
            {
                if (ViewModelBase.IsInDesignModeStatic)
                    return ServiceLocator.Current.GetInstance<StaticticsViewModel>();
                else
                    return null;
            }
        }
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}