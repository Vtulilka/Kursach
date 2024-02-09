using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Models
{
    public class Location
    {
        public Guid SpawnId { get; set; }
        public string SpawnName { get; set; }
        public ICollection<Mob> Mobs { get; set; }
        //public Location()
        //{
        //    Mobs = new List<Mob>();
        //}
    }
}
