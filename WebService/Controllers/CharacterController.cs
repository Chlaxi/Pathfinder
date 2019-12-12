using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.DataService;
using WebService.Middleware;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebService.Controllers
{
    [Authorize]
    [Route("api/characters")]
    public class CharacterController : Controller
    {

        private DataService ds = new DataService();

        /*
        [HttpGet("{playerid}", Name = nameof(GetCharacters))]
        public ActionResult GetCharacters(int playerid)
        {
            List<Character> characters = ds.GetCharactersByPlayer(playerid);
            if (characters.Count == 0) return NotFound();

            return Ok(characters);
        }
        */
        [Authorize]
        [HttpGet("{characterid}", Name = nameof(GetCharacter))]
        public ActionResult<Character> GetCharacter(int characterid)
        {
            PlayerController playercontroller = new PlayerController();

            Character character = ds.GetCharacter(characterid);
            if (character == null) return NotFound();
            if (!AuthService.AuthorizePlayer(HttpContext, character.PlayerId)) return BadRequest("Wrong player");


            return character;
        }

        [HttpPut("{characterid}")]
        public ActionResult<Character> UpdateCharacter(int characterid, [FromBody] CharacterUpdateDTO update)
        {
            Character character = GetCharacter(characterid).Value;

            if (character == null) return NotFound("No Character with this id");
            //string JSONChar = JsonConvert.SerializeObject(character);

            update.Strength = new AbilityDTO(update.Strength.BaseScore, update.Strength.TempScore);
            update.Dexterity = new AbilityDTO(update.Dexterity.BaseScore, update.Dexterity.TempScore);
            update.Constitution = new AbilityDTO(update.Constitution.BaseScore, update.Constitution.TempScore);
            update.Intelligence = new AbilityDTO(update.Intelligence.BaseScore, update.Intelligence.TempScore);
            update.Wisdom = new AbilityDTO(update.Wisdom.BaseScore, update.Wisdom.TempScore);
            update.Charisma = new AbilityDTO(update.Charisma.BaseScore, update.Charisma.TempScore);
            update.HitPoints = new HealthDTO(update.HitPoints.CurrentHitPoints, update.HitPoints.MaxHitPoints, update.HitPoints.NonLethalDamage, update.HitPoints.Wounds);
      
            Console.WriteLine(update.ToString());
            var _character = new Character()
            {
                Name = update.Name,
                Alignment = update.Alignment,
                Gender = update.Gender,
                Age = update.Age,
                Deity = update.Deity,
                Homeland = update.Homeland,
                Height = update.Height,
                Weight = update.Weight,
                Hair = update.Hair,
                Eyes = update.Eyes,
                Experience = update.Experience,

                InitiativeMiscModifier = update.InitiativeMisc,

                Strength = new Ability(update.Strength.BaseScore,update.Strength.TempScore,update.Strength.RacialModifier),
                Dexterity = new Ability(update.Dexterity.BaseScore, update.Dexterity.TempScore, update.Dexterity.RacialModifier),
                Constitution = new Ability(update.Constitution.BaseScore, update.Constitution.TempScore, update.Constitution.RacialModifier),
                Intelligence = new Ability(update.Intelligence.BaseScore, update.Intelligence.TempScore, update.Intelligence.RacialModifier),
                Wisdom = new Ability(update.Wisdom.BaseScore, update.Wisdom.TempScore, update.Wisdom.RacialModifier),
                Charisma = new Ability(update.Charisma.BaseScore, update.Charisma.TempScore, update.Charisma.RacialModifier),
            
                HitPoints = new HitPoints(update.HitPoints.CurrentHitPoints, update.HitPoints.MaxHitPoints,
                update.HitPoints.NonLethalDamage, update.HitPoints.Wounds)

            };
            ds.UpdateCharacter(characterid, _character);
            return Ok(update);
        }

        [HttpGet("{characterid}/spells", Name = nameof(GetSpellbook))]
        public ActionResult GetSpellbook(int characterid)
        {
            Character character = GetCharacter(characterid).Value;

            if (character == null) return NotFound("No Character with this id");


            if (character.Spellbook == null) return NotFound("This character doesn't have a spellbook");

            return Ok(character.Spellbook);
        }

        [HttpGet("{characterid}/spells/{spellLevel}", Name = nameof(GetSpellbookLevel))]
        public ActionResult GetSpellbookLevel(int characterid, int spellLevel)
        {

            Character character = GetCharacter(characterid).Value;

            if (character == null) return NotFound("No Character with this id");


            if (character.Spellbook == null) return NotFound("This character doesn't have a spellbook");

            if (spellLevel > character.Spellbook.SpellLevels.Length || spellLevel < 0) return BadRequest("spell level is out of bounds");

            SpellLevel spellbookLevel = character.Spellbook.SpellLevels[spellLevel];

            if (spellbookLevel == null) return NotFound(String.Format("{0} doesn't have any spells for {1} level spells", character.Name, spellLevel));

            return Ok(spellbookLevel);
        }

        [HttpGet("{characterid}/spells/{spellLevel}/{spellIndex}", Name = nameof(GetSpecificSpell))]
        public ActionResult GetSpecificSpell(int characterid, int spellLevel, int spellIndex)
        {

            Character character = GetCharacter(characterid).Value;

            if (character == null) return NotFound("No Character with this id");

            KnownSpell spell = ds.GetSpellFromSpellbook(character, spellLevel, spellIndex);
            if (spell == null) return NotFound(String.Format("Spell with index {0} was not found for {1}", spellIndex, character.Name));

            /*
            if (character.SpellBook == null) return NotFound("This character doesn't have a spellbook");

            if (spellLevel > character.SpellBook.SpellLevels.Length || spellLevel < 0) return BadRequest("spell level is out of bounds");

            SpellLevel spellbookLevel = character.SpellBook.SpellLevels[spellLevel];

            if (spellbookLevel == null) return NotFound(String.Format("{0} doesn't have any spells for {1} level spells", character.Name, spellLevel));

            if (spellIndex > spellbookLevel.Spells.Count || spellIndex < 0)
                return BadRequest(String.Format("spell with index {0} was not found", spellIndex));
            
            KnownSpell spell = spellbookLevel.Spells[spellIndex];
            */

            return Ok(spell);
        }

        [HttpPost("{characterid}/races/{racename}")]
        public ActionResult AddRaceToCharacter(int characterid, string racename)
        {
            Character character = GetCharacter(characterid).Value;
            if (character == null)
            {
                return NotFound("Character not found");
            }

            Race race = ds.AddRaceToCharacter(characterid, racename);
            if (race == null) return NotFound("Race doesn't exist");


            return Ok(race);
        }

        [HttpPost("{characterid}/spells/")]
        public ActionResult AddSpell(int characterid, [FromBody] SpellToAddDTO _newSpell)
        {

            Character character = GetCharacter(characterid).Value;

            if (character == null)
            {
                return NotFound("Character not found");
            }

            KnownSpell newSpell = ds.AddSpellToCharacter(character, _newSpell.SpellId, _newSpell.SpellLevel);
            if (newSpell == null) return BadRequest(String.Format("{0} already knows this spell, or the spell doesn't exist", character.Name));

            int spellIndex = character.Spellbook.SpellLevels[_newSpell.SpellLevel].Spells.Count();

            return CreatedAtRoute(nameof(GetSpecificSpell), new { characterid = character.Id, _newSpell.SpellLevel, spellIndex }, newSpell);
        }


        [HttpDelete("{characterid}/spells/{spellLevel}/{spellIndex}")]
        public ActionResult RemoveSpell(int characterid, int spellLevel, int spellIndex)
        {
            Character character = GetCharacter(characterid).Value;

            bool result = ds.RemoveSpellFromCharacter(character, spellLevel, spellIndex);
            System.Console.WriteLine(result);
            if (!result) return NotFound("Spell not found for the character");

            return Ok("Spell removed");
        }


        [HttpGet("{characterid}/classes")]
        public ActionResult GetCharacterClasses(int characterid)
        {
            Character character = GetCharacter(characterid).Value;
            if (character == null) return NotFound();

            List<ClassSimpleDTO> DTO = new List<ClassSimpleDTO>();
            foreach(var c in character.Class)
            {
                DTO.Add(new ClassSimpleDTO(c));
            }

            return Ok(DTO);
        }

        [HttpGet("{characterid}/classes/{classname}")]
        public ActionResult GetCharacterClass(int characterid, string classname)
        {
            Character character = GetCharacter(characterid).Value;
            if (character == null) return NotFound();


            ClassDTO DTO = null;
            foreach(var c in character.Class)
            {
                if (c.ClassName.Equals(classname))
                {

                    DTO = new ClassDTO(c.Class, c.Level);
                    break;
                }
            }
            if (DTO == null) NotFound();


            return Ok(DTO);
        }

        [HttpPost("{characterid}/classes/{classname}")]
        public ActionResult AddClassToCharacter(int characterid, string classname, int level = 1)
        {

            Character character = GetCharacter(characterid).Value;

            CharacterClasses _class = ds.AddClassToCharacter(character, classname, level);
            if (_class == null) return NotFound("Class wasn't found");

            ClassSimpleDTO DTO = new ClassSimpleDTO(_class);

            return Ok(DTO);
        }

        [HttpPost("{characterid}/classes/{classname}/levelup")]
        public ActionResult LevelUpClass(int characterid, string classname)
        {

            Character character = GetCharacter(characterid).Value;

            CharacterClasses _class = ds.LevelUp(character, classname);
            if (_class == null) return NotFound("Class wasn't found");

            ClassSimpleDTO DTO = new ClassSimpleDTO(_class);

            return Ok(DTO);
        }


        [HttpDelete("{characterid}/classes/{classname}")]
        public ActionResult RemoveClassFromCharacter(int characterid, string classname)
        {

            Character character = GetCharacter(characterid).Value;

            bool wasRemoved = ds.RemoveClass(character, classname);
            if (!wasRemoved) return NotFound("Class wasn't found");

            return Ok(String.Format("{0} is no longer a {1}", character.Name, classname));
        }

    }
}