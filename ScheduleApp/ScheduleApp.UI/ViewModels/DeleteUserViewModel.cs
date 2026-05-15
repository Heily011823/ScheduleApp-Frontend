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

            CancelCommand = new RelayCommand(ExecuteCancel);
            ConfirmDeleteCommand = new RelayCommand(async o => await ExecuteDeleteAsync());
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

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}