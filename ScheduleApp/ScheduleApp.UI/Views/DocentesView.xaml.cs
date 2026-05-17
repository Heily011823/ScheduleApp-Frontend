using System.Windows.Controls;
using ScheduleApp.UI.ViewModels;

namespace ScheduleApp.UI.Views;

public partial class DocentesView : UserControl
{
    public DocentesView()
    {
        InitializeComponent();
        var vm = new DocentesViewModel();
        DataContext = vm;
        Loaded += async (s, e) => await vm.InicializarAsync();
    }
}