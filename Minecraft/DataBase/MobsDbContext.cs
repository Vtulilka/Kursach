using Minecraft.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.DataBase
{
    public class MobsDbContext : DbContext
    {
        public MobsDbContext() : base("name=MinecraftDataBase")
        { }

        public DbSet<Models.Mob> Mobs { get; set; }
        public DbSet<Models.Drop> Drops { get; set; }
        public DbSet<Models.Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Mob>().Property(p => p.MobId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Models.Drop>().Property(p => p.DropId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Models.Location>().Property(p => p.SpawnId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Mob>().HasMany(m => m.Location).WithMany(l => l.Mobs).Map(m =>
            {
                m.ToTable("MobLocation"); // указываем имя промежуточной таблицы 
                m.MapLeftKey("MobId"); // указываем имя столбца внешнего ключа для таблицы Mob
                m.MapRightKey("SpawnId"); // указываем имя столбца внешнего ключа для таблицы Location
            });

            modelBuilder.Entity<Mob>().HasMany(m => m.Drop).WithMany(l => l.Mobs).Map(m =>
            {
                m.ToTable("MobDrop"); // указываем имя промежуточной таблицы 
                m.MapLeftKey("MobId"); // указываем имя столбца внешнего ключа для таблицы Mob
                m.MapRightKey("DropId"); // указываем имя столбца внешнего ключа для таблицы Location
            });
        }


    }
}
