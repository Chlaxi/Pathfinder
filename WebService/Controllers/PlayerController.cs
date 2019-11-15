using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.DataService;
using Microsoft.AspNetCore.Authorization;
using WebService.Middleware;

namespace WebService.Controllers
{

    //Thia shouldn't be an actual route, just debugging
    [Route("api/players")]
    public class PlayerController : Controller
    {

        private DataService ds = new DataService();

        [Authorize]
        [HttpGet("{id}", Name = nameof(GetPlayer))]
        public ActionResult<PlayerDTO> GetPlayer(int id)
        {

            if(!AuthService.AuthorizePlayer(HttpContext, id)) return BadRequest("Wrong player");

            Player player = ds.GetPlayer(id);
            if (player == null) NotFound("Player doesn't exist");

            return GetPlayerDTO(player);
        }

        [HttpPost("{playerid}")]
        public ActionResult CreateNewCharacter(int playerid, string characterName)
        {
            if (!AuthService.AuthorizePlayer(HttpContext, playerid)) return BadRequest("Wrong player");

            Player player = ds.GetPlayer(playerid);
            if (player == null) return NotFound("Player doesn't exist");

            //TODO FIX
            Character character = ds.CreateCharacter(player.Id, characterName);
            if (character == null) BadRequest("Something went wrong");


            return CreatedAtRoute(nameof(CharacterController.GetCharacter), new { characterid = character.Id }, new SimpleCharacterDTO(character));
        }


        public PlayerDTO GetPlayerDTO(Player player)
        {
            List<SimpleCharacterDTO> simpleCharacters = new List<SimpleCharacterDTO>();
            foreach (var _character in player.Characters)
            {
                simpleCharacters.Add(new SimpleCharacterDTO(_character));
            }

            PlayerDTO dto = new PlayerDTO()
            {
                Username = player.Username,
                Characters = simpleCharacters

            };

            return dto;
        }
    }
}