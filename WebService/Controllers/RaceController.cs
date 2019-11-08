using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.DataService;

namespace WebService.Controllers
{
    [Route("api/races")]
    public class RaceController : Controller
    {

        private DataService ds = new DataService();

        [HttpGet(Name = nameof(GetRaces))]
        public ActionResult GetRaces()
        {
            List<Race> races = ds.GetRaces();
            if (races.Count == 0) return NotFound();

            return Ok(races);
        }

        [HttpGet("{name}", Name = nameof(GetRace))]
        public ActionResult GetRace(string name)
        {
            Race race = ds.GetRace(name);
            if (race == null) return NotFound(race.Size);

            return Ok(race);
        }


    }
}