using ScheduleApp.UI.Models;
using ScheduleApp.UI.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduleApp.UI.ViewModels
{
    public class DeleteUserViewModel : INotifyPropertyChanged
    {
        private readonly UserApiService _userApiService;
        private readonly UserModel _userToDelete;

        private string _confirmationText = string.Empty;
        private string _errorMessage = string.Empty;

        public event Action? OnCancel;
        public event Action? OnDeleteSuccess;

        public string ConfirmationText
        {
            get => _confirmationText;
            set
            {
                _confirmationText = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand CancelCommand { get; }
        public ICommand ConfirmDeleteCommand { get; }

        public DeleteUserViewModel(UserModel userToDelete)
        {
            _userApiService = new UserApiService();
            _userToDelete = userToDelete;

            // El de cancelar usa el RelayCommand tradicional normal de tu proyecto
            CancelCommand = new RelayCommand(ExecuteCancel);

            // CORREGIDO: Usamos el comando asíncrono privado sin enviarle parámetros innecesarios
            ConfirmDeleteCommand = new DeleteAsyncCommand(async () => await ExecuteDeleteAsync());
        }

        private void ExecuteCancel(object? parameter)
        {
            OnCancel?.Invoke();
        }

        private async Task ExecuteDeleteAsync()
        {
            ErrorMessage = string.Empty;

            if (ConfirmationText.Trim() != "Borrar")
            {
                ErrorMessage = "Debe escribir la palabra Borrar para confirmar.";
                return;
            }

            bool deleted = await _userApiService.DeleteUserAsync(_userToDelete.Id);

            if (!deleted)
            {
                ErrorMessage = "No se pudo eliminar el usuario.";
                return;
            }

            MessageBox.Show(
                "Usuario eliminado correctamente.",
                "Éxito",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            OnDeleteSuccess?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // =========================================================================
        // CLASE INTERNA PRIVADA: Comando asíncrono aislado para la eliminación
        // =========================================================================
        private class DeleteAsyncCommand : ICommand
        {
            private readonly Func<Task> _execute;
            private readonly Func<bool>? _canExecute;

            public DeleteAsyncCommand(Func<Task> execute, Func<bool>? canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();
            public async void Execute(object? parameter) => await _execute();

            public event EventHandler? CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}