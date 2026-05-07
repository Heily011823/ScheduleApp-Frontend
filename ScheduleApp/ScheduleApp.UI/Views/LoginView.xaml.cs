using System.Windows;
using System.Windows.Controls;

namespace ScheduleApp.UI.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = TxtUsuario.Text;
            string contrasena = TxtContrasena.Password;

            if (string.IsNullOrWhiteSpace(usuario))
            {
                LblError.Text = "Debe ingresar el usuario.";
                return;
            }

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                LblError.Text = "Debe ingresar la contraseña.";
                return;
            }

            if (usuario == "admin" && contrasena == "1234")
            {
                MessageBox.Show("Inicio de sesión exitoso", "Acceso permitido");

                // Después aquí hacemos que navegue al Dashboard
            }
            else
            {
                LblError.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}