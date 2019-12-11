using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;

namespace WebService
{
    public class CharacterUpdateDTO
    {
        public string Name { get; set; }
        //Set the value of the alignment
        public Alignment Alignment { get; set; }
        //public Class {get;set;}
        public string Gender { get; set; }
        public string Age { get; set; }

        public string Deity { get; set; }
        public string Homeland { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Hair { get; set; }
        public string Eyes { get; set; }
        public string Experience { get; set; }
        public AbilityDTO Strength { get; set; }
        public AbilityDTO Dexterity { get; set; }
        public AbilityDTO Constitution { get; set; }
        public AbilityDTO Intelligence { get; set; }
        public AbilityDTO Wisdom { get; set; }
        public AbilityDTO Charisma { get; set; }

    }

    public class AbilityDTO
    {
        private AbilityDTO() { }
        public AbilityDTO(int? baseScore, int? tempScore) : this()//, int? racialScore)
        {
            BaseScore = (baseScore == null) ? -100 : baseScore;
            TempScore = (tempScore == null) ? -100 : tempScore;
     //       RacialModifier = (racialScore == null) ? -100 : racialScore;
            

        }
        public int? BaseScore
        {
            get
            {
                return baseScore;
            }
            set
            {
                if (value.Equals("")) {
                    Console.WriteLine("--------The ability DTO had a null value");
                    value = null; }
                baseScore = (value == null) ? -100 : value;
            }
        }

        private int? baseScore;
        public int? TempScore { get; set; }
        private int? tempScore = 0;
        public int? RacialModifier { get; set; }
    }
}
