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
        public ActionResult GetPlayer(int id)
        {

            if(!AuthService.AuthorizePlayer(HttpContext, id)) return BadRequest("Wrong player");

            Player player = ds.GetPlayer(id);
            if (player == null) NotFound("Player doesn't exists");

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

            

            return Ok(dto);
        }
/*
        [HttpPost]
        public ActionResult CreatePlayer([FromBody] UserCreationDTO newUser)
        {
            Console.WriteLine("NEW PLAYER FROM PLAYER CONTROLLER");
            if (ds.GetPlayerByUsername(newUser.Username) != null)
            {
                return BadRequest("User already exists");
            }

            Player player = ds.CreatePlayer(newUser.Username, newUser.Password);
            if (player == null) BadRequest("ERROR");
            Console.WriteLine("Player {0} created",player.Username);

            return CreatedAtRoute(nameof(GetPlayer), new { id = player.Id }, player);
        }*/
    }
}