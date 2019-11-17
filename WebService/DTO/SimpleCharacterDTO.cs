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
            if (character == null)
            {
                Console.WriteLine("****************\n\n\nThere is no character for the DTO!!\n \n \n ***************");
            }
            Id = character.Id;
            Name = character.Name;
            Race = character.RaceName;
            Classes = "";
            Console.WriteLine(character.Class.Count() +"SO MANY CLASSES");
            if (character.Class.Count() > 0 || character.Class==null)
            {
                foreach (var _class in character.Class)
                {
                    Classes += _class.ClassName + " " + _class.Level + ", ";
                }
                Classes = Classes.TrimEnd(',', ' ');
            }
            Link = String.Format("/api/characters/{0}", Id);

        }
        public string Link { get; set; }
        public string Name { get; set; }
        
        public string Race { get; set; }
       
        //Turn into class links
        public string Classes { get; set; }
        private int Id { get; set; }
    }
}
