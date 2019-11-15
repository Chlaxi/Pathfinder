using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;

namespace WebService
{
    public class PlayerDTO
    {
        public string Username { get; set; }
        public List<SimpleCharacterDTO> Characters { get; set; }
    }
}
