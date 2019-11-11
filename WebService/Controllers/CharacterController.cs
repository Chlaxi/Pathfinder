using System;
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



    }
}