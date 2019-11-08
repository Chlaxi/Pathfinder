using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.DataService;

namespace WebService.Controllers
{
    [Route("api/specials")]
    public class SpecialAbilityController : Controller
    {
        private DataService ds = new DataService();

        [HttpGet(Name = nameof(GetSpecials))]
        public ActionResult GetSpecials()
        {
            List<SpecialAbility> specials = ds.GetSpecialAbilities();
            if (specials.Count == 0) return NotFound();

            return Ok(specials);
        }

        [HttpGet("{id}", Name = nameof(GetSpecial))]
        public ActionResult GetSpecial(int id)
        {
            SpecialAbility special = ds.GetSpecialAbility(id);
            if (special == null) return NotFound();

            return Ok(special);
        }
    }
}