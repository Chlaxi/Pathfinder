using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DataLayer.DataService;

namespace WebService.Controllers
{
    [Route("api/classes")]
    public class ClassController : Controller
    {
        private DataService ds = new DataService();

        [HttpGet(Name = nameof(GetClasses))]
        public ActionResult GetClasses()
        {
            List<Class> classes = ds.GetClasses();
            if (classes.Count == 0) return NotFound();

            return Ok(classes);
        }

        [HttpGet("{name}", Name = nameof(GetClass))]
        public ActionResult GetClass(string name)
        {
            Class _class = ds.GetClass(name);
            if (_class == null) return NotFound();

            return Ok(_class);
        }
    }
}