using System.Windows.Controls;
using ScheduleApp.UI.ViewModels; 

namespace ScheduleApp.UI.Views
{
    /// <summary>
    /// Lógica de interacción para MateriasView.xaml
    /// </summary>
    public partial class MateriasView : UserControl
    {
        public MateriasView()
        {
            InitializeComponent();

           
            DataContext = new MateriasViewModel();
        }
    }
}