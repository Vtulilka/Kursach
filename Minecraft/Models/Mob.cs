using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Models
{
    public class Mob
    {
        public Guid MobId { get; set; }
        public string MobName { get; set; }
        public int MobHealth { get; set; }
        public ICollection<Location> Location { get; set; }
        public ICollection<Drop> Drop { get; set; }
        //public Mob()
        //{
        //    Location = new List<Location>();
        //}
    }
}
