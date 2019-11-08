using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        public string Index()
        {

            return "Go to either races, spells, specials, classes, feats";
        }
    }
}