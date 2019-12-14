using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Spellbook
    {
        public SpellLevel[] SpellLevels { get; set; }
    }

    public class SpellLevel
    {
        public SpellLevel(int Level)
        {
            this.Level = Level;
            //TODO: Get spells per day from the chracter
            SpellsKnown = 0;
            SpellsPerDay = 0; //Get from character
            Spells = new List<KnownSpell>(); 
                    }
   
        public int SpellDC { get
            {
                return 0;
            }
        }
        public int Level { get; set; }
        public int SpellsKnown { get; set; }
        public int SpellsPerDay { get; set; }
        public int? SpellsPrepared { get; set; }
        public List<KnownSpell> Spells { get; set; }
    }

    public class KnownSpell
    {
        public int CharacterId { get; set; }
        public int SpellId { get; set; }
        public int SpellLevel { get; set; }
        public Spell Spell {get;set;}
        public int? Prepared { get; set; }
        public string Note { get; set; }


    }
}
