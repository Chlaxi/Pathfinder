using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
   public class SpecialAbility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        
        
        public string ClassName { get;set;}
        public string Source { get; set; }
    }
}
