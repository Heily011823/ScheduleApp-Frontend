using Microsoft.Win32;
using ScheduleApp.UI.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduleApp.UI.ViewModels;

public class ProgramasViewModel : BaseViewModel
{
    private readonly ProgramApiService _programApiService = new();
    private bool _isExporting = false;

    public bool IsExporting
    {
        get => _isExporting;
        set { _isExporting = value; OnPropertyChanged(); OnPropertyChanged(nameof(ExportButtonText)); }
    }

    public string ExportButtonText => IsExporting ? "Generando..." : "📄 PDF";

    public ICommand ExportPdfCommand { get; }

    public ProgramasViewModel()
    {
        // ← Solo 1 argumento, sin el canExecute
        ExportPdfCommand = new RelayCommand(async _ => await ExportPdfAsync());
    }

    private async Task ExportPdfAsync()
    {
        if (IsExporting) return; // ← bloqueo manual

        var dialog = new SaveFileDialog
        {
            FileName = "reporte_programas_academicos",
            DefaultExt = ".html",
            Filter = "Archivo HTML|*.html"
        };

        if (dialog.ShowDialog() != true) return;

        IsExporting = true;
        try
        {
            var success = await _programApiService.ExportPdfAsync(dialog.FileName);
            if (success)
                MessageBox.Show("Reporte exportado correctamente.", "Éxito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Error al exportar el reporte.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsExporting = false;
        }
    }
}