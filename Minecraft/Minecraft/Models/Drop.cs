using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Models
{
    public class Drop
    {
        [Key]
        public Guid DropId { get; set; }
        public string DropName { get; set; }
        public ICollection<Mob> Mobs { get; set; }

        public Drop() 
        {
            Mobs = new List<Mob>();
        }
    }
}
