using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Class
    {
        public string Name { get; set; }
        public int level { get; set; }

        public int HitDie { get; set; }
        public int SkillsPerLevel { get; set; }
        public string ClassSkills { get; set; }
        public string WeaponProficiency {get;set;}
        public string ArmourProficiency { get; set; }

        public List<int> SpecialAbilityIds { get; set; }
        public List<SpecialAbility> SpecialAbilities { get; set; }
    }
}
