using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ScheduleApp.UI.Models;
using ScheduleApp.UI.Services;


namespace ScheduleApp.UI.ViewModels;

public class UsuariosViewModel : BaseViewModel
{
    private readonly UserApiService _userApiService = new();
    

    public ObservableCollection<UserModel> Usuarios { get; set; } = new();

    private UserModel _usuario = new()
    {
        IsActive = true
    };

    public UserModel Usuario
    {
        get => _usuario;
        set
        {
            _usuario = value;
            OnPropertyChanged();
        }
    }

    public ICommand CargarUsuariosCommand { get; }
    public ICommand GuardarUsuarioCommand { get; }

    public UsuariosViewModel()
    {
        CargarUsuariosCommand = new RelayCommand(async _ => await CargarUsuariosAsync());
        GuardarUsuarioCommand = new RelayCommand(async _ => await GuardarUsuarioAsync());

        _ = CargarUsuariosAsync();
    }

    private async Task CargarUsuariosAsync()
    {
        try
        {
            Usuarios.Clear();

            var usuariosApi = await _userApiService.GetUsersAsync();

            foreach (var usuario in usuariosApi)
            {
                Usuarios.Add(usuario);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error cargando usuarios: {ex.Message}");
        }
    }

    private async Task GuardarUsuarioAsync()
    {
        try
        {
            var creado = await _userApiService.CreateUserAsync(Usuario);

            if (creado)
            {
                MessageBox.Show("Usuario creado correctamente.");

                Usuario = new UserModel
                {
                    IsActive = true
                };

                await CargarUsuariosAsync();
            }
            else
            {
                MessageBox.Show("No se pudo crear el usuario.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error guardando usuario: {ex.Message}");
        }
    }
}