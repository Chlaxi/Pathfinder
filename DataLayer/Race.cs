using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Race
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Size Size { get; set; }
        
        public int? Speed { get; set; } 
       // public List<SpecialAbility> Traits { get; set; }
        
        public List<string> LanguagesKnown { get; set; }
        public List<string> LanguagesAvailable { get; set; }

        public int? Strength { get; set; }
        public int? Dexterity { get; set; }
        public int? Constitution { get; set; }
        public int? Intelligence { get; set; }
        public int? Wisdom { get; set; }
        public int? Charisma { get; set; }

        public List<int> SpecialModifier { get; set; }

    }
}
