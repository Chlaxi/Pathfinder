using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{

    public enum AbilityEnum { Str, Dex, Con, Int, Wis, Cha }
    public enum Size { small, medium, large}

    public enum Alignment { LawfulGood, LawfulNeutral, LawfulEvil
                            ,NeutralGood, Neutral, NeutralEvil
                            ,ChaoticGood, ChaoticNeutral, ChaoticEvil}
    public class Character
    {
        public Player Player { get; set; }
        public int Id { get; set; }

        public Race Race { get; set; }
        public List<Class> Class{ get;set;}

        public string Name { get; set; }
        public Alignment Alignment { get; set; }

        public string Gender { get; set; }
        public string Age { get; set; }

        public string Diety { get; set; }
        public string Homeland { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Hair { get; set; }
        public string Eyes { get; set; }


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

        public List<Feat> Feats { get; set; }
        public List<SpecialAbility> SpecialAbilities {get; set;}

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

        public HitPoints HitPoints { get; set; }

        public Speed Speed { get; set; }

        public int? Initiative { get; set; }
        public int? InitiativeMiscModifier { get; set; }

        public int? BaseAttackBonus { get; }

        public Size Size { get; set; }
        
        public CombatManeuverBonus CMB { get; set; }
        public CombatManeuverDefence CMD { get; set; }

        public int? SpellResistance { get; set; }
        public string DamageReduction { get; set; }
    

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
            }
            return null;
        }

        public int? GetSizeDefensiveModifier(Size size)
        {
            switch (size)
            {
                case Size.small:
                    return 1;
                case Size.large:
                    return -1;

                default:
                    return 0;
            }
        }

        public int? GetSizeOffensiveModifier(Size size)
        {
            switch (size)
            {
                case Size.small:
                    return -1;
                case Size.large:
                    return 1;

                default:
                    return 0;
            }
        }


        public class Save
        {
            public int? Total { get { return Base + Ability + Magic + Temporary + Misc; } }

            public int? Base { get
                {
                    return null;
                    //For each class a character has, add the base stat
                } 
            }

            public int? Ability { get 
                {
                    //Get the relevant ability modifer from the character
                    return null;
                } 
            }

            public int? Magic { get; set; }
            public int? Temporary { get; set; }
            public int? Misc { get; set; }
            
            public string Note { get; set; }
        }

    }


    public class ArmourClass
    { 
        public ArmourClass(Character character)
        {
            this.character = character;
        }

        public Character character;

        public int? Total { get 
            {
                return 10 + Armour + Dex + Shield + Size + NaturalArmour + Deflection + Misc;
            } 
        }

        public int? Touch
        {
            get
            {
                return 10  + Dex + Size + Deflection + TouchMisc;
            }
        }

        public int? FlatFooted
        {
            get
            {
                return 10 + Armour + Shield + Size + NaturalArmour + Deflection + FlatFootedMisc;
            }
        }

        public int? Armour { get; set; }
        public int? Shield { get; set; }
        public int? Dex { get { return character.Dexterity.Modifier;  } }
    
        public int? Size { get { return character.GetSizeDefensiveModifier(character.Size);  } }

        public int? NaturalArmour { get; set; }
        public int? Deflection { get; set; }
        public int? Misc { get; set; }

        public int? TouchMisc { get; set; }
        public int? FlatFootedMisc { get; set; }

        public string Note { get; set; }

    }


    public class CombatManeuverBonus 
    { 
        public CombatManeuverBonus(Character character)
        {
            this.character = character;
        }

        public Character character;

        public int? total
        { 
            get
            { return BaseAttackBonus + Strength + Size + Misc; } 
        }
        public int? BaseAttackBonus { get { return character.BaseAttackBonus; } }
        public int? Strength { get { return character.Strength.Modifier;  } }
        public int? Size { get { return character.GetSizeOffensiveModifier(character.Size); } }

        public int? Misc { get; set; }
    }

    public class CombatManeuverDefence
    {
        public CombatManeuverDefence(Character character)
        {
            this.character = character;
        }

        public Character character;

        public int? total
        {
            get
            { return 10 + BaseAttackBonus + Strength + Dexterity + Size + Misc; }
        }
        public int? BaseAttackBonus { get { return character.BaseAttackBonus; } }
        public int? Strength { get { return character.Strength.Modifier; } }
        public int? Dexterity { get { return character.Dexterity.Modifier; } }
        public int? Size { get { return character.GetSizeDefensiveModifier(character.Size); } }

        public int? Misc { get; set; }
    }

    public class Speed
    {
        public int? Base { get; set; }
        public int? Armour { get; set; }
        public int? Fly { get; set; }
        public int? Swim { get; set; }
        public int? Climb { get; set; }
        public int? Burrow { get; set; }
        public int? Temporary { get; set; }
    }

    public class HitPoints
    {
        public int? CurrentHitPoints { get; set; }
        public int? MaxHitPoints { get; set; }
        public int? NonLethalDamage { get; set; }
    }

    public class Ability
    {
        public int? BaseScore { get; set; }

        public int? RacialModifier { get; set; }

        public int? BaseModifier
        {
            get { return (BaseScore - 10) / 2; }
        }

        public int? TempScore { get; set; }
        public int? TempModifier { get { return (TempScore - 10) / 2; } }

        /// <summary>
        /// The current modifer. Uses the normal modifier by default, but if a temporary score is in place, the temporary modifier will be used.
        /// </summary>
        public int? Modifier
        {
            get
            {
                if (TempModifier != null)
                {
                    return TempModifier;
                }

                return BaseModifier;
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
