using ScheduleApp.UI.Services;
using ScheduleApp.UI.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScheduleApp.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowDashboardCommand { get; set; }
        public ICommand ShowMateriasCommand { get; set; }

        public MainViewModel()
        {
            ShowDashboardCommand =
                new RelayCommand(o =>
                {
                    CurrentView = new DashboardView();
                });

            ShowMateriasCommand =
                new RelayCommand(o =>
                {
                    CurrentView = new MateriasView();
                });

            CurrentView = new DashboardView();
        }
    }
}
