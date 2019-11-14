﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.DataService;

namespace WebService.Controllers
{
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

        [HttpGet("{characterid}", Name = nameof(GetCharacter))]
        public ActionResult GetCharacter(int characterid)
        {
            Character character = ds.GetCharacter(characterid);
            if (character == null) return NotFound();

            return Ok(character);
        }

        [HttpGet("{characterid}/spells", Name = nameof(GetSpellbook))]
        public ActionResult GetSpellbook(int characterid)
        {
            Character character = ds.GetCharacter(characterid);
            if (character == null) return NotFound("No Character with this id");


            if (character.SpellBook == null) return NotFound("This character doesn't have a spellbook");

            return Ok(character.SpellBook);
        }

        [HttpGet("{characterid}/spells/{spellLevel}", Name = nameof(GetSpellbookLevel))]
        public ActionResult GetSpellbookLevel(int characterid, int spellLevel)
        {
            //TODO Authorisation

            Character character = ds.GetCharacter(characterid);
            if (character == null) return NotFound("No Character with this id");


            if (character.SpellBook == null) return NotFound("This character doesn't have a spellbook");

            if (spellLevel > character.SpellBook.SpellLevels.Length || spellLevel < 0) return BadRequest("spell level is out of bounds");

            SpellLevel spellbookLevel = character.SpellBook.SpellLevels[spellLevel];

            if (spellbookLevel == null) return NotFound(String.Format("{0} doesn't have any spells for {1} level spells",character.Name, spellLevel));
            
            return Ok(spellbookLevel);
        }
        
        [HttpGet("{characterid}/spells/{spellLevel}/{spellIndex}", Name = nameof(GetSpecificSpell))]
        public ActionResult GetSpecificSpell(int characterid, int spellLevel, int spellIndex)
        {
            //TODO Authorisation


            Character character = ds.GetCharacter(characterid);
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

        [HttpPost("{characterid}/spells/")]
        public ActionResult AddSpell(int characterid, int spellid, int spellLevel)
        {

            //TODO Authorisation

            Character character = ds.GetCharacter(characterid);

            if (character == null)
            {
                return NotFound("Character not found");
            }

            KnownSpell newSpell = ds.AddSpellToCharacter(character, spellid, spellLevel);
            if (newSpell == null) return BadRequest(String.Format("{0} already knows this spell, or the spell doesn't exist", character.Name));

            int spellIndex = character.SpellBook.SpellLevels[spellLevel].Spells.Count();

            return CreatedAtRoute(nameof(GetSpecificSpell), new { characterid = character.Id, spellLevel, spellIndex }, newSpell);
        }

        [HttpDelete("{characterid}/spells/{spellLevel}/{spellIndex}")]
        public ActionResult RemoveSpell(int characterid, int spellLevel, int spellIndex)
        {
            Character character = ds.GetCharacter(characterid);
            bool result = ds.RemoveSpellFromCharacter(character, spellLevel, spellIndex);
            System.Console.WriteLine(result);
            if (!result) return NotFound("Spell not found for the character");

            return Ok("Spell removed");
        }
    }
}