using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Feat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public List<string> Prerequisites { get; set; }
        
        //Should probably be a list of feats
        public List<string> PrerequisiteFeats { get; set; }


        public string Benefit { get; set; }
        public string Normal { get; set; }
        public string Special { get; set; }

        public string FullText { get; set; }
        public bool Multiples { get; set; }
        public List<string> PrerequisiteSkills { get; set; }
        
        public List<string> RaceNames { get; set; }
        //public List<Race> Races { get; set; }
        public string Source { get; set; }

    }
}
