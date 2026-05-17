using System.Windows.Controls;
using ScheduleApp.UI.ViewModels;

namespace ScheduleApp.UI.Views
{
    public partial class UsuariosView : UserControl
    {
        public UsuariosView()
        {
            InitializeComponent();
            DataContext = new UsuariosViewModel();
        }
    }
}