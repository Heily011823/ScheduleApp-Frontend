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

     
        private void BtnOpciones_Click(object sender, RoutedEventArgs e)
        {
            if (BtnOpciones.ContextMenu != null)
            {
                BtnOpciones.ContextMenu.PlacementTarget = BtnOpciones;
                BtnOpciones.ContextMenu.IsOpen = true;
            }
        }
    }
}