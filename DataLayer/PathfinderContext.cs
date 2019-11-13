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
        public DbSet<ClassInfo> ClassInfo { get; set; }
        public DbSet<CharacterClasses> CharacterClasses { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Feat> Feats { get; set; }
        public DbSet<CharacterFeats> CharacterFeats { get; set; }
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
            modelBuilder.Entity<Character>().HasKey(m => m.Id);
            modelBuilder.Entity<Character>().Property(m => m.Name).HasColumnName("character_name");
            modelBuilder.Entity<Character>().Property(m => m.RaceName).HasColumnName("race");
            modelBuilder.Entity<Character>().Ignore(m => m.Class);
            modelBuilder.Entity<Character>().Ignore(m => m.EffectiveLevel);
            modelBuilder.Entity<Character>().Ignore(m => m.Diety);  //Add to DB
            modelBuilder.Entity<Character>().Ignore(m => m.Homeland);
            modelBuilder.Entity<Character>().Ignore(m => m.Feats);

            modelBuilder.Entity<Character>().Property(m => m.Alignment).HasColumnName("alignment").HasConversion(v => v.ToString(), v => (Alignment)Enum.Parse(typeof(Alignment), v.Replace(" ", String.Empty), true));

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
                    Ability.Ignore(e => e.TotalScore);
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
                    Ability.Ignore(e => e.TotalScore);
                    
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
                    Ability.Ignore(e => e.TotalScore);
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
                    Ability.Ignore(e => e.TotalScore);
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
                    Ability.Ignore(e => e.TotalScore);

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


            #region Hit Points
            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.HitPoints, HP =>
                {
                    HP.Property(m => m.MaxHitPoints).HasColumnName("hp_total");
                    HP.Property(m => m.CurrentHitPoints).HasColumnName("hp_current");
                    HP.Property(m => m.NonLethalDamage).HasColumnName("hp_non_lethal");
                    HP.Property(m => m.Wounds).HasColumnName("hp_wounds");
                });
            });
            #endregion

            #region Saves
            #region Fortitude
            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Fortitude, Fortitude =>
                {
                    Fortitude.Ignore(m => m.Total);     //.HasColumnName("fortitude_total");
                    Fortitude.Ignore(m => m.Base);      //.HasColumnName("fortitude_base");
                    Fortitude.Ignore(m => m.Ability);   //.HasColumnName("fortitude_con_mod");
                    Fortitude.Property(m => m.Magic).HasColumnName("fortitude_magic_mod");
                    Fortitude.Property(m => m.Temporary).HasColumnName("fortitude_temp_mod");
                    Fortitude.Property(m => m.Misc).HasColumnName("fortitude_other");

                    //TODO Add the DB
                    Fortitude.Ignore(m => m.Note);
                });
            });


            #endregion
            #region Reflex
            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Reflex, Reflex =>
                {
                    Reflex.Ignore(m => m.Total);
                    Reflex.Ignore(m => m.Base);
                    Reflex.Ignore(m => m.Ability);
                    Reflex.Property(m => m.Magic).HasColumnName("reflex_magic_mod");
                    Reflex.Property(m => m.Temporary).HasColumnName("reflex_temp_mod");
                    Reflex.Property(m => m.Misc).HasColumnName("reflex_other");
                    
                    //TODO Add the DB
                    Reflex.Ignore(m => m.Note);

                    //Remove from DB
//                    Reflex.Ignore(m => m.Total).HasColumnName("reflex_total");
//                    Reflex.Ignore(m => m.Base).HasColumnName("reflex_base");
//                    Reflex.Ignore(m => m.Ability).HasColumnName("reflex_dex_mod");
                });
            });

            #endregion
            #region Will
            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Will, Will =>
                {
                    Will.Ignore(m => m.Total);    //.HasColumnName("will_total");
                    Will.Ignore(m => m.Base);     //.HasColumnName("will_base");
                    Will.Ignore(m => m.Ability);  //.HasColumnName("will_con_mod");
                    Will.Property(m => m.Magic).HasColumnName("will_magic_mod");
                    Will.Property(m => m.Temporary).HasColumnName("will_temp_mod");
                    Will.Property(m => m.Misc).HasColumnName("will_other");

                    //TODO Add the DB
                    Will.Ignore(m => m.Note);
                });
            });

            #endregion
        #endregion

            modelBuilder.Entity<Character>().Property(m => m.DamageReduction).HasColumnName("damage_reduction");
            modelBuilder.Entity<Character>().Property(m => m.SpellResistance).HasColumnName("spell_resistance");
            modelBuilder.Entity<Character>().Property(m => m.Resistance).HasColumnName("resistance");
            modelBuilder.Entity<Character>().Property(m => m.Immunity).HasColumnName("immunity");

            modelBuilder.Entity<Character>().Ignore(m => m.BaseAttackBonus);
            //TODO: Remove from DB, since it has no setter, and gets info from the class. Maybe change to Misc attack bonus?
            //Property(m => m.BaseAttackBonus).HasColumnName("bab");
            //        modelBuilder.Entity<Character>().Property(m => m.BaseAttackBonus).HasColumnName("range_bab");
            //        modelBuilder.Entity<Character>().Property(m => m.BaseAttackBonus).HasColumnName("melee_bab");

            
            #region Combat Maneuver Bonus (CMB)
            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.CMB, CMB =>
                {
                    //Ignored since they don't have settersm since they rely on information from the character
                    CMB.Ignore(m => m.Total);           
                    CMB.Ignore(m => m.BaseAttackBonus); 
                    CMB.Ignore(m => m.Strength);        
                    CMB.Ignore(m => m.Size);            
                    
                    CMB.Property(m => m.Misc).HasColumnName("cmb_misc");
                    CMB.Property(m => m.Temp).HasColumnName("cmb_temp");


                    //TODO Remove from DB
                    //.HasColumnName("cmb_total");
                    //.HasColumnName("cmb_bab");
                    //.HasColumnName("cmb_str");
                    //.HasColumnName("cmb_size_mod");
                });
            });
            #endregion

            #region Combat Maneuver Defence (CMD)
            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.CMD, CMD =>
                {
                    //Ignored since they don't have settersm since they rely on information from the character
                    CMD.Ignore(m => m.Total);
                    CMD.Ignore(m => m.BaseAttackBonus);
                    CMD.Ignore(m => m.Strength);
                    CMD.Ignore(m => m.Dexterity);
                    CMD.Ignore(m => m.Size);

                    CMD.Property(m => m.Misc).HasColumnName("cmd_misc");
                    CMD.Property(m => m.Temp).HasColumnName("cmd_temp");


                    //TODO Remove from DB
                    //.HasColumnName("cmd_total");
                    //.HasColumnName("cmd_bab");
                    //.HasColumnName("cmd_str");
                    //.HasColumnName("cmd_dex");
                    //.HasColumnName("cmd_size_mod");
                });
            });
            #endregion

            modelBuilder.Entity<Character>().Ignore(m => m.Initiative);
                //TODO: Remove from DB, since it doesn't have a setter.
                //.HasColumnName("initiative_total");
            modelBuilder.Entity<Character>().Property(m => m.InitiativeMiscModifier).HasColumnName("initiative_misc");

            #region Speed
            modelBuilder.Entity<Character>(m =>
            {
                m.OwnsOne(e => e.Speed, Speed =>
                {
                    //Gets value from race.
                    Speed.Ignore(m => m.Base);
                    Speed.Property(m => m.BaseModifier).HasColumnName("speed_base");

                    //TODO Add to DB.                    
                    Speed.Ignore(m => m.BaseTempModifier);   //Property(m => m.BaseTempModifier).HasColumnName("speed_base_temporary");
                    Speed.Property(m => m.Armour).HasColumnName("speed_armour");
                    Speed.Property(m => m.Fly).HasColumnName("speed_fly");
                    Speed.Property(m => m.Swim).HasColumnName("speed_swim");
                    Speed.Property(m => m.Climb).HasColumnName("speed_climb");
                    Speed.Property(m => m.Burrow).HasColumnName("speed_burrow");
                    Speed.Property(m => m.Temporary).HasColumnName("speed_misc");
                });
            });
            
            #endregion
            
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

            modelBuilder.Entity<CharacterFeats>().ToTable("characterfeats");
            modelBuilder.Entity<CharacterFeats>().Property(m => m.CharacterId).HasColumnName("characterid").IsRequired(true);
            modelBuilder.Entity<CharacterFeats>().Property(m => m.FeatId).HasColumnName("featid").IsRequired(true);
            modelBuilder.Entity<CharacterFeats>().Property(m => m.Choice).HasColumnName("choice");
            modelBuilder.Entity<CharacterFeats>().Property(m => m.Multiple).HasColumnName("multiple");
            modelBuilder.Entity<CharacterFeats>().Property(m => m.Note).HasColumnName("note");
            modelBuilder.Entity<CharacterFeats>().HasKey(m => new { m.CharacterId, m.FeatId, m.Choice });
            modelBuilder.Entity<CharacterFeats>().Ignore(m => m.Feat);
            modelBuilder.Entity<CharacterFeats>().Ignore(m => m.FeatName);


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

            modelBuilder.Entity<CharacterClasses>().ToTable("characterclasses");
            modelBuilder.Entity<CharacterClasses>().Property(m => m.CharacterId).HasColumnName("characterid");
            modelBuilder.Entity<CharacterClasses>().Property(m => m.ClassName).HasColumnName("class");
            modelBuilder.Entity<CharacterClasses>().Property(m => m.Level).HasColumnName("level");
            modelBuilder.Entity<CharacterClasses>().HasKey(m => new { m.CharacterId, m.ClassName });

            modelBuilder.Entity<ClassInfo>().ToTable("class_levels");
            modelBuilder.Entity<ClassInfo>().Property(m => m.ClassName).HasColumnName("class");
            modelBuilder.Entity<ClassInfo>().Property(m => m.Level).HasColumnName("level");
            modelBuilder.Entity<ClassInfo>().Property(m => m.BaseAttackBonus).HasColumnName("base_attack_bonus");
            modelBuilder.Entity<ClassInfo>().Property(m => m.BaseFortitude).HasColumnName("fortitude");
            modelBuilder.Entity<ClassInfo>().Property(m => m.BaseReflex).HasColumnName("reflex");
            modelBuilder.Entity<ClassInfo>().Property(m => m.BaseWill).HasColumnName("will");
            modelBuilder.Entity<ClassInfo>().Property(m => m.Specials).HasColumnName("special");
            //TODO Add spells
            modelBuilder.Entity<ClassInfo>().HasKey(m => new { m.ClassName, m.Level });
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
