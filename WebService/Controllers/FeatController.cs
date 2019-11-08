using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.DataService;

namespace WebService.Controllers
{
    [Route("api/feats")]
    public class FeatController : Controller
    {
        private DataService ds = new DataService();

        [HttpGet(Name = nameof(GetFeats))]
        public ActionResult GetFeats()
        {
            List<Feat> feats = ds.GetFeats();
            if (feats.Count == 0) return NotFound();

            return Ok(feats);
        }

        [HttpGet("{id}", Name = nameof(GetFeat))]
        public ActionResult GetFeat(int id)
        {
            Feat feat = ds.GetFeat(id);
            if (feat == null) return NotFound();

            return Ok(feat);
        }
    }
}