using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Spellbook
    {
        public Character character { get; set; }
        public SpellLevel[] SpellLevels { get; set; }

    }

    public class SpellLevel
    {
        public int? SpellsKnown { get; set; }
        public int? SpellsPerDay { get; set; }
        public List<KnownSpell> Spells { get; set; }
    }

    public class KnownSpell
    {
        public int SpellId { get; set; }
        public Spell Spell {get;set;}
        public bool Prepared { get; set; }
    }
}
