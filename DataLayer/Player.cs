using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public List<Character> Characters { get; set; }

    }
}
