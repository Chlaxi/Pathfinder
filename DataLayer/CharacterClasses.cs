using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer
{
    public class CharacterClasses
    {
        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public string ClassName { get; set; }
        public int Level { get; set; }

        public Class Class { get; set; }
    }
}
