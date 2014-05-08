using System;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace ExpenseTrackingSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Principal principal = new Principal();
            AppDomain.CurrentDomain.SetThreadPrincipal(principal);
        }
    }
}
