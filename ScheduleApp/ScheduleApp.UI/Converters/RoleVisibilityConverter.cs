using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ScheduleApp.UI.Converters
{
    public class RoleVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // value es el rol del usuario (string)
            // parameter es el rol que queremos Ocultar (string, ej: "Coordinador")

            if (value != null && parameter != null && value.ToString() == parameter.ToString())
            {
                // Si el rol es el parametro, ocultamos la tarjeta
                return Visibility.Collapsed;
            }

            // Si el rol es administrador (o nulo), mostramos la tarjeta
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}