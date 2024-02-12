using Minecraft.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Controllers
{
    public class DataBaseController
    {
        public void DeleteAll()
        {
            using (MobsDbContext db = new MobsDbContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM MOBDROP");
                db.Database.ExecuteSqlCommand("DELETE FROM DROPS");
                db.Database.ExecuteSqlCommand("DELETE FROM MOBLOCATION");
                
                
                db.Database.ExecuteSqlCommand("DELETE FROM LOCATIONS");
                db.Database.ExecuteSqlCommand("DELETE FROM MOBS");


                //db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Mobs', RESEED, 0)");
                //db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Drops', RESEED, 0)");
                //db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Locations', RESEED, 0)");
                db.SaveChanges();
            }
        }

        public void DeleteElement(Models.Mob element)
        {
            ViewModel.Mine VM = new ViewModel.Mine();
            using (MobsDbContext db = new MobsDbContext())
            {
                Models.Mob a = db.Mobs.Where(p => p.MobId == element.MobId).First();
                if (element != null)
                {
                    //db.Mobs.Remove(element);
                    db.Entry(a).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();
                }
            }
            VM.GetMobs.Execute(null);
        }
    }
}
