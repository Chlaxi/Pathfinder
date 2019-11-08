using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataService
{
    public class DataService : IDataService
    {
       
        //Spells
        public List<Spell> GetSpells()
        {
            using var db = new PathfinderContext();
            List<Spell> spells = new List<Spell>();

            foreach(var spell in db.Spells)
            {
                //Use get spell instead
                spells.Add(GetSpell(spell.Id));
            }

            return spells;
        }

        public Spell GetSpell(int id)
        {
            using var db = new PathfinderContext();

            Spell spell = db.Spells.Find(id);
            if (spell == null) return null;

            return spell;
        }



        //Feats
        public List<Feat> GetFeats()
        {
            using var db = new PathfinderContext();
            List<Feat> feats = new List<Feat>();

            foreach (var feat in db.Feats)
            {
                //Use get spell instead
                feats.Add(GetFeat(feat.Id));
            }

            return feats;
        }

        public Feat GetFeat(int id)
        {
            using var db = new PathfinderContext();

            Feat feat = db.Feats.Find(id);
            if (feat == null) return null;

            return feat;
        }



        //Special Ability
        public List<SpecialAbility> GetSpecialAbilities()
        {
            using var db = new PathfinderContext();
            List<SpecialAbility> specailAbilities = new List<SpecialAbility>();

            foreach (var sa in db.SpecialAbilities)
            {
                specailAbilities.Add(GetSpecialAbility(sa.Id));
            }

            return specailAbilities;
        }

        public SpecialAbility GetSpecialAbility(int id)
        {
            using var db = new PathfinderContext();

            SpecialAbility SA = db.SpecialAbilities.Find(id);
            if (SA == null) return null;

            return SA;
        }


        //Race
        public List<Race> GetRaces()
        {
            using var db = new PathfinderContext();
            List<Race> races = new List<Race>();

            foreach (var race in db.Races)
            {
                //Use get spell instead
                races.Add(GetRace(race.Name));
            }

            return races;
        }


        public Race GetRace(string name)
        {
            using var db = new PathfinderContext();

            Race race = db.Races.Find(name);
            if (race == null) return null;

            return race;
        }



        //Classes
        public List<Class> GetClasses()
        {
            using var db = new PathfinderContext();
            List<Class> classes = new List<Class>();

            foreach (var c in db.Classes)
            {
                classes.Add(GetClass(c.Name));
            }

            return classes;
        }


        public Class GetClass(string name)
        {
            using var db = new PathfinderContext();

            Class _class = db.Classes.Find(name);
            if (_class == null) return null;

            return _class;
        }


    }
}
