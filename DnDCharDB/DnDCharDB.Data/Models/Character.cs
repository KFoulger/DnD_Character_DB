
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharDB.Data.Models
{
    public class Character
    {
        public int Id { get; set; }
        public int[] AbilityScore { get; set; } 
        public int[] Modifiers { get; set; }
        public int Health { get; set; }
        public int ArmourClass { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Level { get; set; }
        public RaceEnum Race { get; set; }
        public ClassEnum Class { get; set; }
        public AlignmentEnum Alignment { get; set; }

    }
}
