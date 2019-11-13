using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class CharacterFeats
    {
        public int CharacterId { get; set; }

        public int FeatId { get; set; }

        public string Choice { get; set; }

        public int? Multiple { get; set; }
        public string Note { get; set; }

        public string FeatName {
            get
            {
                if (Feat == null) return null;
                return Feat.Name;
            }
        }
        public Feat Feat { get; set; }
        
    }
}
