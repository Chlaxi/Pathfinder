using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DataService
{
    public class DataService : IDataService
    {
        public Player GetPlayerByUsername(string username)
        {
            using var db = new PathfinderContext();

            var query = from player in db.Players
                        where player.Username.Equals(username)
                        select player;

            if (query.Count() == 0) return null;
            return GetPlayer(query.First().Id);            
        }
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

        /// <summary>
        /// Returns the player id from a character.
        /// Used for verification
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns>The playerId that belongs to the character</returns>
        public int? GetPlayerFromCharId(int characterId)
        {
            using var db = new PathfinderContext();
            Character character = db.Characters.Find(characterId);
            if (character == null) return null;

            return character.PlayerId;
        }

        public Player CreatePlayer(string username, string password, string salt)
        {
            using var db = new PathfinderContext();
            //Username check is made in the webservice
            Player player = new Player();
            if (db.Players.Count() == 0) player.Id = 1;
            player.Id = db.Players.Max(x => x.Id) + 1;
            player.Username = username;
            player.Password = password;
            player.Salt = salt;

            Console.WriteLine("Creating a new player, {0}, with id {1}", player.Username, player.Id);


            db.Players.Add(player);    
            db.SaveChanges();
            return player;
        }

        public Character GetCharacter(int id)
        {
            using var db = new PathfinderContext();
            Character character = db.Characters.Find(id);
            if (character == null) return null;

            Race race = null;
            // character.Player = GetPlayer(character.PlayerId);


            race = GetRace(character.RaceName);

            character.Class = FindCharacterClasses(id);
            

            character.Strength =      new Ability(race);
            character.Dexterity =     new Ability(race);
            character.Constitution =  new Ability(race);
            character.Intelligence =  new Ability(race);
            character.Wisdom =        new Ability(race);
            character.Charisma =      new Ability(race);
            

            character.AC = new Character.ArmourClass(character);
            character.Fortitude = new Character.Save(character.Fortitude, character.Constitution);
            character.Reflex = new Character.Save(character.Reflex, character.Dexterity);
            character.Will = new Character.Save(character.Will, character.Wisdom);
            character.CMB = new CombatManeuverBonus(character);
            character.CMD = new CombatManeuverDefence(character);

            character.Speed = new Speed(character.Speed, character.Race);
            character.Spellbook = GetSpellBook(character);
            //CharacterClasses relation to set character's classes
            character.Feats = GetCharacterFeats(character.Id);
            

            return character;
        }

        public Character CreateCharacter (int playerId, string characterName="Unknown")
        {
            using var db = new PathfinderContext();
            Player player = GetPlayer(playerId);
            if (player == null) return null;

            Character character = new Character()
            {
                //Player = player,
                PlayerId = playerId,
                Name = characterName,
                Id = db.Characters.Max(x => x.Id) + 1,
                Race = null,
                Class = new List<CharacterClasses>()
            };
            //TODO Use the function in sql?

            Console.WriteLine("Creating a new Character owned by player {0}.", player.Username);

            db.Characters.Add(character);

            db.SaveChanges();
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

        public Spellbook GetSpellBook(Character character)
        {
            using var db = new PathfinderContext();
            Console.WriteLine("------Getting Spellbook for the character {0}, who is a {1}", character.Id, character.Class.ToString());

            var query = from spells in db.KnownSpells
                        where spells.CharacterId.Equals(character.Id)
                        select spells;

      
            if(query.Count() == 0)
            {
                Console.WriteLine("No spells found for this character");
                return null;
            }
            Console.WriteLine("Found {0} spells for the {1}",query.Count(), character.Name);
            Spellbook spellbook = new Spellbook();
            spellbook.SpellLevels = new SpellLevel[10];
            for (int i = 0; i < spellbook.SpellLevels.Length; i++)
            {
                spellbook.SpellLevels[i] = new SpellLevel();
            }
            
            Console.WriteLine("----Adding spells to {0}", character.Name);
            foreach (var spell in query)
            {
                Console.WriteLine("      -Trying spell {0} at level {1}", spell.SpellId, spell.SpellLevel);
                spell.Spell = GetSpell(spell.SpellId);

                if (spell.Spell == null)
                {
                    //This part is redundant due to database constraints, but adding as an extra layer of security.
                    Console.WriteLine("       For some reason, the spell was not found.");
                    continue;
                }

                Console.WriteLine("       Spell found. Spell is called {0}", spell.Spell.Name);

                var currentSpellLevel = spellbook.SpellLevels[spell.SpellLevel];

                currentSpellLevel.Spells.Add(spell);
                currentSpellLevel.SpellsKnown++;
                if (spell.Prepared != null)
                    currentSpellLevel.SpellsPrepared += spell.Prepared;
                Console.WriteLine("***Spell {0} added to the spellbook at level {1}", spell.Spell.Name, spell.SpellLevel);
            }

            return spellbook;
        }

        /// <summary>
        /// Finds the index in which a spell is stored in the spellbook based on the spellId
        /// </summary>
        /// <param name="character"></param>
        /// <param name="spellLevel">The spell level you want to find the spell in</param>
        /// <param name="spellIndex">The index of the spell</param>
        /// <returns>The index of the spell</returns>
        public KnownSpell GetSpellFromSpellbook(Character character, int spellLevel, int spellIndex)
        {
            Spellbook spellbook = GetSpellBook(character);
            if (spellLevel > spellbook.SpellLevels.Length || spellLevel < 0)
                return null;
            SpellLevel curLevel = spellbook.SpellLevels[spellLevel];

            if (spellIndex >= curLevel.Spells.Count || spellIndex < 0)
                return null;

            KnownSpell spell = curLevel.Spells[spellIndex];
            if (spell == null) return null;

            return spell;
        }

        /// <summary>
        /// Adds a spell to the character spell relation.
        /// </summary>
        /// <param name="character">The character that will learn the spell</param>
        /// <param name="spellId">The ID of the spell to be taught</param>
        /// <param name="spellLevel">The level the spell is taught at</param>
        /// <returns></returns>
        public KnownSpell AddSpellToCharacter(Character character, int spellId, int spellLevel) //, string note=null)
        {
            using var db = new PathfinderContext();

            Console.WriteLine("--------Trying to add a spell to the character {0}", character.Name);

            //TODO Authorisation


            var spell = GetSpell(spellId);
            if (spell == null)
            {
                Console.WriteLine("Error: Spell not Found");
                return null;
            }
            Console.WriteLine("spell found! Trying to add {0}", spell.Name);
            
            var query = from knownSpell in db.KnownSpells
                        where knownSpell.CharacterId.Equals(character.Id) && knownSpell.SpellId.Equals(spellId)
                        select knownSpell;

           // Console.WriteLine("The query gave the following result for spell: {0}, level {}", query.FirstOrDefault().SpellId, query.FirstOrDefault().SpellLevel);

            if (query.Count() != 0) {
                Console.WriteLine("Character already knows this spell!");
                //Change spell level?
                return null;
            }
            Console.WriteLine("Spell is not already in use! Adding to character");

            KnownSpell newSpell = new KnownSpell()
            {
                CharacterId = character.Id,
                SpellId = spell.Id,
                Spell = spell,
                SpellLevel = spellLevel,
                Prepared = null,
                Note = null
            };

            db.KnownSpells.Add(newSpell);
            db.SaveChanges();
            return newSpell;
        }

        public bool RemoveSpellFromCharacter(Character character, int spellLevel, int spellIndex)
        {
            using var db = new PathfinderContext();
            
            KnownSpell spell = GetSpellFromSpellbook(character, spellLevel, spellIndex);
            if (spell == null)
                return false;
            

            Console.WriteLine("--------Trying to remove a spell to the character {0}", character.Name);

            //TODO Authorisation

   /*         var query = from knownSpell in db.KnownSpells
                        where knownSpell.CharacterId.Equals(character.Id) && knownSpell.SpellId.Equals(spellId) && knownSpell.SpellLevel.Equals(spellLevel)
                        select knownSpell;
        
            if(query.First() == null)
            {
                Console.WriteLine("No spell found for that character.");
                return false;
            }*/

            db.KnownSpells.Remove(spell);
            db.SaveChanges();
            return true;
        
        }

        public List<SpellSearchResult> SpellSearch(string query)
        {
            using var db = new PathfinderContext();

            List<SpellSearchResult> spells = new List<SpellSearchResult>();
            foreach (var result in db.SpellSearchResults.FromSqlRaw("select * from search_spells({0})", query))
            {
                //TODO use spell query DTO
                var spell = new SpellSearchResult()
                {
                    SpellId = result.SpellId,
                    Name = result.Name, 
                    ShortDescription = result.ShortDescription,
                };
                spells.Add(spell);
            }

            return spells;
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
