using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Models
{
    public class Drop
    {
        public Guid DropId { get; set; }
        public string DropName { get; set; }
        public ICollection<Mob> Mobs { get; set; }
    }
}
