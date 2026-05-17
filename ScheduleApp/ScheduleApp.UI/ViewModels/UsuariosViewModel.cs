using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ScheduleApp.UI.Models;
using ScheduleApp.UI.Services;

namespace ScheduleApp.UI.ViewModels;

public class UsuariosViewModel : BaseViewModel
{
    private readonly UserApiService _userApiService = new();

    private readonly List<UserModel> _todosLosUsuarios = new();

    private int _currentPage = 1;
    private int _totalPages = 1;

    private string _searchText = string.Empty;
    private string _selectedStatus = string.Empty;
    private string _selectedRole = string.Empty;

    public ObservableCollection<UserModel> Usuarios { get; set; } = new();

    public ObservableCollection<UserModel> UsuariosPaginados { get; set; } = new();

    public int PageSize { get; set; } = 4;

    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            _currentPage = value;
            OnPropertyChanged();
        }
    }

    public int TotalPages
    {
        get => _totalPages;
        set
        {
            _totalPages = value;
            OnPropertyChanged();
        }
    }

    public ICommand CargarUsuariosCommand { get; }
    public ICommand NextPageCommand { get; }
    public ICommand PreviousPageCommand { get; }

    public UsuariosViewModel()
    {
        SelectedStatus = "Estado";
        SelectedRole = "Rol";

        CargarUsuariosCommand = new RelayCommand(async _ => await CargarUsuariosAsync());
        NextPageCommand = new RelayCommand(_ => GoToNextPage());
        PreviousPageCommand = new RelayCommand(_ => GoToPreviousPage());

        _ = CargarUsuariosAsync();
    }

    private async Task CargarUsuariosAsync()
    {
        try
        {
            Usuarios.Clear();
            UsuariosPaginados.Clear();
            _todosLosUsuarios.Clear();

            var usuariosApi = await _userApiService.GetUsersAsync();

            foreach (var usuario in usuariosApi)
            {
                Usuarios.Add(usuario);
                _todosLosUsuarios.Add(usuario);
            }

            CurrentPage = 1;
            TotalPages = Math.Max(
                1,
                (int)Math.Ceiling((double)_todosLosUsuarios.Count / PageSize));

            LoadCurrentPage();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error cargando usuarios: {ex.Message}");
        }
    }

    private void LoadCurrentPage()
    {
        UsuariosPaginados.Clear();

        var usuariosPagina = _todosLosUsuarios
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        foreach (var usuario in usuariosPagina)
        {
            UsuariosPaginados.Add(usuario);
        }

        OnPropertyChanged(nameof(Usuarios));
        OnPropertyChanged(nameof(UsuariosPaginados));
        OnPropertyChanged(nameof(CurrentPage));
        OnPropertyChanged(nameof(TotalPages));
    }

    private void GoToNextPage()
    {
        if (CurrentPage >= TotalPages)
            return;

        CurrentPage++;
        LoadCurrentPage();
    }

    private void GoToPreviousPage()
    {
        if (CurrentPage <= 1)
            return;

        CurrentPage--;
        LoadCurrentPage();
    }

    private void FilterUsers()
    {
        var filteredUsers = _todosLosUsuarios.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filteredUsers = filteredUsers.Where(u =>
                u.FullName.Contains(SearchText,
                    StringComparison.OrdinalIgnoreCase)
                ||
                u.Username.Contains(SearchText,
                    StringComparison.OrdinalIgnoreCase)
                ||
                u.Email.Contains(SearchText,
                    StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(SelectedStatus) && SelectedStatus != "Estado")
        {
            filteredUsers = filteredUsers.Where(u =>
                u.Status.Equals(SelectedStatus,
                    StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(SelectedRole) && SelectedRole != "Rol")
        {
            filteredUsers = filteredUsers.Where(u =>
                u.Role.Equals(SelectedRole,
                    StringComparison.OrdinalIgnoreCase));
        }

        UsuariosPaginados.Clear();

        foreach (var usuario in filteredUsers)
        {
            UsuariosPaginados.Add(usuario);
        }

        OnPropertyChanged(nameof(UsuariosPaginados));
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            FilterUsers();
        }
    }

    public string SelectedStatus
    {
        get => _selectedStatus;
        set
        {
            _selectedStatus = value;
            OnPropertyChanged();
            FilterUsers();
        }
    }

    public string SelectedRole
    {
        get => _selectedRole;
        set
        {
            _selectedRole = value;
            OnPropertyChanged();
            FilterUsers();
        }
    }
}