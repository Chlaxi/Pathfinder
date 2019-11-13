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
            character.Race = GetRace(character.RaceName);

            Race race = character.Race;

            character.Class = FindCharacterClasses(id);

            if (race != null)
            {
                character.Strength = new Ability(character.Strength, race, race.Strength);
                character.Dexterity = new Ability(character.Dexterity, race, race.Dexterity);
                character.Constitution = new Ability(character.Constitution, race, race.Constitution);
                character.Intelligence = new Ability(character.Intelligence, race, race.Intelligence);
                character.Wisdom = new Ability(character.Wisdom, race, race.Wisdom);
                character.Charisma = new Ability(character.Charisma, race, race.Charisma);
            }

            character.AC = new Character.ArmourClass(character);
            character.Fortitude = new Character.Save(character.Fortitude, character.Constitution);
            character.Reflex = new Character.Save(character.Reflex, character.Dexterity);
            character.Will = new Character.Save(character.Will, character.Wisdom);
            character.CMB = new CombatManeuverBonus(character);
            character.CMD = new CombatManeuverDefence(character);

            character.Speed = new Speed(character.Speed, character.Race);

            //CharacterClasses relation to set character's classes
            character.Feats = GetCharacterFeats(character.Id);
            

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

        public List<CharacterClasses> FindCharacterClasses(int characterId)
        {
            using var db = new PathfinderContext();

            var query = from classes in db.CharacterClasses
                        where classes.CharacterId == characterId
                        select classes;

            if (query == null) {
                Console.WriteLine("---------------------------\n Character has no classes yet\n------------------");
                return null;
            }

            Console.WriteLine("------------Classes found for the character with {0}",characterId);
            

            List<CharacterClasses> _classes = new List<CharacterClasses>();
            //List<Class> _classes = new List<Class>();
            foreach (var i in query){
                CharacterClasses _class = new CharacterClasses()
                {
                    CharacterId = i.CharacterId,
                    ClassName = i.ClassName,
                    Level = i.Level,
                    Class = GetClass(i.ClassName, i.Level)
                };
                _classes.Add(_class);    
               
                Console.WriteLine("{0}* {1} {2}", i.CharacterId ,i.ClassName, i.Level);
            }

            return _classes;
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

        public List<CharacterFeats> GetCharacterFeats(int characterId)
        {
            using var db = new PathfinderContext();
            var query = from cFeats in db.CharacterFeats
                        where cFeats.CharacterId.Equals(characterId)
                        select cFeats;

            if(query.Count() == 0)
            {
                return null;
            }

            List<CharacterFeats> feats = new List<CharacterFeats>();
            foreach(var feat in query)
            {
                CharacterFeats f = new CharacterFeats();
                f = feat;
                f.Feat = GetFeat(feat.FeatId);

                feats.Add(f);
            }

            return feats;
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


        public Class GetClass(string name, int level=20)
        {
            using var db = new PathfinderContext();

            Class _class = db.Classes.Find(name);
            if (_class == null) return null;

            List<ClassInfo> _classInfo = new List<ClassInfo>(); //GetAllClassInfo(_class.Name);
            for(int i = 1; i <=level; i++)
            {
                _classInfo.Add(GetClassInfoForLevel(_class.Name, i));
            }
            _class.LevelInfo = _classInfo;
            return _class;
        }

        public List<ClassInfo> GetAllClassInfo(string name)
        {
            using var db = new PathfinderContext();

            List<ClassInfo> classInfo = new List<ClassInfo>();

            var query = from info in db.ClassInfo
                        where info.ClassName.Equals(name)
                        select info;
             
            if(query == null)
            {
                Console.WriteLine("----NO RESULT FOR FOR CLASS INFO, WHEN SEARCHING FOR THE CLASS {0}", name);
                return null;
            }

            foreach(var i in query)
            {
                classInfo.Add(GetClassInfoForLevel(name, i.Level));
            }

            return classInfo;
        }
        
        public ClassInfo GetClassInfoForLevel(string name, int level)
        {
            using var db = new PathfinderContext();

            ClassInfo classInfo = db.ClassInfo.Find(name, level);
            if (classInfo == null) return null;

            //TODO Add special abilities to the class info
            return classInfo;

        }
    }
}
