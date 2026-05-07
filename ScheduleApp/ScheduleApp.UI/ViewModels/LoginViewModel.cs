using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ScheduleApp.UI.Services; // Apunta a la carpeta donde tienes tus servicios

namespace ScheduleApp.UI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;

        private string _usuario = string.Empty;
        private string _contrasena = string.Empty;
        private string _mensajeError = string.Empty;

        public event Action? OnLoginSuccess;

        public LoginViewModel()
        {
            _authenticationService = new FakeAuthenticationService();

            // SOLUCIÓN AL ERROR CS1729: Le pasamos solo 1 argumento como pide tu RelayCommand
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        public string Usuario
        {
            get => _usuario;
            set { _usuario = value; OnPropertyChanged(); }
        }

        public string Contrasena
        {
            get => _contrasena;
            set { _contrasena = value; OnPropertyChanged(); }
        }

        public string MensajeError
        {
            get => _mensajeError;
            set { _mensajeError = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        private void ExecuteLogin(object? parameter)
        {
            MensajeError = string.Empty;

            // Criterios de aceptación: Validar campos vacíos
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

            // Validamos mediante nuestro servicio backend desacoplado
            if (_authenticationService.Login(Usuario, Contrasena))
            {
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