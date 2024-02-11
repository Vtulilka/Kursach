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
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE MOBS");
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE DROPS");
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE LOCATIONS");
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE MOBLOCATION");
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE MOBDROP");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Mobs', RESEED, 0)");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Drops', RESEED, 0)");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Locations', RESEED, 0)");
                db.SaveChanges();
            }
        }
    }
}
