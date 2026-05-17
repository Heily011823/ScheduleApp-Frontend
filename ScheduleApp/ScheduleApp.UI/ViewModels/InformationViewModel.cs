using System;

using System.Collections.ObjectModel;

namespace ScheduleApp.UI.ViewModels
{
  
    public class InformationViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string AcercaDelSistemaTitle { get; set; }
        public string AcercaDelSistemaDescripcion { get; set; }
        public string DesarrolladoresTitle { get; set; }
        public string VersionTitle { get; set; }
        public string VersionNumero { get; set; }

        public ObservableCollection<string> DesarrolladoresColumna1 { get; set; }
        public ObservableCollection<string> DesarrolladoresColumna2 { get; set; }

        public InformationViewModel()
        {
            Title = "Información";
            AcercaDelSistemaTitle = "Acerca del sistema";
            AcercaDelSistemaDescripcion = "ScheduleApp es un sistema de gestión de horarios académicos que permite administrar materias, docentes, aulas y generar horarios automáticamente de manera eficiente, optimizando la organización académica.";

            DesarrolladoresTitle = "Desarrolladores";

            VersionTitle = "Versión";
            VersionNumero = "Versión 1.0";

            DesarrolladoresColumna1 = new ObservableCollection<string>
            {
                "Heily Yohana Rios Ayala",
                "Elizabeth Meneses Muñoz",
                "Maria Paz Puerta Acevedo",
                "Mateo Quintero Morales",
                "Salomé Carmona Gaviria"
            };

            DesarrolladoresColumna2 = new ObservableCollection<string>
            {
                "Juan Jacobo cañas Henao",
                "Juan Esteban Hernandez",
                "Juan José Morales Aristizabal"
            };
        }
    }
}