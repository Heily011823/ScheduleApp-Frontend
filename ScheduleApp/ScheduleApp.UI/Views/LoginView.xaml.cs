using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ScheduleApp.UI.ViewModels; // Agregado para reconocer el ViewModel

namespace ScheduleApp.UI.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        // El método BtnLogin_Click se eliminó porque ahora todo lo maneja el ViewModel mediante comandos

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TxtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtUsuario.Text == "Ingresa tu usuario o correo")
            {
                TxtUsuario.Text = "";
                TxtUsuario.Foreground = Brushes.Black;
            }
        }

        private void TxtUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtUsuario.Text))
            {
                TxtUsuario.Text = "Ingresa tu usuario o correo";
                TxtUsuario.Foreground = Brushes.Gray;
            }
        }

        private void TxtContrasena_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // 1. Manejo visual del placeholder
            if (TxtPlaceholderPassword != null)
            {
                TxtPlaceholderPassword.Visibility = string.IsNullOrEmpty(TxtContrasena.Password)
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }

            // 2. PUENTE CLAVE: Pasa la contraseña escrita al ViewModel en tiempo real
            if (this.DataContext is LoginViewModel viewModel)
            {
                viewModel.Contrasena = TxtContrasena.Password;
            }
        }
    }
}