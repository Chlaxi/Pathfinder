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
       // public DbSet<Spellbook> SpellBooks { get; set; }
        public DbSet<SpecialAbility> SpecialAbilities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                DBLogin.DBInfo);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //Player
            modelBuilder.Entity<Player>().ToTable("players");
            modelBuilder.Entity<Player>().Property(m => m.Id).HasColumnName("playerid");
            modelBuilder.Entity<Player>().Property(m => m.Username).HasColumnName("username");
            modelBuilder.Entity<Player>().HasKey(m => m.Id);
            //Add Password, salt

            #region Character
            //Character
            modelBuilder.Entity<Character>().ToTable("character");
            modelBuilder.Entity<Character>().Property(m => m.PlayerId).HasColumnName("playerid");
            modelBuilder.Entity<Character>().Property(m => m.Id).HasColumnName("characterid");
            modelBuilder.Entity<Character>().Property(m => m.Name).HasColumnName("character_name");
            modelBuilder.Entity<Character>().Property(m => m.RaceName).HasColumnName("race");
            modelBuilder.Entity<Character>().Ignore(m => m.Alignment);  //Add to DB
            modelBuilder.Entity<Character>().Ignore(m => m.Diety);  //Add to DB
            modelBuilder.Entity<Character>().Ignore(m => m.Homeland);

            //            modelBuilder.Entity<Character>().Property(m => m.Alignment).HasColumnName("playerid"); Use conversion thingy

            #region abilities 

            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Strength, Ability =>
                {
                    Ability.Property(e => e.BaseScore).HasColumnName("base_str");
                    Ability.Ignore(e => e.BaseModifier);// Property(e => e.BaseModifier).HasColumnName("str_mod");
                    Ability.Property(e => e.TempScore).HasColumnName("temp_str");
                    Ability.Ignore(e => e.TempModifier); // Property(e => e.TempModifier).HasColumnName("temp_str_mod");
                    Ability.Ignore(e => e.Modifier);
                    Ability.Ignore(e => e.RacialModifier);
                });
            });

            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Dexterity, Ability =>
                {
                    Ability.Property(e => e.BaseScore).HasColumnName("base_dex");
                    Ability.Ignore(e => e.BaseModifier);// Property(e => e.BaseModifier).HasColumnName("dex_mod");
                    Ability.Property(e => e.TempScore).HasColumnName("temp_dex");
                    Ability.Ignore(e => e.TempModifier); // Property(e => e.TempModifier).HasColumnName("temp_dex_mod");
                    Ability.Ignore(e => e.Modifier);
                    Ability.Ignore(e => e.RacialModifier);
                });
            });

            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Constitution, Ability =>
                {
                    Ability.Property(e => e.BaseScore).HasColumnName("base_con");
                    Ability.Ignore(e => e.BaseModifier);// Property(e => e.BaseModifier).HasColumnName("con_mod");
                    Ability.Property(e => e.TempScore).HasColumnName("temp_con");
                    Ability.Ignore(e => e.TempModifier); // Property(e => e.TempModifier).HasColumnName("temp_con_mod");
                    Ability.Ignore(e => e.Modifier);
                    Ability.Ignore(e => e.RacialModifier);
                });
            });

            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Intelligence, Ability =>
                {
                    Ability.Property(e => e.BaseScore).HasColumnName("base_int");
                    Ability.Ignore(e => e.BaseModifier);// Property(e => e.BaseModifier).HasColumnName("int_mod");
                    Ability.Property(e => e.TempScore).HasColumnName("temp_int");
                    Ability.Ignore(e => e.TempModifier); // Property(e => e.TempModifier).HasColumnName("temp_int_mod");
                    Ability.Ignore(e => e.Modifier);
                    Ability.Ignore(e => e.RacialModifier);
                });
            });

            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Wisdom, Ability =>
                {
                    Ability.Property(e => e.BaseScore).HasColumnName("base_wis");
                    Ability.Ignore(e => e.BaseModifier);// Property(e => e.BaseModifier).HasColumnName("wis_mod");
                    Ability.Property(e => e.TempScore).HasColumnName("temp_wis");
                    Ability.Ignore(e => e.TempModifier); // Property(e => e.TempModifier).HasColumnName("temp_wis_mod");
                    Ability.Ignore(e => e.Modifier);
                    Ability.Ignore(e => e.RacialModifier);
                });
            });

            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Charisma, Ability =>
                {
                    Ability.Property(e => e.BaseScore).HasColumnName("base_cha");
                    Ability.Ignore(e => e.BaseModifier);// Property(e => e.BaseModifier).HasColumnName("cha_mod");
                    Ability.Property(e => e.TempScore).HasColumnName("temp_cha");
                    Ability.Ignore(e => e.TempModifier); // Property(e => e.TempModifier).HasColumnName("temp_cha_mod");
                    Ability.Ignore(e => e.Modifier);
                    Ability.Ignore(e => e.RacialModifier);
                });
            });

            #endregion
            
            modelBuilder.Entity<Character>().Property(m => m.Experience).HasColumnName("experience");
            modelBuilder.Entity<Character>().Ignore(m => m.Size);

            //modelBuilder.Entity<Character>().Property(m => m.Size).HasColumnName("size").HasConversion(v => v.ToString(), v => (Size)Enum.Parse(typeof(Size), v));
            modelBuilder.Entity<Character>().Property(m => m.Gender).HasColumnName("gender");
            modelBuilder.Entity<Character>().Property(m => m.Age).HasColumnName("age");
            modelBuilder.Entity<Character>().Property(m => m.Height).HasColumnName("height");
            modelBuilder.Entity<Character>().Property(m => m.Weight).HasColumnName("weight");
            modelBuilder.Entity<Character>().Property(m => m.Hair).HasColumnName("hair");
            modelBuilder.Entity<Character>().Property(m => m.Eyes).HasColumnName("eyes");


            #region AC
            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.AC, AC =>
                {
                    
                    AC.Property(m => m.Armour).HasColumnName("ac_armour_bonus");
                    AC.Property(m => m.Shield).HasColumnName("ac_shield_bonus");
                    AC.Property(m => m.NaturalArmour).HasColumnName("ac_natural_armour");
                    AC.Property(m => m.Deflection).HasColumnName("ac_deflection");
                    //AC.Property(m => m.).HasColumnName("ac_temp_armour"); //TODO Reavluate whether needed, and add field/database
                    AC.Property(m => m.Misc).HasColumnName("ac_misc");

                    //Ignore the totals, since they don't have a getter.
                    //TODO: Remove from Database.
                    AC.Ignore(m => m.Total);
                    AC.Ignore(m => m.Touch);
                    AC.Ignore(m => m.FlatFooted);
                    
                    //Not added to Database yet.
                    AC.Ignore(m => m.TouchMisc);
                    AC.Ignore(m => m.FlatFootedMisc);
                    AC.Ignore(m => m.Note);

                    //Ignore since it has no setter
                    //TODO remove from database
                    AC.Ignore(m => m.Dex);
                    AC.Ignore(m => m.Size);
                });
            });


        #endregion

/*
        #region Hit Points
            modelBuilder.Entity<Character>().Property(m => m.HitPoints.MaxHitPoints).HasColumnName("hp_total");
            modelBuilder.Entity<Character>().Property(m => m.HitPoints.CurrentHitPoints).HasColumnName("hp_current");
            modelBuilder.Entity<Character>().Property(m => m.HitPoints.NonLethalDamage).HasColumnName("hp_non_lethal");
            modelBuilder.Entity<Character>().Property(m => m.HitPoints.Wounds).HasColumnName("hp_wounds");
        #endregion

         #region Saves
            #region Fortitude
            modelBuilder.Entity<Character>().Property(m => m.Fortitude.Total).HasColumnName("fortitude_total");
            modelBuilder.Entity<Character>().Property(m => m.Fortitude.Base).HasColumnName("fortitude_base");
            modelBuilder.Entity<Character>().Property(m => m.Fortitude.Ability).HasColumnName("fortitude_con_mod");
            modelBuilder.Entity<Character>().Property(m => m.Fortitude.Magic).HasColumnName("fortitude_magic_mod");
            modelBuilder.Entity<Character>().Property(m => m.Fortitude.Temporary).HasColumnName("fotritude_temp_mod");
            modelBuilder.Entity<Character>().Property(m => m.Fortitude.Misc).HasColumnName("fortitude_other");
            #endregion
            #region Reflex
            modelBuilder.Entity<Character>().Property(m => m.Reflex.Total).HasColumnName("reflex_total");
            modelBuilder.Entity<Character>().Property(m => m.Reflex.Base).HasColumnName("reflex_base");
            modelBuilder.Entity<Character>().Property(m => m.Reflex.Ability).HasColumnName("reflex_dex_mod");
            modelBuilder.Entity<Character>().Property(m => m.Reflex.Magic).HasColumnName("reflex_magic_mod");
            modelBuilder.Entity<Character>().Property(m => m.Reflex.Temporary).HasColumnName("reflex_temp_mod");
            modelBuilder.Entity<Character>().Property(m => m.Reflex.Misc).HasColumnName("reflex_other");
            #endregion
            #region Will
            modelBuilder.Entity<Character>().Property(m => m.Will.Total).HasColumnName("will_total");
            modelBuilder.Entity<Character>().Property(m => m.Will.Base).HasColumnName("will_base");
            modelBuilder.Entity<Character>().Property(m => m.Will.Ability).HasColumnName("will_con_mod");
            modelBuilder.Entity<Character>().Property(m => m.Will.Magic).HasColumnName("will_magic_mod");
            modelBuilder.Entity<Character>().Property(m => m.Will.Temporary).HasColumnName("will_temp_mod");
            modelBuilder.Entity<Character>().Property(m => m.Will.Misc).HasColumnName("will_other");
            #endregion
        #endregion
*/
            modelBuilder.Entity<Character>().Property(m => m.DamageReduction).HasColumnName("damage_reduction");
            modelBuilder.Entity<Character>().Property(m => m.SpellResistance).HasColumnName("spell_resistance");
            modelBuilder.Entity<Character>().Property(m => m.Resistance).HasColumnName("resistance");
            modelBuilder.Entity<Character>().Property(m => m.Immunity).HasColumnName("immunity");

            modelBuilder.Entity<Character>().Property(m => m.BaseAttackBonus).HasColumnName("bab");
    //        modelBuilder.Entity<Character>().Property(m => m.BaseAttackBonus).HasColumnName("range_bab");
    //        modelBuilder.Entity<Character>().Property(m => m.BaseAttackBonus).HasColumnName("melee_bab");


/*
            #region Combat Maneuver Bonus (CMB)
            modelBuilder.Entity<Character>().Property(m => m.CMB.Total).HasColumnName("cmb_total");
            modelBuilder.Entity<Character>().Property(m => m.CMB.BaseAttackBonus).HasColumnName("cmb_bab");
            modelBuilder.Entity<Character>().Property(m => m.CMB.Strength).HasColumnName("cmb_str");
            modelBuilder.Entity<Character>().Property(m => m.CMB.Size).HasColumnName("cmb_size_mod");
            modelBuilder.Entity<Character>().Property(m => m.CMB.Misc).HasColumnName("cmb_misc");
            modelBuilder.Entity<Character>().Property(m => m.CMB.Temp).HasColumnName("cmb_temp");

            #endregion

            #region Combat Maneuver Defence (CMD
            modelBuilder.Entity<Character>().Property(m => m.CMD.Total).HasColumnName("cmd_total");
            modelBuilder.Entity<Character>().Property(m => m.CMD.BaseAttackBonus).HasColumnName("cmd_bab");
            modelBuilder.Entity<Character>().Property(m => m.CMD.Strength).HasColumnName("cmd_str");
            modelBuilder.Entity<Character>().Property(m => m.CMD.Strength).HasColumnName("cmd_dex");
            modelBuilder.Entity<Character>().Property(m => m.CMD.Size).HasColumnName("cmd_size_mod");
            modelBuilder.Entity<Character>().Property(m => m.CMD.Misc).HasColumnName("cmd_misc");
            modelBuilder.Entity<Character>().Property(m => m.CMD.Temp).HasColumnName("cmd_temp");

            #endregion
*/
            modelBuilder.Entity<Character>().Property(m => m.Initiative).HasColumnName("initiative_total");
            modelBuilder.Entity<Character>().Property(m => m.InitiativeMiscModifier).HasColumnName("initiative_misc");
/*
            #region Speed
            modelBuilder.Entity<Character>().Property(m => m.Speed.Base).HasColumnName("speed_base");
            modelBuilder.Entity<Character>().Property(m => m.Speed.Armour).HasColumnName("speed_armour");
            modelBuilder.Entity<Character>().Property(m => m.Speed.Fly).HasColumnName("speed_fly");
            modelBuilder.Entity<Character>().Property(m => m.Speed.Swim).HasColumnName("speed_swim");
            modelBuilder.Entity<Character>().Property(m => m.Speed.Climb).HasColumnName("speed_climb");
            modelBuilder.Entity<Character>().Property(m => m.Speed.Burrow).HasColumnName("speed_burrow");
            modelBuilder.Entity<Character>().Property(m => m.Speed.Temporary).HasColumnName("speed_misc");
            #endregion
            */
            modelBuilder.Entity<Character>().Property(m => m.Languages).HasColumnName("languages");
            modelBuilder.Entity<Character>().Property(m => m.Platinum).HasColumnName("platinum");
            modelBuilder.Entity<Character>().Property(m => m.Gold).HasColumnName("gold");
            modelBuilder.Entity<Character>().Property(m => m.Silver).HasColumnName("silver");
            modelBuilder.Entity<Character>().Property(m => m.Copper).HasColumnName("copper");
            modelBuilder.Entity<Character>().Property(m => m.Note).HasColumnName("notes");

            #region Skills
         //   modelBuilder.Entity<Character>().Ignore(m => m.Acrobatic);


            #endregion

            #endregion


            #region Spells
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
            #endregion

            #region Feats
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
            #endregion


            #region Special Abilities
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
            #endregion

            #region Race
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
            #endregion

            #region Classes
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
            #endregion



        }

        /*public void MapClass(this ModelBuilder modelBuilder, Skills skill, string identifier)
        {
            Console.WriteLine(nameof(skill));
            string skillName = nameof(skill);
            
            modelBuilder.Entity<Character>().Property(m => skill.Total).HasColumnName(identifier + "_total");
            modelBuilder.Entity<Character>().Property(m => m.Acrobatic.Ranks).HasColumnName(identifier + "_ranks");
            modelBuilder.Entity<Character>().Property(m => m.Acrobatic.AbilityModifier).HasColumnName(identifier + "_mod");
            modelBuilder.Entity<Character>().Property(m => m.Acrobatic.Racial).HasColumnName(identifier + "_racial");
            modelBuilder.Entity<Character>().Property(m => m.Acrobatic.IsClassSkill).HasColumnName(identifier + "_traits"); //name should be "_classSkill"
            modelBuilder.Entity<Character>().Property(m => m.Acrobatic.Misc).HasColumnName(identifier + "_misc");
        }*/

    }
}
