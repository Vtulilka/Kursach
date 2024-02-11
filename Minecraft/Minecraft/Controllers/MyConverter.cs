using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Minecraft.Controllers
{
    public class MyConverter : IMultiValueConverter
    {
        ICollection<Models.Location> _locations;
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length > 1)
                return string.Join("\n", _locations.Select(l => l.SpawnName));
            if (values.Length > 1 && values[0] is ICollection<Models.Drop> drops)
                return string.Join("\n", drops.Select(a => a.DropName));
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
