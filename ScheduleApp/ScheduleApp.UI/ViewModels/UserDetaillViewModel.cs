using ScheduleApp.UI.Models;
using ScheduleApp.UI.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ScheduleApp.UI.ViewModels
{
    public class UserDetailViewModel : INotifyPropertyChanged
    {
        public event Action? OnBack;

        public string Username { get; }
        public string FullName { get; }
        public string Email { get; }
        public string IdentityDocument { get; }
        public string RoleName { get; }
        public string Status { get; }
        public string PasswordDisplay { get; } = "********";

        public ICommand BackCommand { get; }

        public UserDetailViewModel(UserModel user)
        {
            Username = user.Username;
            FullName = user.FullName;
            Email = user.Email;
            IdentityDocument = user.IdentityDocument;
            RoleName = user.RoleName;
            Status = user.Status;

            // CORREGIDO: Usamos la clase interna privada y exclusiva para este ViewModel
            BackCommand = new DetalleRelayCommand(o =>
            {
                OnBack?.Invoke();
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // =========================================================================
        // CLASE INTERNA PRIVADA: Comando exclusivo de Detalles para evitar colisiones
        // =========================================================================
        private class DetalleRelayCommand : ICommand
        {
            private readonly Action<object?> _execute;
            private readonly Predicate<object?>? _canExecute;

            public DetalleRelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
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