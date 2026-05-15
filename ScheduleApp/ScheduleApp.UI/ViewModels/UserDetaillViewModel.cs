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

            BackCommand = new RelayCommand(o =>
            {
                OnBack?.Invoke();
            });
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