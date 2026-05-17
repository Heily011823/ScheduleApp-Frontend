using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ScheduleApp.UI.Services;

namespace ScheduleApp.UI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthApiService _authenticationService;

        private string _usuario = string.Empty;
        private string _contrasena = string.Empty;
        private string _mensajeError = string.Empty;
        private string _rolUsuario = string.Empty;

        public event Action? OnLoginSuccess;

        public LoginViewModel()
        {
            _authenticationService = new AuthApiService();

            // CORREGIDO: Ahora usa la clase interna privada y aislada de Login
            LoginCommand = new LoginRelayCommand(ExecuteLogin);
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

        public string RolUsuario
        {
            get => _rolUsuario;
            set { _rolUsuario = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        private async void ExecuteLogin(object? parameter)
        {
            MensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(Usuario) ||
                Usuario == "Ingresa tu usuario o correo")
            {
                MensajeError = "Debe ingresar el usuario.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Contrasena))
            {
                MensajeError = "Debe ingresar la contraseña.";
                return;
            }

            var loginSuccess = await _authenticationService.LoginAsync(
                Usuario.Trim(),
                Contrasena.Trim());

            if (loginSuccess)
            {
                SessionService.Token = _authenticationService.Token;
                SessionService.Role = _authenticationService.Role;
                RolUsuario = _authenticationService.Role;
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

        // =========================================================================
        // CLASE INTERNA PRIVADA: Comando exclusivo del Login para evitar colisiones
        // =========================================================================
        private class LoginRelayCommand : ICommand
        {
            private readonly Action<object?> _execute;
            private readonly Predicate<object?>? _canExecute;

            public LoginRelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);
            public void Execute(object? parameter) => _execute(parameter);

            public event EventHandler? CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}