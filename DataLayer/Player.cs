using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Player
    {
        public int Id { get; set; }

        public List<int> CharacterId { get; set; }
        public List<Character> Characters { get; set; }

    }
}
