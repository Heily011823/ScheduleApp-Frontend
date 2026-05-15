using ScheduleApp.UI.ViewModels;
using System.Windows.Controls;

namespace ScheduleApp.UI.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel();
        }
    }
}