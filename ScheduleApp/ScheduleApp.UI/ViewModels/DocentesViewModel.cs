using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ScheduleApp.UI.Models;
using ScheduleApp.UI.Services;

namespace ScheduleApp.UI.ViewModels;

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
        NextPageCommand = new RelayCommand(_ => GoToNextPage());
        PreviousPageCommand = new RelayCommand(_ => GoToPreviousPage());
        // ← Ya no carga aquí
    }

    // ← Llamado desde DocentesView.xaml.cs cuando la vista ya está cargada
    public async Task InicializarAsync()
    {
     
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

        if (!string.IsNullOrWhiteSpace(SearchText))
            filtrados = filtrados.Where(d =>
                d.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                d.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(SelectedStatus) && SelectedStatus != "Estado")
            filtrados = filtrados.Where(d =>
                d.Status.Equals(SelectedStatus, StringComparison.OrdinalIgnoreCase));

        var lista = filtrados.ToList();
        TotalPages = Math.Max(1, (int)Math.Ceiling((double)lista.Count / PageSize));

        if (CurrentPage > TotalPages) CurrentPage = TotalPages;

        DocentesPaginados.Clear();
        foreach (var d in lista.Skip((CurrentPage - 1) * PageSize).Take(PageSize))
            DocentesPaginados.Add(d);

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
}