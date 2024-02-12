using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Minecraft.Controllers
{
    public class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<Models.Location> locations)
                return string.Join("\n", locations.Select(l => l.SpawnName));
            if (value is ICollection<Models.Drop> drops)
                return string.Join("\n", drops.Select(a => a.DropName));
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
