using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduleApp.UI.Views
{
    /// <summary>
    /// Lógica de interacción para TeacherFormView.xaml
    /// </summary>
    public partial class TeacherFormView : UserControl
    {
        public TeacherFormView()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Step1Grid.Visibility = Visibility.Collapsed;
            Step2Grid.Visibility = Visibility.Visible;

            Step1Circle.Background = Brushes.Transparent;
            Step1Circle.BorderBrush = new SolidColorBrush(Color.FromRgb(156, 163, 175));
            Step1Circle.BorderThickness = new Thickness(1.5);

            ((TextBlock)Step1Circle.Child).Foreground =
                new SolidColorBrush(Color.FromRgb(107, 114, 128));

            Step1Text.Foreground =
                new SolidColorBrush(Color.FromRgb(107, 114, 128));

            Step2Circle.Background =
                new SolidColorBrush(Color.FromRgb(109, 118, 131));

            Step2Circle.BorderThickness = new Thickness(0);

            ((TextBlock)Step2Circle.Child).Foreground = Brushes.White;

            Step2Text.Foreground =
                new SolidColorBrush(Color.FromRgb(102, 112, 133));
        }
    }
}
