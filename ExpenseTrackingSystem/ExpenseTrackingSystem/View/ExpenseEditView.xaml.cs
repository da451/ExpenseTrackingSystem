using System.Windows;

namespace ExpenseTrackingSystem.View
{
    /// <summary>
    /// Description for ExpenseEditView.
    /// </summary>
    public partial class ExpenseEditView : Window
    {
        /// <summary>
        /// Initializes a new instance of the ExpenseEditView class.
        /// </summary>
        public ExpenseEditView()
        {
            InitializeComponent();

            DataContext = new ExpenseEditViewModel();
        }
    }
}