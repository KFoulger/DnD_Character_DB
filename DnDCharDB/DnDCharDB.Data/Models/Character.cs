
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
        public int[] Stats { get; set; } 
        public int[] Bases { get; set; }
        public int[] Growths { get; set; }
        public int Movement { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Level { get; set; }
        public RaceEnum Race { get; set; }
        public ClassEnum Class { get; set; }
        public AlignmentEnum Alignment { get; set; }
        public bool StatsAtBase { get; set; }

        public Character()
        {
            //this.Modifiers = GetModifiers();
        }
    }
}
