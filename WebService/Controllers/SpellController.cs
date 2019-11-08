using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.DataService;

namespace WebService.Controllers
{
    [Route("api/spells")]
    public class SpellController : Controller
    {
        private DataService ds = new DataService();

        [HttpGet(Name = nameof(GetSpells))]
        public ActionResult GetSpells()
        {
            List<Spell> spells = ds.GetSpells();
            if (spells.Count == 0) return NotFound();

            return Ok(spells);
        }

        [HttpGet("{id}", Name = nameof(GetSpell))]
        public ActionResult GetSpell(int id)
        {
            Spell spell = ds.GetSpell(id);
            if (spell == null) return NotFound();

            return Ok(spell);
        }


    }
}