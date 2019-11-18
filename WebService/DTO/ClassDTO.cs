using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;

namespace WebService
{
    public class ClassSimpleDTO
    {
        public ClassSimpleDTO(CharacterClasses _class)
        {
            ClassLink = String.Format("/api/characters/{0}/classes/{1}", _class.CharacterId, _class.ClassName);
            Name = _class.ClassName;
            level = _class.Level;

        }

        public string ClassLink { get; set; }
        public string Name { get; set; }
        public int level { get; set; }
    }

    public class ClassDTO
    {
        //public string ClassLink { get; set; }
        public string Name { get; set; }
        public int level { get; set; }
        public int SkillsPerLevel { get; set; }
        public int HitDie { get; set; }
        public string[] ClassSkills { get; set; }
        public string[] ArmourProficiency { get; set; }
        public string[] WeaponProficiency { get; set; }
        public List<int> BaseAttackBonus { get; set; }
        public int? BaseFortitude { get; set; }
        public int? BaseReflex { get; set; }
        public int? BaseWill { get; set; }
        public List<string> Specials {get;set;}

        public ClassDTO(Class _class, int level)
        {
            //ClassLink = String.Format("/api/characters/{0}/classes/{1}", _class.CharacterId,_class.ClassName);
            Name = _class.Name;
            this.level = level;
            SkillsPerLevel = _class.SkillsPerLevel;
            HitDie = _class.HitDie;
            ClassSkills = _class.ClassSkills;
            ArmourProficiency = _class.ArmourProficiency;
            WeaponProficiency = _class.WeaponProficiency;
            Specials = new List<string>();
            foreach(var info in _class.LevelInfo)
            {
                if(info.Specials != null)
                    Specials.AddRange(info.Specials);

                if (info.Level == level) {
                    BaseAttackBonus = info.BaseAttackBonus;
                    BaseFortitude = info.BaseFortitude;
                    BaseReflex = info.BaseReflex;
                    BaseWill = info.BaseWill;
                }
            }
            

            
        }
    }
}
