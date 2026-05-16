using System.Windows;
using ScheduleApp.UI.Views;
using ScheduleApp.UI.ViewModels;

namespace ScheduleApp.UI
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            //var loginViewModel = new LoginViewModel();
            //var loginWindow = new LoginWindow();

            //loginWindow.DataContext = loginViewModel;

            //loginViewModel.OnLoginSuccess += () =>
            {
                //string rolUsuario = loginViewModel.RolUsuario;

                MainWindow mainWindow = new MainWindow("Administrador");
                mainWindow.Show();

                //loginWindow.Close();
            };

            //loginWindow.Show();
        }
    }
}