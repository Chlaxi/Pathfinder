using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Spell
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        //String since it doesn't refers directly to a class, but rather is a description       
        //public string Class { get; set; }
        public string Description { get; set; }

        public string School { get; set; }
        public string SubSchool { get; set; }
        public string Element { get; set; }
        public string SpellLevel { get; set; }
        public string CastingTime { get; set; }
        public string Components { get; set; }
        public string Range { get; set; }
        public string Target { get; set; }
        public string Area { get; set; }
        public string Effect { get; set; }
        public string Duration { get; set; }
        public string SavingThrow { get; set; }
        public string SpellResistance { get; set; }
        public string Domain { get; set; }
        public string Bloodline { get; set; }
        public string Patron { get; set; }

        public bool Dismissible { get; set; }
        public bool Shapeable { get; set; }
        public bool Verbal { get; set; }
        public bool Somatic { get; set; }
        public bool Material { get; set; }
        public bool Focus { get; set; }
        public bool DivineFocus { get; set; }
        public bool CostlyComponent { get; set; }
        public int MaterialCost { get; set; }

        public string DescriptionFormatted { get; set; }
        public string FullText { get; set; }

        public string Source { get; set; }

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
