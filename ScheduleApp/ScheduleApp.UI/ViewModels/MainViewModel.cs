using ScheduleApp.UI.Models;
using ScheduleApp.UI.Services;
using ScheduleApp.UI.Views;

using System.Windows;


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

        // COMANDOS
        public ICommand ShowDashboardCommand { get; set; }
        public ICommand ShowMateriasCommand { get; set; }
        public ICommand ShowProgramasCommand { get; set; }
        public ICommand ShowDocentesCommand { get; set; }
        public ICommand ShowAulasCommand { get; set; }
        public ICommand ShowHorariosCommand { get; set; }
        public ICommand ShowRestriccionesCommand { get; set; }
        public ICommand ShowUsuariosCommand { get; set; }
        public ICommand ShowInformacionCommand { get; set; }
        public ICommand ShowManualCommand { get; set; }

        public ICommand ShowUserFormCommand { get; set; }
        public ICommand ShowLogoutCommand { get; set; }
        public ICommand CancelLogoutCommand { get; set; }
        public ICommand ConfirmLogoutCommand { get; set; }
        public ICommand ShowEditUserFormCommand { get; set; }

        public MainViewModel(string rolUsuario)
        {
            RolUsuario = rolUsuario;

            // INICIO
            ShowDashboardCommand = new RelayCommand(o =>
            {
                CurrentView = new DashboardView();
                ModuloActivo = "Inicio";
            });

            //EDITAR USUARIO
            ShowEditUserFormCommand = new RelayCommand(o =>
            {
                if (o is not UserModel selectedUser)
                {
                    MessageBox.Show("Debe seleccionar un usuario para editar.");
                    return;
                }

                var userFormViewModel = new UserFormViewModel(selectedUser);

                userFormViewModel.OnCancel += () =>
                {
                    CurrentView = new UsuariosView();
                    ModuloActivo = "Usuarios";
                };

                userFormViewModel.OnSaveSuccess += () =>
                {
                    CurrentView = new UsuariosView();
                    ModuloActivo = "Usuarios";
                };

                var userFormView = new UserFormView();
                userFormView.DataContext = userFormViewModel;

                CurrentView = userFormView;
                ModuloActivo = "Usuarios";
            });

            //AGREGAR USUARIO
            ShowUserFormCommand = new RelayCommand(o =>
             {
                 var userFormViewModel = new UserFormViewModel();

                 userFormViewModel.OnCancel += () =>
                 {
                     CurrentView = new UsuariosView();
                     ModuloActivo = "Usuarios";
                 };

                 userFormViewModel.OnSaveSuccess += () =>
                 {
                     CurrentView = new UsuariosView();
                     ModuloActivo = "Usuarios";
                 };

                 var userFormView = new UserFormView();
                 userFormView.DataContext = userFormViewModel;

                 CurrentView = userFormView;
                 ModuloActivo = "Usuarios";
             });

            // MATERIAS
            ShowMateriasCommand = new RelayCommand(o =>
            {
                CurrentView = new MateriasView();
                ModuloActivo = "Materias";
            });

            // PROGRAMAS
            ShowProgramasCommand = new RelayCommand(o =>
            {
                CurrentView = new ProgramasView();
                ModuloActivo = "Programas";
            });

            // DOCENTES
            ShowDocentesCommand = new RelayCommand(o =>
            {
                CurrentView = new DocentesView();
                ModuloActivo = "Docentes";
            });

            // AULAS
            ShowAulasCommand = new RelayCommand(o =>
            {
                CurrentView = new AulasView();
                ModuloActivo = "Aulas";
            });

            // HORARIOS
            ShowHorariosCommand = new RelayCommand(o =>
            {
                CurrentView = new HorariosView();
                ModuloActivo = "Horarios";
            });

            // RESTRICCIONES
            ShowRestriccionesCommand = new RelayCommand(o =>
            {
                CurrentView = new RestriccionesView();
                ModuloActivo = "Restricciones";
            });

            // USUARIOS
            ShowUsuariosCommand = new RelayCommand(o =>
            {
                CurrentView = new UsuariosView();
                ModuloActivo = "Usuarios";
            });

            // INFORMACIÓN
            ShowInformacionCommand = new RelayCommand(o =>
            {
                CurrentView = new InformacionView();
                ModuloActivo = "Información";
            });

            // MANUAL
            ShowManualCommand = new RelayCommand(o =>
            {
                CurrentView = new InformacionView();
                ModuloActivo = "Manual";
            });
            // LOGOUT
            ShowLogoutCommand = new RelayCommand(o =>
            {
                CurrentView = new LogoutView();
                ModuloActivo = "Logout";
            });

            CancelLogoutCommand = new RelayCommand(o =>
            {
                CurrentView = new DashboardView();
                ModuloActivo = "Inicio";
            });

            ConfirmLogoutCommand = new RelayCommand(o =>
            {
                var loginViewModel = new LoginViewModel();
                var loginWindow = new LoginWindow();
                loginWindow.DataContext = loginViewModel;

                loginViewModel.OnLoginSuccess += () =>
                {
                    string rolUsuario = loginViewModel.RolUsuario;

                    MainWindow mainWindow = new MainWindow(rolUsuario);
                    mainWindow.Show();

                    loginWindow.Close();
                };

                loginWindow.Show();

                foreach (Window w in Application.Current.Windows)
                {
                    if (w is MainWindow)
                    {
                        w.Close();
                        break;
                    }
                }
            });

            // VISTA INICIAL
            CurrentView = new DashboardView();
            ModuloActivo = "Inicio";

            

        }
    }
}