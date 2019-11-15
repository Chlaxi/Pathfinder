using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;

namespace WebService
{
    /// <summary>
    /// A shorthand identifier for a character. Includes the Id, the name, and classes, as well as a link to the character.
    /// </summary>
    public class SimpleCharacterDTO
    {
        public SimpleCharacterDTO(Character character)
        {
            Id = character.Id;
            Name = character.Name;
            Classes = "";
            foreach(var _class in character.Class)
            {
                Classes += _class.ClassName + " " + _class.Level+", ";
            }
            Classes = Classes.TrimEnd(',', ' ');
            Link = String.Format("/api/characters/{0}", Id);

        }
        public string Link { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        //Turn into class links
        public string Classes { get; set; }
    }
}
