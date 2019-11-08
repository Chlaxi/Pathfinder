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

      //  public DbSet<Character> Characters { get; set; }
       // public DbSet<Player> Players { get; set; }
       // public DbSet<Class> Classes { get; set; }
       // public DbSet<Race> Races { get; set; }
       // public DbSet<Feat> Feats { get; set; }
        public DbSet<Spell> Spells { get; set; }
       // public DbSet<Spellbook> SpellBooks { get; set; }
       // public DbSet<SpecialAbility> SpecialAbilities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                DBLogin.DBInfo);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Spells
            modelBuilder.Entity<Spell>().ToTable("spells");
            modelBuilder.Entity<Spell>().Property(m => m.Id).HasColumnName("spellid").IsRequired(true);
            modelBuilder.Entity<Spell>().Property(m => m.Name).HasColumnName("spell_name").IsRequired(true);
            modelBuilder.Entity<Spell>().Property(m => m.School).HasColumnName("school");
            modelBuilder.Entity<Spell>().Property(m => m.SubSchool).HasColumnName("subschool");
            modelBuilder.Entity<Spell>().Property(m => m.Element).HasColumnName("element");
            modelBuilder.Entity<Spell>().Property(m => m.ShortDescription).HasColumnName("short_description");
            modelBuilder.Entity<Spell>().Property(m => m.SpellLevel).HasColumnName("spell_level");
            modelBuilder.Entity<Spell>().Property(m => m.CastingTime).HasColumnName("casting_time");
            modelBuilder.Entity<Spell>().Property(m => m.Components).HasColumnName("components");
            modelBuilder.Entity<Spell>().Property(m => m.Range).HasColumnName("range");
            modelBuilder.Entity<Spell>().Property(m => m.Area).HasColumnName("area");
            modelBuilder.Entity<Spell>().Property(m => m.Effect).HasColumnName("effect");
            modelBuilder.Entity<Spell>().Property(m => m.Target).HasColumnName("targets");
            modelBuilder.Entity<Spell>().Property(m => m.Duration).HasColumnName("duration");
            modelBuilder.Entity<Spell>().Property(m => m.SavingThrow).HasColumnName("saving_throw");
            modelBuilder.Entity<Spell>().Property(m => m.SpellResistance).HasColumnName("spell_resistence"); //Note name
            modelBuilder.Entity<Spell>().Property(m => m.Description).HasColumnName("description");
            modelBuilder.Entity<Spell>().Property(m => m.Domain).HasColumnName("domain");
            modelBuilder.Entity<Spell>().Property(m => m.Bloodline).HasColumnName("bloodline");
            modelBuilder.Entity<Spell>().Property(m => m.Patron).HasColumnName("patron");
            modelBuilder.Entity<Spell>().Property(m => m.Dismissible).HasColumnName("dismissible");
            modelBuilder.Entity<Spell>().Property(m => m.Shapeable).HasColumnName("shapeable");
            modelBuilder.Entity<Spell>().Property(m => m.Verbal).HasColumnName("verbal");
            modelBuilder.Entity<Spell>().Property(m => m.Somatic).HasColumnName("somatic");
            modelBuilder.Entity<Spell>().Property(m => m.Material).HasColumnName("material");
            modelBuilder.Entity<Spell>().Property(m => m.Focus).HasColumnName("focus");
            modelBuilder.Entity<Spell>().Property(m => m.DivineFocus).HasColumnName("divine_focus");
            modelBuilder.Entity<Spell>().Property(m => m.CostlyComponent).HasColumnName("costly_components");
            modelBuilder.Entity<Spell>().Property(m => m.MaterialCost).HasColumnName("material_costs");
            modelBuilder.Entity<Spell>().Property(m => m.Source).HasColumnName("source");
            modelBuilder.Entity<Spell>().Property(m => m.DescriptionFormatted).HasColumnName("description_formated");
            modelBuilder.Entity<Spell>().Property(m => m.FullText).HasColumnName("full_text");
            modelBuilder.Entity<Spell>().HasKey(m => m.Id);


            //Orders



            //    modelBuilder.Entity<Order>().ToTable("orders");
            // modelBuilder.Entity<Order>().Property(m => m.Shipped).HasColumnName("shippeddate").IsRequired(false);
            // modelBuilder.Entity<Order>().Ignore(m => m.OrderDetails);
            // modelBuilder.Entity<Order>().HasKey(m => m.Id);


            //   modelBuilder.Entity<OrderDetails>().HasKey(m => new { m.OrderId, m.ProductId });

        }

    }
}
