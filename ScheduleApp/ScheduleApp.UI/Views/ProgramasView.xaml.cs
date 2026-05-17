using System.Windows;
using System.Windows.Controls;

namespace ScheduleApp.UI.Views
{
    /// <summary>
    /// Lógica de interacción para ProgramasView.xaml
    /// </summary>
    public partial class ProgramasView : UserControl
    {
        public ProgramasView()
        {
            InitializeComponent();
        }

        private void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
           
            if (BtnExportar.ContextMenu != null)
            {
                BtnExportar.ContextMenu.PlacementTarget = BtnExportar;
                BtnExportar.ContextMenu.IsOpen = true;
            }
        }
    }
}