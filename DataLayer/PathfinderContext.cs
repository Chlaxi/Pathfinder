using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class PathfinderContext : DbContext
    {
//        Defines the database sets we want to use

        public DbSet<Character> Characters { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Feat> Feats { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<Spellbook> SpellBooks { get; set; }
        public DbSet<SpecialAbility> SpecialAbilities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                DBLogin.DBInfo);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Orders
        //    modelBuilder.Entity<Order>().ToTable("orders");
           // modelBuilder.Entity<Order>().Property(m => m.Shipped).HasColumnName("shippeddate").IsRequired(false);
           // modelBuilder.Entity<Order>().Ignore(m => m.OrderDetails);
           // modelBuilder.Entity<Order>().HasKey(m => m.Id);

         
         //   modelBuilder.Entity<OrderDetails>().HasKey(m => new { m.OrderId, m.ProductId });

        }

    }
}
