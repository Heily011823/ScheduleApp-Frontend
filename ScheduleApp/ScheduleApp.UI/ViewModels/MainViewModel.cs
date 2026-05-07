using ScheduleApp.UI.Services;
using ScheduleApp.UI.Views;
using System.Windows.Input;

namespace ScheduleApp.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;
        private string _rolUsuario;
        private bool _esAdministrador;
        private string _moduloActivo;
        private string _imagenRol;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string RolUsuario
        {
            get => _rolUsuario;
            set
            {
                _rolUsuario = value;
                OnPropertyChanged();

                EsAdministrador = _rolUsuario == "Administrador";

                ImagenRol = EsAdministrador
                    ? "pack://application:,,,/Assets/Administrador.png"
                    : "pack://application:,,,/Assets/Coordinador.png";
            }
        }

        public bool EsAdministrador
        {
            get => _esAdministrador;
            set
            {
                _esAdministrador = value;
                OnPropertyChanged();
            }
        }

        public string ModuloActivo
        {
            get => _moduloActivo;
            set
            {
                _moduloActivo = value;
                OnPropertyChanged();
            }
        }

        public string ImagenRol
        {
            get => _imagenRol;
            set
            {
                _imagenRol = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowDashboardCommand { get; set; }
        public ICommand ShowMateriasCommand { get; set; }

        public MainViewModel(string rolUsuario)
        {
            RolUsuario = rolUsuario;

            ShowDashboardCommand =
                new RelayCommand(o =>
                {
                    CurrentView = new DashboardView();
                    ModuloActivo = "Inicio";
                });

            ShowMateriasCommand =
                new RelayCommand(o =>
                {
                    CurrentView = new MateriasView();
                    ModuloActivo = "Materias";
                });

            CurrentView = new DashboardView();
            ModuloActivo = "Inicio";
        }
    }
}