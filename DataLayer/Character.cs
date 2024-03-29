﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer
{
    //TODO:  Spells, Race, Classes, Feats, Special Abilities, Skills

    public enum AbilityEnum { Str, Dex, Con, Int, Wis, Cha }
    public enum Size { none, small, medium, large }

    public enum Alignment { LawfulGood, LawfulNeutral, LawfulEvil
                            ,NeutralGood, Neutral, NeutralEvil
                            ,ChaoticGood, ChaoticNeutral, ChaoticEvil}
    public class Character
    {
        public int PlayerId { get; set; }
       // public Player Player { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public string RaceName { get; set; }
        public Race Race { get; set; }
        public List<CharacterClasses> Class { get; set; }
        public int EffectiveLevel
        {
            get
            {
                int level = 0;
                if (Class != null && Class.Count > 0)
                {
                    
                    foreach(var l in Class)
                    {
                        level += l.Level;
                    }
                }
                return level;
            }
        } 
        public Alignment Alignment { get; set; }

        public string Gender { get; set; }
        public string Age { get; set; }

        public string Deity { get; set; }
        public string Homeland { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Hair { get; set; }
        public string Eyes { get; set; }
        public string Experience { get; set; }

        public Ability Strength { get; set; }
        public Ability Dexterity { get; set; }
        public Ability Constitution { get; set; }
        public Ability Intelligence { get; set; }
        public Ability Wisdom { get; set; }
        public Ability Charisma { get; set; }

        public ArmourClass AC { get; set; }
        
        
        public Save Fortitude { get; set; }
        public Save Reflex { get; set; }
        public Save Will { get; set; }

        public List<CharacterFeats> Feats { get; set; }
        //       public List<SpecialAbility> SpecialAbilities {get; set;}
        public Spellbook Spellbook { get; set; }

        #region Skills (WIP)
        /*
    public Skills Acrobatic { get; set; } //Dex
    public Skills Appraise { get; set; } //Int
    public Skills Bluff { get; set; } //Cha
    public Skills Climb { get; set; } //Str
    public List<Skills> Craft { get; set; } //Int
    public Skills Diplomacy { get; set; } //Cha
    public Skills DisableDevice { get; set; } //Dex *
    public Skills Disguise { get; set; } //Cha
    public Skills EscapeArtist { get; set; } //Dex
    public Skills Fly { get; set; } //Dex
    public Skills HandleAnimal { get; set; } //Cha *
    public Skills Heal { get; set; } //Wis
    public Skills Intimidate { get; set; } //Cha
    public Skills KnowledgeArcana { get; set; } //Int *
    public Skills KnowledgeDungeoneering { get; set; } //Int *
    public Skills KnowledgeEngineering { get; set; } //Int *
    public Skills KnowledgeGeography { get; set; } //Int * 
    public Skills KnowledgeHistory { get; set; } //Int *
    public Skills KnowledgeLocal { get; set; } //Int *
    public Skills KnowledgeNature { get; set; } //Int *
    public Skills KnowledgeNobility { get; set; } //Int *
    public Skills KnowledgePlanes { get; set; } //Int *
    public Skills KnowledgeReligion { get; set; } //Int *
    public Skills Linguistics { get; set; } //Int *
    public Skills Perception { get; set; } //Wis
    public List<Skills> Perform { get; set; } //Cha
    public List<Skills> Profession { get; set; } //Wis *
    public Skills Ride { get; set; } //Dex
    public Skills SenseMotive { get; set; } //Int
    public Skills SleightOfHand { get; set; } //Dex *
    public Skills Spellcraft { get; set; } //Int *
    public Skills Stealth { get; set; } //Dex 
    public Skills Survival { get; set; } //Wis 
    public Skills Swim { get; set; } //Str 
    public Skills UseMagicDevice { get; set; } //Int *
    */
        #endregion

        public HitPoints HitPoints { get; set; }

        public Speed Speed { get; set; }
        
        public int? Initiative {
            get
            {
                int initiative = 0;
                if (Dexterity!=null)
                    initiative += (Dexterity.Modifier == null) ? 0 : (int)Dexterity.Modifier;
                initiative += (InitiativeMiscModifier == null) ? 0 : (int)InitiativeMiscModifier;

                return initiative;
            }
        }
        public int? InitiativeMiscModifier { get; set; }

        public int? BaseAttackBonus { get; }

        public Size Size { get; set; }
        
        public CombatManeuverBonus CMB { get; set; }
        public CombatManeuverDefence CMD { get; set; }

        public string SpellResistance { get; set; }
        public string DamageReduction { get; set; }

        public string Resistance { get; set; }
        public string Immunity { get; set; }
    
        public int? Platinum { get; set; }
        public int? Gold { get; set; }
        public int? Silver { get; set; }
        public int? Copper { get; set; }

        public string Languages { get; set; }

        public string Note { get; set; }

        /// <summary>
        /// Converts the ability enum to the corrosponding ability
        /// </summary>
        /// <param name="ability"></param>
        /// <returns>Returns the ability</returns>
        public Ability GetAbilityFromEnum(AbilityEnum ability)
        {
            switch(ability)
            {
                case AbilityEnum.Str:
                    return Strength;

                case AbilityEnum.Dex:
                    return Dexterity;

                case AbilityEnum.Con:
                    return Constitution;

                case AbilityEnum.Int:
                    return Intelligence;

                case AbilityEnum.Wis:
                    return Wisdom;

                case AbilityEnum.Cha:
                    return Charisma;
                    
                default: return Strength; 
            }
            
        }

        public static int? GetSizeDefensiveModifier(Size size)
        {
            switch (size)
            {
                case Size.medium:
                    return 0;
                case Size.small:
                    return 1;
                case Size.large:
                    return -1;

                default:
                    return 0;
            }
        }

        public static int? GetSizeOffensiveModifier(Size size)
        {
            
            switch (size)
            {
                case Size.medium:
                    return 0;

                case Size.small:
                    return -1;
                case Size.large:
                    return 1;

                default:
                    return 0;
            }
        }


        public class ArmourClass
        {
            private ArmourClass() { }
            //Empty constructor, so the Mapping works
            public ArmourClass(int? Armour, int? Shield, int? NaturalArmour, int? Deflection, int? Misc) : this()
            {
                this.Armour = Armour;
                this.Shield = Shield;
                this.NaturalArmour = NaturalArmour;
                this.Deflection = Deflection;
                this.Misc = Misc;
            }

            //Constructor to add a character. Should be done in the DataService for GetCharacter.
            /// <summary>
            /// Construtor that allows the ArmourClass to get information dirctly from the character.
            /// Useful for automatically calculating dexterity as part of the total.
            /// </summary>
            /// <param name="character">The character you want to add the armour class for</param>
            public ArmourClass(ArmourClass AC, Character character) : this(AC.Armour, AC.Shield, AC.NaturalArmour, AC.Deflection, AC.Misc)
            {
                this.character = character;
            }



            private Character character;

            public int? Total
            {
                get
                {
                    int result = 10;
                    result += (Armour == null) ? 0 : (int)Armour;
                    result += (Dex == null) ? 0 : (int)Dex;
                    result += (Shield == null) ? 0 : (int)Shield;
                    result += (Size == null) ? 0 : (int)Size;
                    result += (NaturalArmour == null) ? 0 : (int)NaturalArmour;
                    result += (Deflection == null) ? 0 : (int)Deflection;
                    result += (Misc == null) ? 0 : (int)Misc;
                    return result;
                    //return 10 + Armour + Dex + Shield + Size + NaturalArmour + Deflection + Misc;
                }
            }

            public int? Touch
            {
                get
                {
                    int result = 10;
                    result += (Dex == null) ? 0 : (int)Dex;
                    result += (Size == null) ? 0 : (int)Size;
                    result += (Deflection == null) ? 0 : (int)Deflection;
                    result += (TouchMisc == null) ? 0 : (int)TouchMisc;
                    return result;

                    //return 10 + Dex + Size + Deflection + TouchMisc;
                }
            }

            public int? FlatFooted
            {
                get
                {
                    int result = 10;
                    result += (Armour == null) ? 0 : (int)Armour;
                    result += (Shield == null) ? 0 : (int)Shield;
                    result += (Size == null) ? 0 : (int)Size;
                    result += (NaturalArmour == null) ? 0 : (int)NaturalArmour;
                    result += (Deflection == null) ? 0 : (int)Deflection;
                    result += (FlatFootedMisc == null) ? 0 : (int)FlatFootedMisc;
                    return result;

                    //return 10 + Armour + Shield + Size + NaturalArmour + Deflection + FlatFootedMisc;
                }
            }

            public int? Armour { get; set; }
            public int? Shield { get; set; }
            public int? Dex { 
                get 
                {
                    if (character == null)
                        return null;

                    return character.Dexterity.Modifier; 
                } 
            }

            public int? Size { 
                get 
                {
                    if (character == null)
                        return null;

                    return GetSizeDefensiveModifier(character.Size); 
                } 
            }

            public int? NaturalArmour { get; set; }
            public int? Deflection { get; set; }
            public int? Misc { get; set; }

            public int? TouchMisc { get; set; }
            public int? FlatFootedMisc { get; set; }

            public string Note { get; set; }

        }


        public class Save
        {
            //Empty constructor, so the Mapping works
            private Save() //, string Note)
            {

            }
            public Save(int? Magic, int? Temporary, int? Misc, string Note) :this(){
                this.Magic = Magic;
                this.Temporary = Temporary;
                this.Misc = Misc;
                this.Note = Note;
                ability = null;
            }
            /// <summary>
            /// The ability, where the required modifier is in.
            /// Fortitude = Constitution,
            /// Reflex = Dexterity,
            /// Will = Wisdom
            /// </summary>
            /// <param name="ability"></param>
            public Save(Save save, Ability ability)  : this()//Add base from class levels
            {
                this.ability = ability;
                Magic = save.Magic;
                Temporary = save.Temporary;
                Misc = save.Misc;
                Note = save.Note;
                
            }

            private Ability ability;

            public int? Total {
                get 
                { 
                    int result = (Base == null) ? 0 : (int)Base;
                    result += (Ability == null) ? 0 : (int)Ability;
                    result += (Magic == null) ? 0 : (int)Magic;
                    result += (Temporary == null) ? 0 : (int)Temporary;
                    result += (Misc == null) ? 0 : (int)Misc;

                    return result;

                    //return Base + Ability + Magic + Temporary + Misc;
                }
            }

            public int? Base { get
                {
                    return null;
                    //For each class a character has, add the base stat
                } 
            }

            public int? Ability { get 
                {
                    if (ability == null)
                        return null;

                    int? abilityMod = (ability == null) ? null : ability.Modifier;
                    return abilityMod;
                } 
            }

            public int? Magic { get; set; }
            public int? Temporary { get; set; }
            public int? Misc { get; set; }
            
            public string Note { get; set; }
        }

    }





    public class CombatManeuverBonus 
    {
        private CombatManeuverBonus(){}
        
        public CombatManeuverBonus(int? Misc, int? Temp, string Note)
        {
            this.Misc = Misc;//(Misc == null) ? -100 : Misc;
            this.Temp = Temp;//(Temp == null) ? -100 : Temp;
            this.Note = Note;
        }

        public CombatManeuverBonus(CombatManeuverBonus CMB, Character character) : this(CMB.Misc, CMB.Temp, CMB.Note)
        {
            this.character = character;
        }

        public Character character;

        public int? Total
        { 
            get
            {
                int result = (BaseAttackBonus == null) ? 0 : (int)BaseAttackBonus;
                result += (Strength == null) ? 0 : (int)Strength;
                result += (Size == null) ? 0 : (int)Size;
                result += (Misc == null) ? 0 : (int)Misc;
                return result;
                
                //return BaseAttackBonus + Strength + Size + Misc;
            } 
        }
        public int? BaseAttackBonus {
            get
            {
                if (character == null)
                    return null;

                return (character.BaseAttackBonus == null) ? null : character.BaseAttackBonus;
            }
        }

        public int? Strength {
            get
            {
                if (character == null)
                    return null;

                return (character.Strength.Modifier == null) ? null : character.Strength.Modifier; 
            }
        }

        public int? Size {
            get
            {
                if (character == null)
                    return null;
                return Character.GetSizeOffensiveModifier(character.Size);
            }
        }

        public int? Misc { get; set; }
        public int? Temp { get; set; }
        public string Note { get; set; }
    }

    public class CombatManeuverDefence
    {
        private CombatManeuverDefence() { }

        public CombatManeuverDefence(int? Misc, int? Temp, string Note)
        {
            this.Misc = Misc;//(Misc == null) ? -100 : Misc;
            this.Temp = Temp;//(Temp == null) ? -100 : Temp;
            this.Note = Note;
        }

        public CombatManeuverDefence(CombatManeuverDefence CMD, Character character) : this(CMD.Misc, CMD.Temp, CMD.Note)
        {
            this.character = character;
        }
        public Character character;
        

        public int? Total
        {
            get
            {
                int result = 10;
                result += (BaseAttackBonus == null) ? 0 : (int)BaseAttackBonus;
                result += (Strength == null) ? 0 : (int)Strength;
                result += (Dexterity == null) ? 0 : (int)Dexterity;
                result += (Size == null) ? 0 : (int)Size;
                result += (Misc == null) ? 0 : (int)Misc;
                return result;

               // return 10 + BaseAttackBonus + Strength + Dexterity + Size + Misc; 
            }
        }
        public int? BaseAttackBonus
        {
            get
            {
                if (character == null)
                    return null;
                return (character.BaseAttackBonus == null) ? null : character.BaseAttackBonus;
            }
        }

        public int? Strength
        {
            get
            {
                if (character == null)
                    return null;
                return (character.Strength.Modifier == null) ? null : character.Strength.Modifier;
            }
        }

        public int? Dexterity
        {
            get
            {
                if (character == null)
                    return null;
                return (character.Dexterity.Modifier == null) ? null : character.Dexterity.Modifier;
            }
        }

        public int? Size
        {
            get
            {
                if (character == null)
                    return null;
                return Character.GetSizeDefensiveModifier(character.Size);
            }
        }


        public int? Misc { get; set; }
        public int? Temp { get; set; }
        public string Note { get; set; }
    }

    public class Speed
    {
        private Speed() { }

        public Speed(Speed speed, Race race) : this(speed.BaseModifier, speed.BaseTempModifier, speed.Armour, speed.Fly, speed.Swim, speed.Climb, speed.Burrow, speed.Temporary)
        {
            this.race = race;

            //BaseModifier = speed.BaseModifier;

        }
        public Speed(int? BaseModifier, int? BaseTempModifier, int? Armour, int? Fly, int? Swim, int? Climb, int? Burrow, int? Temporary) : this()
        {
            this.BaseModifier = (BaseModifier == null) ? -100 : BaseModifier;
            this.BaseTempModifier = (BaseTempModifier == null) ? -100 : BaseTempModifier;
            this.Armour = (Armour == null) ? -100 : Armour;
            this.Fly = (Fly == null) ? -100 : Fly;
            this.Swim = (Swim == null) ? -100 : Swim;
            this.Climb = (Climb == null) ? -100 : Climb;
            this.Burrow = (Burrow == null) ? -100 : Burrow;
            this.Temporary = (Temporary == null) ? -100 : Temporary;
        }
        private Race race;
        public int? Base
        {
            get
            {
                int? result = RacialModifier;
                result += (BaseModifier == null) ? 0 : BaseModifier;
                result += (BaseTempModifier == null) ? 0 : BaseTempModifier;
                return  result;
            } 
        }
        public int? RacialModifier { get {
                if (race == null)
                    return null;
                return (race == null) ? null : (int?)race.Speed; } }
        public int? BaseModifier { get; set; }
        public int? BaseTempModifier { get; set; }

        public int? Armour { get; set; }
        public int? Fly { get; set; }
        public int? Swim { get; set; }
        public int? Climb { get; set; }
        public int? Burrow { get; set; }
        public int? Temporary { get; set; }
    }

    public class HitPoints
    {
        public HitPoints() { }
        public HitPoints(int? CurrentHitPoints, int? MaxHitPoints, int? NonLethalDamage, string Wounds) : this()
        {
            
           this.CurrentHitPoints = CurrentHitPoints;
           this.MaxHitPoints = MaxHitPoints;
           this.NonLethalDamage = NonLethalDamage;
           this.Wounds = Wounds;
        }

        public int? CurrentHitPoints { get; set; }
        public int? MaxHitPoints { get; set; }
        public int? NonLethalDamage { get; set; }
        public string Wounds { get; set; }
    }

    [ComplexType]
    public class Ability
    {
        private Ability()
        {
            //CanEditRacial = true;
        }

        public Ability(int? BaseScore, int? TempScore, int? RacialModifier)
        {
            this.BaseScore = BaseScore;
            this.TempScore = TempScore;
            this.RacialModifier = RacialModifier;
        }

        public Ability(Ability ability, Race race) :this(ability.BaseScore, ability.TempScore, ability.RacialModifier) //: this(ability.BaseScore, ability.TempScore)
        {
            if (race == null)
            {
                RacialModifier = null;
                return;
            }

               // CanEditRacial = false;
                //RacialModifier = race.;
              //  return;
            
            
            
            
        }

        public int? BaseScore { get; set; }

        public bool CanEditRacial;
        
        public int? RacialModifier {
            get;
            set;
        }

        /// <summary>
        /// The total score, consisting of the base + the racial
        /// </summary>
        public int TotalScore
        {
            get
            {
                int score = (BaseScore == null) ? 0 : (int)BaseScore;
                score += (RacialModifier == null) ? 0 : (int)RacialModifier;
                return score;
            }
        }
        /// <summary>
        /// Gets the base modifier.
        /// For the current modifer, consider calling Modifier instead.
        /// </summary>
        public int? BaseModifier
        {
            get
            {
                if (BaseScore == null)
                    return null;

                float score = (BaseScore == null) ? 0 : (int)BaseScore;
                score += (RacialModifier == null) ? 0: (int)RacialModifier;

                int result = (int)MathF.Floor((score - 10) / 2);

                return result;

               // return (score - 10) / 2;
            }
        }

        public int? TempScore { get; set; }

        /// <summary>
        /// Gets the temporary modifier.
        /// For the current modifer, consider calling Modifier instead
        /// </summary>
        public int? TempModifier
        {

            get {
                if (TempScore == null)
                    return null;

                float score = (TempScore == null) ? 0 : (int)TempScore;
                score += (RacialModifier == null) ? 0 : (int)RacialModifier;

                int result = (int)MathF.Floor((score -10)/2);

                return result;
            }
        }

        /// <summary>
        /// Gets the current modifer. Uses the normal modifier by default, but if a temporary score is in place, the temporary modifier will be used.
        /// </summary>
        public int? Modifier
        {
            get
            {
                if (TempModifier != null)
                {
                    return TempModifier;
                }

                return (BaseModifier == null) ? null : BaseModifier;
            }    
        }

    }

    public class Skills
    {
        public Skills(Ability ability)
        {
            _ablity = ability;
            //Set skillname to the name of the variable
        }

        public Skills(Ability ability, string skillName)
        {
            _ablity = ability;
            SkillName = skillName;
        }


        private Ability _ablity;

        public string SkillName;

        public int? Total
        {
            get
            {
                int? total = Ranks + AbilityModifier + Misc + Racial;
                //Adds the class skill bonus, if there is at least 1 rank in the skill
                if (IsClassSkill && Ranks >= 1)
                {
                    total += 3;
                }

                return total;
            }
        }

        public bool IsClassSkill { get; set; }

        public int? Ranks { get; set; }

        public int? Misc { get; set; }

        public int? Racial { get; set; }

        public string AbilityName { get { return nameof(_ablity); } }
        public int? AbilityModifier
        {
            get { return _ablity.Modifier; }
        }
    }
}
