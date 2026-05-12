using ScheduleApp.UI.Models;
using ScheduleApp.UI.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduleApp.UI.ViewModels
{
    public class UserFormViewModel : INotifyPropertyChanged
    {
        private readonly UserApiService _userApiService;

        private string _fullName = string.Empty;
        private string _email = string.Empty;
        private string _username = string.Empty;
        private string _identityDocument = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private string _selectedRole = string.Empty;
        private string _selectedStatus = string.Empty;
        private string _errorMessage = string.Empty;

        public event Action? OnCancel;
        public event Action? OnSaveSuccess;

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string IdentityDocument
        {
            get => _identityDocument;
            set { _identityDocument = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public string SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; OnPropertyChanged(); }
        }

        public string SelectedStatus
        {
            get => _selectedStatus;
            set { _selectedStatus = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public UserFormViewModel()
        {
            _userApiService = new UserApiService();

            SaveCommand = new RelayCommand(async o => await ExecuteSaveAsync());
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private async Task ExecuteSaveAsync()
        {
            ErrorMessage = string.Empty;

            // NOMBRE
            if (string.IsNullOrWhiteSpace(FullName))
            {
                ErrorMessage = "El nombre completo es obligatorio.";
                return;
            }

            if (FullName.Trim().Length < 5)
            {
                ErrorMessage = "El nombre debe tener mínimo 5 caracteres.";
                return;
            }

            if (!Regex.IsMatch(FullName, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                ErrorMessage = "El nombre solo debe contener letras.";
                return;
            }

            // EMAIL
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "El correo es obligatorio.";
                return;
            }

            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ErrorMessage = "Correo inválido.";
                return;
            }

            if (!Email.EndsWith("@autonoma.edu.co", StringComparison.OrdinalIgnoreCase))
            {
                ErrorMessage = "Debe usar correo institucional.";
                return;
            }

            // USERNAME
            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "El usuario es obligatorio.";
                return;
            }

            if (Username.Trim().Length < 4)
            {
                ErrorMessage = "El usuario debe tener mínimo 4 caracteres.";
                return;
            }

            if (!Regex.IsMatch(Username, @"^[a-zA-Z0-9._]+$"))
            {
                ErrorMessage = "El usuario solo puede contener letras, números, punto o guion bajo.";
                return;
            }

            // DOCUMENTO
            if (string.IsNullOrWhiteSpace(IdentityDocument))
            {
                ErrorMessage = "El documento es obligatorio.";
                return;
            }

            if (!Regex.IsMatch(IdentityDocument, @"^[0-9]+$"))
            {
                ErrorMessage = "El documento solo debe contener números.";
                return;
            }

            if (IdentityDocument.Length < 6 || IdentityDocument.Length > 15)
            {
                ErrorMessage = "El documento debe tener entre 6 y 15 dígitos.";
                return;
            }

            // PASSWORD
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "La contraseña es obligatoria.";
                return;
            }

            if (Password.Length < 8)
            {
                ErrorMessage = "La contraseña debe tener mínimo 8 caracteres.";
                return;
            }

            if (!Regex.IsMatch(Password, @"[A-Z]"))
            {
                ErrorMessage = "La contraseña debe tener una mayúscula.";
                return;
            }

            if (!Regex.IsMatch(Password, @"[a-z]"))
            {
                ErrorMessage = "La contraseña debe tener una minúscula.";
                return;
            }

            if (!Regex.IsMatch(Password, @"[0-9]"))
            {
                ErrorMessage = "La contraseña debe tener un número.";
                return;
            }

            // CONFIRMAR PASSWORD
            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "Debe confirmar la contraseña.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Las contraseñas no coinciden.";
                return;
            }

            // ROL
            if (string.IsNullOrWhiteSpace(SelectedRole))
            {
                ErrorMessage = "Debe seleccionar un rol.";
                return;
            }

            // ESTADO
            if (string.IsNullOrWhiteSpace(SelectedStatus))
            {
                ErrorMessage = "Debe seleccionar un estado.";
                return;
            }
            var user = new UserModel
            {
                FullName = FullName,
                Email = Email,
                Username = Username,
                IdentityDocument = IdentityDocument,
                Password = Password,
                ConfirmPassword = ConfirmPassword,

                RoleName = SelectedRole,

                IsActive = SelectedStatus == "Activo"
            };

            bool created = await _userApiService.CreateUserAsync(user);

            if (!created)
            {
                ErrorMessage = "No se pudo guardar el usuario. Verifica que el correo, usuario o documento no existan.";
                return;
            }

            MessageBox.Show(
                "Usuario guardado correctamente.",
                "Éxito",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            OnSaveSuccess?.Invoke();
        }

        private void ExecuteCancel(object? parameter)
        {
            OnCancel?.Invoke();
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