using System.Windows;
using ScheduleApp.UI.ViewModels;

namespace ScheduleApp.UI.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public LoginWindow(LoginViewModel loginViewModel)
        {
            InitializeComponent();

            DataContext = loginViewModel;
            LoginViewControl.DataContext = loginViewModel;
        }
    }
}