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
        public DbSet<Class> Classes { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Feat> Feats { get; set; }
        public DbSet<Spell> Spells { get; set; }
       // public DbSet<Spellbook> SpellBooks { get; set; }
        public DbSet<SpecialAbility> SpecialAbilities { get; set; }

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

            /* Won't work until I either make another class, or gather the tables
            modelBuilder.Entity<Spell>().ToTable("spell_classes");
            modelBuilder.Entity<Spell>().Property(m => m.Bard).HasColumnName("bard");
            modelBuilder.Entity<Spell>().Property(m => m.Cleric).HasColumnName("cleric");
            modelBuilder.Entity<Spell>().Property(m => m.Druid).HasColumnName("druid");
            modelBuilder.Entity<Spell>().Property(m => m.Paladin).HasColumnName("paladin");
            modelBuilder.Entity<Spell>().Property(m => m.Ranger).HasColumnName("ranger");
            modelBuilder.Entity<Spell>().Property(m => m.Sorcerer).HasColumnName("sorcerer");
            modelBuilder.Entity<Spell>().Property(m => m.Wizard).HasColumnName("wizard");
            */
            /* Additional columns
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("alchemist");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("summoner");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("oracle");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("witch");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("inquisitor");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("antipaladin");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("magus");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("adept");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("bloodrager");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("shaman");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("skald");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("investigator");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("hunter");
                        modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("sla_level");
            */

            /* Spell types
            modelBuilder.Entity<Spell>().ToTable("spell_types");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("acid");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("air");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("chaotic");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("cold");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("curse");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("darkness");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("death");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("disease");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("earth");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("electricity");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("emotion");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("evil");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("fear");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("fire");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("force");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("good");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("language_dependent");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("lawful");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("light");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("mind_affecting");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("pain");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("poison");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("shadow");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("sonic");
            modelBuilder.Entity<Spell>().Property(m => m.).HasColumnName("water");
            */


            //Feats
            modelBuilder.Entity<Feat>().ToTable("feats");
            modelBuilder.Entity<Feat>().Property(m => m.Id).HasColumnName("featid").IsRequired(true);
            modelBuilder.Entity<Feat>().Property(m => m.Name).HasColumnName("feat_name").IsRequired(true);
            modelBuilder.Entity<Feat>().Property(m => m.Type).HasColumnName("feat_type");
            modelBuilder.Entity<Feat>().Property(m => m.Description).HasColumnName("description");
            modelBuilder.Entity<Feat>().Property(m => m.Prerequisites).HasColumnName("prerequisites");
            modelBuilder.Entity<Feat>().Property(m => m.PrerequisiteFeats).HasColumnName("prerequisite_feats");
            modelBuilder.Entity<Feat>().Property(m => m.Benefit).HasColumnName("benefit");
            modelBuilder.Entity<Feat>().Property(m => m.Normal).HasColumnName("normal");
            modelBuilder.Entity<Feat>().Property(m => m.Special).HasColumnName("special");
            modelBuilder.Entity<Feat>().Property(m => m.FullText).HasColumnName("fulltext");
            modelBuilder.Entity<Feat>().Property(m => m.Multiples).HasColumnName("multiples");
            modelBuilder.Entity<Feat>().Property(m => m.PrerequisiteSkills).HasColumnName("prerequisite_skills");
            modelBuilder.Entity<Feat>().Property(m => m.RaceNames).HasColumnName("race_name");
            modelBuilder.Entity<Feat>().Property(m => m.Source).HasColumnName("source");
            modelBuilder.Entity<Feat>().HasKey(m => m.Id);


            //Special Abilities
            modelBuilder.Entity<SpecialAbility>().ToTable("specialabilities");
            modelBuilder.Entity<SpecialAbility>().Property(m => m.Id).HasColumnName("said").IsRequired(true);
            modelBuilder.Entity<SpecialAbility>().Property(m => m.Name).HasColumnName("special_ability");
            modelBuilder.Entity<SpecialAbility>().Property(m => m.Description).HasColumnName("description");
            modelBuilder.Entity<SpecialAbility>().Property(m => m.Type).HasColumnName("type");
            modelBuilder.Entity<SpecialAbility>().Property(m => m.Category).HasColumnName("category");
            modelBuilder.Entity<SpecialAbility>().Property(m => m.ClassName).HasColumnName("class");
            modelBuilder.Entity<SpecialAbility>().Property(m => m.Source).HasColumnName("source");
            modelBuilder.Entity<SpecialAbility>().HasKey(m => m.Id);

            //Race
            modelBuilder.Entity<Race>().ToTable("races");
            modelBuilder.Entity<Race>().Property(m => m.Name).HasColumnName("race_name").IsRequired(true);
            modelBuilder.Entity<Race>().Property(m => m.Size).HasColumnName("size").HasConversion(v => v.ToString(), v=> (Size)Enum.Parse(typeof(Size), v));
            modelBuilder.Entity<Race>().Property(m => m.Speed).HasColumnName("speed");
            modelBuilder.Entity<Race>().Property(m => m.LanguagesKnown).HasColumnName("known_languages");
            modelBuilder.Entity<Race>().Property(m => m.LanguagesAvailable).HasColumnName("available_languages");
            modelBuilder.Entity<Race>().Property(m => m.Strength).HasColumnName("str");
            modelBuilder.Entity<Race>().Property(m => m.Dexterity).HasColumnName("dex");
            modelBuilder.Entity<Race>().Property(m => m.Constitution).HasColumnName("con");
            modelBuilder.Entity<Race>().Property(m => m.Intelligence).HasColumnName("int");
            modelBuilder.Entity<Race>().Property(m => m.Wisdom).HasColumnName("wis");
            modelBuilder.Entity<Race>().Property(m => m.Charisma).HasColumnName("cha");
            modelBuilder.Entity<Race>().Property(m => m.SpecialModifier).HasColumnName("varied_attribute");
            modelBuilder.Entity<Race>().Property(m => m.Description).HasColumnName("race_description");
            modelBuilder.Entity<Race>().HasKey(m => m.Name);


            //Class
            modelBuilder.Entity<Class>().ToTable("classes");
            modelBuilder.Entity<Class>().Property(m => m.Name).HasColumnName("class");
            modelBuilder.Entity<Class>().Property(m => m.HitDie).HasColumnName("hit_die");
            modelBuilder.Entity<Class>().Property(m => m.ClassSkills).HasColumnName("class_skills");
            modelBuilder.Entity<Class>().Property(m => m.SkillsPerLevel).HasColumnName("skills");
            modelBuilder.Entity<Class>().Property(m => m.ArmourProficiency).HasColumnName("armour_proficiency");
            modelBuilder.Entity<Class>().Property(m => m.WeaponProficiency).HasColumnName("weapon_proficiency");
            modelBuilder.Entity<Class>().Property(m => m.Spells).HasColumnName("spells");
            modelBuilder.Entity<Class>().Property(m => m.source).HasColumnName("source");
            modelBuilder.Entity<Class>().HasKey(m => m.Name);






            //Orders



            //    modelBuilder.Entity<Order>().ToTable("orders");
            // modelBuilder.Entity<Order>().Property(m => m.Shipped).HasColumnName("shippeddate").IsRequired(false);
            // modelBuilder.Entity<Order>().Ignore(m => m.OrderDetails);
            // modelBuilder.Entity<Order>().HasKey(m => m.Id);


            //   modelBuilder.Entity<OrderDetails>().HasKey(m => new { m.OrderId, m.ProductId });

        }

    }
}
