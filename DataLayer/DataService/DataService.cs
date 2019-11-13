using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DataService
{
    public class DataService : IDataService
    {
        public Player GetPlayer(int id)
        {
            using var db = new PathfinderContext();
            Player player = db.Players.Find(id);
            if (player == null) return null;

            var characters = GetCharactersByPlayer(id);
            player.Characters = characters;
            return player;
        }

        public Player GetSimplePlayer(int id)
        {
            using var db = new PathfinderContext();
            Player player = db.Players.Find(id);
            if (player == null) return null;

            return player;
        }

        public Player CreatePlayer(string username)
        {
            using var db = new PathfinderContext();
            Player player = new Player();
            player.Id = db.Players.Max(x => x.Id) + 1;
            player.Username = username;

            Console.WriteLine("Creating a new player, {0}, with id {1}", player.Username, player.Id);

            //add here?
            //var i = db.Players.Add(player);
            //            db.PlayerCharacterRelation.Add().
            
            //db.SaveChanges;
            return player;
        }

        public Character GetCharacter(int id)
        {
            using var db = new PathfinderContext();
            Character character = db.Characters.Find(id);
            if (character == null) return null;

           // character.Player = GetPlayer(character.PlayerId);
            character.AC = new Character.ArmourClass(character);
            character.Fortitude = new Character.Save(character.Constitution);
            character.Reflex = new Character.Save(character.Dexterity);
            character.Will = new Character.Save(character.Wisdom);
            character.CMB = new CombatManeuverBonus(character);
            character.CMD = new CombatManeuverDefence(character);
          //  character.Race = GetRace(character.RaceName);

            //CharacterClasses relation to set character's classes

            character.Class = null;

            return character;
        }

        public Character CreateCharacter (int playerId)
        {
            using var db = new PathfinderContext();
            Player player = GetPlayer(playerId);
            if (player == null) return null;

            Character character = new Character()
            {
                //Player = player,
                PlayerId = playerId,
                Id = db.Characters.Max(x => x.Id) + 1
            };
            //TODO Use the function in sql?

            Console.WriteLine("Creating a new Character owned by player {0}.", player.Username);

            //Add to character
            //add to player character relation
            //db.SaveChanges();
            return character;
        }

        public List<Character> GetCharactersByPlayer(int playerId)
        {
            Console.WriteLine("------------\n Getting characters from player");
            using var db = new PathfinderContext();
            List <Character> characters = new List<Character>();
            int i = 0;
            foreach(var character in db.Characters)
            {
                if (character.PlayerId.Equals(playerId))
                {
                    Console.WriteLine("Character found");
                    characters.Add(GetCharacter(character.Id));
                    i++;
                    Console.WriteLine("Character Added");
                }
            }
            Console.WriteLine("{0} characters added to the player",i);
            foreach(var c in characters)
            {
                Console.WriteLine("* {0} ({1})",c.Name, c.Id);
            }
            Console.WriteLine("-----------");
            return characters;
        }


       
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
