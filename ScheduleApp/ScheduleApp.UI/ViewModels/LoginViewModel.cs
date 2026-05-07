using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ScheduleApp.UI.Services;

namespace ScheduleApp.UI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;

        private string _usuario = string.Empty;
        private string _contrasena = string.Empty;
        private string _mensajeError = string.Empty;
        private string _rolUsuario = string.Empty;

        public event Action? OnLoginSuccess;

        public LoginViewModel()
        {
            _authenticationService = new FakeAuthenticationService();
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        public string Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                OnPropertyChanged();
            }
        }

        public string Contrasena
        {
            get => _contrasena;
            set
            {
                _contrasena = value;
                OnPropertyChanged();
            }
        }

        public string MensajeError
        {
            get => _mensajeError;
            set
            {
                _mensajeError = value;
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
            }
        }

        public ICommand LoginCommand { get; }

        private void ExecuteLogin(object? parameter)
        {
            MensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(Usuario) || Usuario == "Ingresa tu usuario o correo")
            {
                MensajeError = "Debe ingresar el usuario.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Contrasena))
            {
                MensajeError = "Debe ingresar la contraseña.";
                return;
            }

            if (_authenticationService.Login(Usuario, Contrasena))
            {
                if (Usuario.ToLower() == "admin" || Usuario.ToLower() == "administrador")
                {
                    RolUsuario = "Administrador";
                }
                else
                {
                    RolUsuario = "Coordinador";
                }

                OnLoginSuccess?.Invoke();
            }
            else
            {
                MensajeError = "Usuario o contraseña incorrectos.";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? nombre = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
    }
}