using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.DataService;

namespace WebService.Controllers
{

    //Thia shouldn't be an actual route, just debugging
    [Route("api/players")]
    public class PlayerController : Controller
    {

        private DataService ds = new DataService();

        [HttpGet("{id}", Name = nameof(GetPlayer))]
        public ActionResult GetPlayer(int id)
        {
            Player player = ds.GetPlayer(id);
            if (player == null) NotFound("Player doesn't exists");

            return Ok(player);
        }

        [HttpPost()]
        public ActionResult CreatePlayer(string username)
        {
            Player player = ds.CreatePlayer(username);
            if (player == null) BadRequest();

            return CreatedAtRoute(nameof(GetPlayer), new { id = player.Id }, player);
        }
    }
}