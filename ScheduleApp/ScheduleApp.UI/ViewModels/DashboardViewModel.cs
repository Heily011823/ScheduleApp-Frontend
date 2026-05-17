using ScheduleApp.UI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace ScheduleApp.UI.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly UserApiService _userApiService = new();

        private string _welcomeMessage = "¡Bienvenido!";
        private string _userRole = "Administrador";
        private string _gestionMessage = "Gestiona los usuarios y consulta la documentación del sistema.";
        private int _materiasTotal;
        private int _docentesTotal;
        private int _horariosTotal;
        private int _programasTotal;
        private int _aulasTotal;
        private int _coordinadoresTotal;

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set { _welcomeMessage = value; OnPropertyChanged(); }
        }

        public string UserRole
        {
            get => _userRole;
            set { _userRole = value; OnPropertyChanged(); }
        }

        public string GestionMessage
        {
            get => _gestionMessage;
            set { _gestionMessage = value; OnPropertyChanged(); }
        }

        public int MateriasTotal
        {
            get => _materiasTotal;
            set { _materiasTotal = value; OnPropertyChanged(); }
        }

        public int DocentesTotal
        {
            get => _docentesTotal;
            set { _docentesTotal = value; OnPropertyChanged(); }
        }

        public int HorariosTotal
        {
            get => _horariosTotal;
            set { _horariosTotal = value; OnPropertyChanged(); }
        }

        public int ProgramasTotal
        {
            get => _programasTotal;
            set { _programasTotal = value; OnPropertyChanged(); }
        }

        public int AulasTotal
        {
            get => _aulasTotal;
            set { _aulasTotal = value; OnPropertyChanged(); }
        }

        public int CoordinadoresTotal
        {
            get => _coordinadoresTotal;
            set { _coordinadoresTotal = value; OnPropertyChanged(); }
        }

        public DashboardViewModel()
        {
            _ = LoadDashboardAsync();
        }

        private async Task LoadDashboardAsync()
        {
            try
            {
               
                if (UserRole == "Administrador")
                {
                    WelcomeMessage = "¡Bienvenido, Administrador!";
                    GestionMessage = "Gestiona los usuarios y consulta la documentación del sistema.";
                }
                else if (UserRole == "Coordinador")
                {
                    WelcomeMessage = "¡Bienvenido, Coordinador!";
                    GestionMessage = "Gestiona los horarios de manera fácil y eficiente.";
                }

               
                var users = await _userApiService.GetUsersAsync();
                CoordinadoresTotal = users.FindAll(u => u.RoleName == "Coordinador").Count;

                
                MateriasTotal = 0;
                DocentesTotal = 0;
                HorariosTotal = 0;
                ProgramasTotal = 0;
                AulasTotal = 0;
            }
            catch (Exception ex)
            {
              
                MessageBox.Show($"Error cargando dashboard: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}