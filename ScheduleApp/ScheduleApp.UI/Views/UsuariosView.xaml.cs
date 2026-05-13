using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduleApp.UI.Views
{
    /// <summary>
    /// Lógica de interacción para UsuariosView.xaml
    /// </summary>
    public partial class UsuariosView : UserControl
    {
        public UsuariosView()
        {
            InitializeComponent();

            //Users = new ObservableCollection<User>();

            //UsersDataGrid.ItemsSource = Users;
        }

        //private void BtnNuevoUsuario_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
            //NewUserView ventana = new NewUserView(Users);

            //ventana.ShowDialog();
        //}
    }
}
