using ScheduleApp.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input; // Requerido para enlazar los comandos ICommand

namespace ScheduleApp.UI.ViewModels
{
    public class MateriasViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private bool _isLoading;
        private int _currentPage = 1;
        private int _pageSize = 6; // Puedes cambiar este número para definir cuántas filas quieres ver por página
        private int _totalPages;

        // Colección observable que el DataGrid estará escuchando
        public ObservableCollection<SubjectModel> Subjects { get; set; }

        // Propiedad para controlar estados de carga o spinners
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        // Propiedades de paginación vinculadas dinámicamente al XAML
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        // Comandos que escucharán los clics de las flechas ≪ y ≫
        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }

        public MateriasViewModel()
        {
            Subjects = new ObservableCollection<SubjectModel>();

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7216/")
            };

            // CORREGIDO: Ahora instanciamos el comando privado y exclusivo de esta clase
            NextPageCommand = new MateriaPaginacionCommand(async () => await ExecuteNextPage(), () => CurrentPage < _totalPages);
            PreviousPageCommand = new MateriaPaginacionCommand(async () => await ExecutePreviousPage(), () => CurrentPage > 1);

            // Ejecutamos la carga inicial de manera segura
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            IsLoading = true;
            await LoadSubjects();
            IsLoading = false;
        }

        private async Task LoadSubjects()
        {
            try
            {
                // Construimos la URL enviando las variables Query String exactas que espera el controlador
                string url = $"api/subjects/search?page={CurrentPage}&pageSize={_pageSize}";

                // Deserializamos usando tu PagedResultModel (que procesa la envoltura JSON del backend)
                var pagedResult = await _httpClient.GetFromJsonAsync<PagedResultModel<SubjectModel>>(url);

                if (pagedResult != null && pagedResult.Items != null)
                {
                    _totalPages = pagedResult.TotalPages;
                    CurrentPage = pagedResult.Page; // Sincronizamos la página devuelta por el servidor

                    // Volvemos al hilo principal de la UI para modificar la ObservableCollection de forma segura
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Subjects.Clear();
                        foreach (var subject in pagedResult.Items)
                        {
                            Subjects.Add(subject);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"=== ERROR CRÍTICO EN MATERIAS ===");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                System.Diagnostics.Debug.WriteLine($"=================================");
            }
        }

        // Métodos de ejecución de los comandos de paginación
        private async Task ExecuteNextPage()
        {
            CurrentPage++;
            await LoadSubjects();
        }

        private async Task ExecutePreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadSubjects();
            }
        }

        #region Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        // =========================================================================
        // CLASE INTERNA PRIVADA: Blindada para que no rompa el MainViewModel
        // =========================================================================
        private class MateriaPaginacionCommand : ICommand
        {
            private readonly Func<Task> _execute;
            private readonly Func<bool> _canExecute;

            public MateriaPaginacionCommand(Func<Task> execute, Func<bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
            public async void Execute(object parameter) => await _execute();

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    } // Fin de la clase MateriasViewModel
} // Fin del namespace