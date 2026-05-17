using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ScheduleApp.UI.Models;
using ScheduleApp.UI.Services;

namespace ScheduleApp.UI.ViewModels
{
    public class DocentesViewModel : BaseViewModel
    {
        private readonly TeacherApiService _teacherApiService = new();
        private readonly List<TeacherModel> _todosLosDocentes = new();

        private int _currentPage = 1;
        private int _totalPages = 1;
        private string _searchText = string.Empty;
        private string _selectedStatus = "Estado";

        public ObservableCollection<TeacherModel> DocentesPaginados { get; set; } = new();
        public int PageSize { get; set; } = 4;

        public int CurrentPage
        {
            get => _currentPage;
            set { _currentPage = value; OnPropertyChanged(); }
        }

        public int TotalPages
        {
            get => _totalPages;
            set { _totalPages = value; OnPropertyChanged(); }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterAndPage();
            }
        }

        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                OnPropertyChanged();
                FilterAndPage();
            }
        }

        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        public DocentesViewModel()
        {
            NextPageCommand = new DocentesRelayCommand(o => GoToNextPage());
            PreviousPageCommand = new DocentesRelayCommand(o => GoToPreviousPage());
        }

        public async Task InicializarAsync()
        {
            // Puedes comentar o remover este MessageBox en producción
            MessageBox.Show($"Token: '{SessionService.Token}'");
            await CargarDocentesAsync();
        }

        private async Task CargarDocentesAsync()
        {
            try
            {
                var docentes = await _teacherApiService.GetTeachersAsync();
                _todosLosDocentes.Clear();
                _todosLosDocentes.AddRange(docentes);
                CurrentPage = 1;
                FilterAndPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando docentes: {ex.Message}");
            }
        }

        private void FilterAndPage()
        {
            var filtrados = _todosLosDocentes.AsEnumerable();

            // 1. Filtro de búsqueda (Buscador) protegido contra propiedades nulas en el modelo
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtrados = filtrados.Where(d =>
                    (!string.IsNullOrEmpty(d.FullName) && d.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(d.Email) && d.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(d.IdentityDocument) && d.IdentityDocument.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            }

            // 2. Filtro de Estado ("Activo" / "Inactivo") coordinado con el ComboBox
            if (!string.IsNullOrWhiteSpace(SelectedStatus) && SelectedStatus != "Estado")
            {
                filtrados = filtrados.Where(d =>
                    !string.IsNullOrEmpty(d.Status) && d.Status.Equals(SelectedStatus, StringComparison.OrdinalIgnoreCase));
            }

            var lista = filtrados.ToList();
            TotalPages = Math.Max(1, (int)Math.Ceiling((double)lista.Count / PageSize));

            if (CurrentPage > TotalPages) CurrentPage = TotalPages;

            DocentesPaginados.Clear();
            foreach (var d in lista.Skip((CurrentPage - 1) * PageSize).Take(PageSize))
            {
                DocentesPaginados.Add(d);
            }

            OnPropertyChanged(nameof(DocentesPaginados));
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(TotalPages));
        }

        private void GoToNextPage()
        {
            if (CurrentPage >= TotalPages) return;
            CurrentPage++;
            FilterAndPage();
        }

        private void GoToPreviousPage()
        {
            if (CurrentPage <= 1) return;
            CurrentPage--;
            FilterAndPage();
        }

        // =========================================================================
        // CLASE INTERNA PRIVADA: Comando exclusivo para evitar colisiones con el Main
        // =========================================================================
        private class DocentesRelayCommand : ICommand
        {
            private readonly Action<object?> _execute;
            private readonly Predicate<object?>? _canExecute;

            public DocentesRelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
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