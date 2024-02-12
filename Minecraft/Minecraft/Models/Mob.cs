using Minecraft.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Models
{
    public class Mob
    {
        [Key]
        public Guid MobId { get; set; }
        public string MobName { get; set; }
        public int MobHealth { get; set; }
        public ICollection<Location> Location { get; set; }
        public ICollection<Drop> Drop { get; set; }
        public Mob()
        {
            Location = new List<Location>();
            Drop = new List<Drop>();
        }

        //[NotMapped]
        //public string LoactionAsString
        //{
        //    get { return string.Join("\n", Location.Select(x => x.SpawnName)); }
        //}
    }
}
