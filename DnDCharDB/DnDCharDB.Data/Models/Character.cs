
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

        public Character()
        {
            //this.Modifiers = GetModifiers();
        }

        private int[] GetModifiers()
        {
            int[] mods = new int[6];
            for(int i = 0; i < 6; i++)
            {
                if(AbilityScore[i] <= 3)
                {
                    mods[i] = -4;
                }
                else if(AbilityScore[i] <= 5)
                {
                    mods[i] = -3;
                }
                else if(AbilityScore[i] <= 7)
                {
                    mods[i] = -2;
                }
                else if(AbilityScore[i] <= 9)
                {
                    mods[i] = -1;
                }
                else if (AbilityScore[i] <= 11)
                {
                    mods[i] = 0;
                }
                else if (AbilityScore[i] <= 13)
                {
                    mods[i] = 1;
                }
                else if (AbilityScore[i] <= 15)
                {
                    mods[i] = 2;
                }
                else if (AbilityScore[i] <= 17)
                {
                    mods[i] = 3;
                }
                else if (AbilityScore[i] <= 19)
                {
                    mods[i] = 4;
                }
                else
                {
                    mods[i] = 5;
                }
            }
            return mods;
        }
    }
}
