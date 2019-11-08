using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Spell
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        //String since it doesn't refers directly to a class, but rather is a description       
        public string Class { get; set; }
        public string Description { get; set; }

        public string School { get; set; }
        public string SubSchool { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public string Area { get; set; }


        //Class checks
        public int? Bard { get; }
        public int? Cleric { get; }
        public int? Druid { get; }
        public int? Paladin { get; }
        public int? Ranger { get; }
        public int? Sorcerer { get; }
        public int? Wizard { get; }



        public string GetClassDescription()
        {
            string result = "";
            if (Bard != null) result += "Bard " + Bard;
            if (Cleric != null) result += "Cleric " + Cleric;
            if (Druid != null) result += "Druid " + Druid;
            if (Paladin != null) result += "Paladin " + Paladin;
            if (Ranger != null) result += "Ranger " + Ranger;
           
            if((Sorcerer!= null && Wizard != null) && Sorcerer == Wizard)
            {
                result += "Sorcerer/Wizard" + Sorcerer;
            }
            else
            {
                if (Sorcerer != null) result += "Sorcerer " + Sorcerer;
                if (Wizard != null) result += "Wizard " + Wizard;
            }

            return result;
        }

    }
}
