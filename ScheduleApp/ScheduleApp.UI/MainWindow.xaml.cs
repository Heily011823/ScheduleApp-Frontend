using ScheduleApp.UI.ViewModels;
using System.Windows;

namespace ScheduleApp.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow(string rolUsuario)
        {
            InitializeComponent();

            DataContext = new MainViewModel(rolUsuario);
        }
    }
}