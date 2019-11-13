using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class ClassInfo
    {
        public string ClassName { get; set; }
        public int Level { get; set; }
        public List<int> BaseAttackBonus { get; set; }
        public int? BaseFortitude { get; set; }
        public int? BaseReflex { get; set; }
        public int? BaseWill { get; set; }
        
        public List<string> Specials { get; set; }

        //Spell levels
        
    }
}
