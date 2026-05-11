using System;
using System.Collections.Generic;
using System.Text;



namespace ScheduleApp.UI.Models;

public class UserModel
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string IdentityDocument { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public string Status => IsActive ? "Activo" : "Inactivo";
}