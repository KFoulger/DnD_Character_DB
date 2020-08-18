using DnDCharDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DnDCharDB.Data.Services
{
    public class InMemoryCharData : ICharData
    {
        List<Character> characters;

        private Random rng = new Random();

        public InMemoryCharData()
        {
            characters = new List<Character>()
            {
                new Character { Id = 1, Stats = new int[]{14,19,12,11,6,22,2,13}, Age = 369,
                                Alignment =  AlignmentEnum.LN, Class = ClassEnum.Bard,
                                Level = 6, Name = "Viro", Race = RaceEnum.Elf},
                new Character { Id = 2, Stats = new int[]{18,12,20,8,17,14,20,10}, Age = 20,
                                Alignment =  AlignmentEnum.LE, Class = ClassEnum.Barbarian,
                                Level = 5, Name = "a guy", Race = RaceEnum.Human}
            };
        }

        public void Create(Character character)
        {
            characters.Add(character);
            character.Id = characters.Max(c => c.Id) + 1;

            if (character.StatsAtBase)
            {
                for(int i = 0; i < (character.Level - 1); i++)
                {
                    Level(character);
                }
                character.StatsAtBase = false;
            }
        }

        public IEnumerable<Character> GetAll()
        {
            return characters.OrderBy(C => C.Name);
        }

        public Character GetModel(int id)
        {
            return characters.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Character character)
        {
            Character cha = GetModel(character.Id);
            if (cha != null)
            {
                cha.Name = character.Name;
                cha.Class = character.Class;
                cha.Race = character.Race;
                cha.Age = character.Age;
                cha.Level = character.Level;
            }
        }

        public void Level(Character character)
        {
            Character cha = GetModel(character.Id);
            if (cha != null)
            {
                if (!character.StatsAtBase || character.Level == 1)
                {
                    cha.Level++;
                }
                
                for (int i = 0; i < cha.Stats.Length; i++)
                {
                    if (rng.Next(0, 100) <= cha.Growths[i])
                    {
                        cha.Stats[i]++;
                    }
                }

            }
        }

        public int Random()
        {
            char[] consonants = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
            char[] vowels = { 'a', 'i', 'u', 'e', 'o' };

            Character character = new Character();
            characters.Add(character);
            character.Id = characters.Max(c => c.Id) + 1;

            int nameLength = rng.Next(3, 9);
            int consecutiveSoundCount = 0;
            char lastSound = 'n'; //'c' for consonant or 'v' for vowel
            string name = "";

            for(int i = 0; i < nameLength; i++)
            {
                if (consecutiveSoundCount == 2)
                {
                    switch (lastSound)
                    {
                        case 'v':
                            name += consonants[rng.Next(0, 19)];
                            consecutiveSoundCount = 1;
                            lastSound = 'c';
                            break;

                        case 'c':
                            name += vowels[rng.Next(0, 4)];
                            consecutiveSoundCount = 1;
                            lastSound = 'v';
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    if (rng.Next(1, 3) == 1)
                    {
                        name += consonants[rng.Next(0, 19)];
                        if (lastSound == 'c')
                        {
                            consecutiveSoundCount++;
                        }
                        else
                        {
                            consecutiveSoundCount = 1;
                        }
                        lastSound = 'c';
                    }
                    else
                    {
                        name += vowels[rng.Next(0, 4)];
                        if (lastSound == 'v')
                        {
                            consecutiveSoundCount++;
                        }
                        else
                        {
                            consecutiveSoundCount = 1;
                        }
                        lastSound = 'v';
                    }
                }
            }
            character.Name = name;

            character.Race = (RaceEnum) rng.Next(0, 8);
            switch (character.Race)
            {
                case RaceEnum.Dwarf:
                    character.Age = rng.Next(35, 350);
                    break;
                case RaceEnum.Elf:
                    character.Age = rng.Next(70, 750);
                    break;
                case RaceEnum.Halfling:
                    character.Age = rng.Next(15, 250);
                    break;
                case RaceEnum.Human:
                    character.Age = rng.Next(14, 80);
                    break;
                case RaceEnum.Dragonborn:
                    character.Age = rng.Next(10, 80);
                    break;
                case RaceEnum.Gnome:
                    character.Age = rng.Next(25, 500);
                    break;
                case RaceEnum.hElf:
                    character.Age = rng.Next(15, 200);
                    break;
                case RaceEnum.hOrc:
                    character.Age = rng.Next(10, 75);
                    break;
                case RaceEnum.Tiefling:
                    character.Age = rng.Next(14, 110);
                    break;
                default:
                    break;
            }
            character.Alignment = (AlignmentEnum) rng.Next(0, 8);
            character.Class = (ClassEnum) rng.Next(0,11);
            int[] growths = Growths();
            switch (character.Class)
            {
                case ClassEnum.Barbarian:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(19, 28);

                    character.Growths[1] = growths[1];
                    character.Stats[1] = Stat("pri");

                    character.Growths[2] = growths[6];
                    character.Stats[2] = Stat("");

                    character.Growths[3] = growths[4];
                    character.Stats[3] = Stat("ter");

                    character.Growths[4] = growths[5];
                    character.Stats[4] = Stat("ter");

                    character.Growths[5] = growths[3];
                    character.Stats[5] = Stat("sec");

                    character.Growths[6] = growths[2];
                    character.Stats[6] = Stat("sec");

                    character.Growths[7] = growths[7];
                    character.Stats[7] = Stat("");
                    break;
                case ClassEnum.Bard:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(15, 24);

                    character.Growths[1] = growths[2];
                    character.Stats[1] = Stat("sec");

                    character.Growths[2] = growths[3];
                    character.Stats[2] = Stat("sec");

                    character.Growths[3] = growths[4];
                    character.Stats[3] = Stat("ter");

                    character.Growths[4] = growths[1];
                    character.Stats[4] = Stat("pri");

                    character.Growths[5] = growths[5];
                    character.Stats[5] = Stat("ter");

                    character.Growths[6] = growths[6];
                    character.Stats[6] = Stat("");

                    character.Growths[7] = growths[7];
                    character.Stats[7] = Stat("");
                    break;
                case ClassEnum.Cleric:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(9, 19);

                    character.Growths[1] = growths[7];
                    character.Stats[1] = Stat("");

                    character.Growths[2] = growths[4];
                    character.Stats[2] = Stat("ter");

                    character.Growths[3] = growths[2];
                    character.Stats[3] = Stat("sec");

                    character.Growths[4] = growths[6];
                    character.Stats[4] = Stat("");

                    character.Growths[5] = growths[3];
                    character.Stats[5] = Stat("sec");

                    character.Growths[6] = growths[5];
                    character.Stats[6] = Stat("ter");

                    character.Growths[7] = growths[1];
                    character.Stats[7] = Stat("pri");
                    break;
                case ClassEnum.Druid:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(18, 31);

                    character.Growths[1] = growths[2];
                    character.Stats[1] = Stat("sec");

                    character.Growths[2] = growths[3];
                    character.Stats[2] = Stat("sec");

                    character.Growths[3] = growths[5];
                    character.Stats[3] = Stat("ter");

                    character.Growths[4] = growths[6];
                    character.Stats[4] = Stat("");

                    character.Growths[5] = growths[7];
                    character.Stats[5] = Stat("");

                    character.Growths[6] = growths[4];
                    character.Stats[6] = Stat("ter");

                    character.Growths[7] = growths[1];
                    character.Stats[7] = Stat("pri");
                    break;
                case ClassEnum.Fighter:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(15, 26);

                    character.Growths[1] = growths[2];
                    character.Stats[1] = Stat("sec");

                    character.Growths[2] = growths[3];
                    character.Stats[2] = Stat("sec");

                    character.Growths[3] = growths[4];
                    character.Stats[3] = Stat("ter");

                    character.Growths[4] = growths[1];
                    character.Stats[4] = Stat("pri");

                    character.Growths[5] = growths[5];
                    character.Stats[5] = Stat("ter");

                    character.Growths[6] = growths[6];
                    character.Stats[6] = Stat("");

                    character.Growths[7] = growths[7];
                    character.Stats[7] = Stat("");
                    break;
                case ClassEnum.Monk:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(12, 24);

                    character.Growths[1] = growths[2];
                    character.Stats[1] = Stat("sec");

                    character.Growths[2] = growths[7];
                    character.Stats[2] = Stat("");

                    character.Growths[3] = growths[1];
                    character.Stats[3] = Stat("pri");

                    character.Growths[4] = growths[3];
                    character.Stats[4] = Stat("sec");

                    character.Growths[5] = growths[5];
                    character.Stats[5] = Stat("ter");

                    character.Growths[6] = growths[4];
                    character.Stats[6] = Stat("ter");

                    character.Growths[7] = growths[6];
                    character.Stats[7] = Stat("");
                    break;
                case ClassEnum.Paladin:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(20, 33);

                    character.Growths[1] = growths[3];
                    character.Stats[1] = Stat("sec");

                    character.Growths[2] = growths[4];
                    character.Stats[2] = Stat("ter");

                    character.Growths[3] = growths[6];
                    character.Stats[3] = Stat("");

                    character.Growths[4] = growths[5];
                    character.Stats[4] = Stat("ter");

                    character.Growths[5] = growths[7];
                    character.Stats[5] = Stat("");

                    character.Growths[6] = growths[1];
                    character.Stats[6] = Stat("pri");

                    character.Growths[7] = growths[2];
                    character.Stats[7] = Stat("sec");
                    break;
                case ClassEnum.Ranger:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(15, 25);

                    character.Growths[1] = growths[3];
                    character.Stats[1] = Stat("sec");

                    character.Growths[2] = growths[6];
                    character.Stats[2] = Stat("");

                    character.Growths[3] = growths[2];
                    character.Stats[3] = Stat("sec");

                    character.Growths[4] = growths[1];
                    character.Stats[4] = Stat("pri");

                    character.Growths[5] = growths[5];
                    character.Stats[5] = Stat("ter");

                    character.Growths[6] = growths[4];
                    character.Stats[6] = Stat("ter");

                    character.Growths[7] = growths[7];
                    character.Stats[7] = Stat("");
                    break;
                case ClassEnum.Rogue:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(11, 20);

                    character.Growths[1] = growths[4];
                    character.Stats[1] = Stat("ter");

                    character.Growths[2] = growths[7];
                    character.Stats[2] = Stat("");

                    character.Growths[3] = growths[1];
                    character.Stats[3] = Stat("pri");

                    character.Growths[4] = growths[2];
                    character.Stats[4] = Stat("sec");

                    character.Growths[5] = growths[3];
                    character.Stats[5] = Stat("sec");

                    character.Growths[6] = growths[5];
                    character.Stats[6] = Stat("ter");

                    character.Growths[7] = growths[6];
                    character.Stats[7] = Stat("");
                    break;
                case ClassEnum.Sorcerer:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(11, 20);

                    character.Growths[1] = growths[5];
                    character.Stats[1] = Stat("ter");

                    character.Growths[2] = growths[3];
                    character.Stats[2] = Stat("sec");

                    character.Growths[3] = growths[4];
                    character.Stats[3] = Stat("ter");

                    character.Growths[4] = growths[1];
                    character.Stats[4] = Stat("pri");

                    character.Growths[5] = growths[7];
                    character.Stats[5] = Stat("");

                    character.Growths[6] = growths[6];
                    character.Stats[6] = Stat("");

                    character.Growths[7] = growths[2];
                    character.Stats[7] = Stat("sec");
                    break;
                case ClassEnum.Warlock:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(16, 26);

                    character.Growths[1] = growths[6];
                    character.Stats[1] = Stat("");

                    character.Growths[2] = growths[2];
                    character.Stats[2] = Stat("sec");

                    character.Growths[3] = growths[4];
                    character.Stats[3] = Stat("ter");

                    character.Growths[4] = growths[7];
                    character.Stats[4] = Stat("");

                    character.Growths[5] = growths[1];
                    character.Stats[5] = Stat("pri");

                    character.Growths[6] = growths[3];
                    character.Stats[6] = Stat("sec");

                    character.Growths[7] = growths[5];
                    character.Stats[7] = Stat("ter");
                    break;
                case ClassEnum.Wizard:
                    character.Growths[0] = growths[0];
                    character.Stats[0] = rng.Next(10, 21);

                    character.Growths[1] = growths[7];
                    character.Stats[1] = Stat("");

                    character.Growths[2] = growths[1];
                    character.Stats[2] = Stat("pri");

                    character.Growths[3] = growths[2];
                    character.Stats[3] = Stat("sec");

                    character.Growths[4] = growths[4];
                    character.Stats[4] = Stat("ter");

                    character.Growths[5] = growths[5];
                    character.Stats[5] = Stat("ter");

                    character.Growths[6] = growths[6];
                    character.Stats[6] = Stat("");

                    character.Growths[7] = growths[3];
                    character.Stats[7] = Stat("sec");
                    break;
                default:
                    break;
            }
            return character.Id;
        }

        private int Stat(string tier)
        {
            int stat = rng.Next(2, 7);
            switch (tier)
            {
                case "pri":
                    stat += 4;
                    break;
                case "sec":
                    stat += 2;
                    break;
                case "ter":
                    stat += 1;
                    break;
                default:
                    break;
            }

            return stat;
        }
        private int[]Growths()
        {
            int[] growths = new int[8];

            growths[0] = rng.Next(65, 95);
            growths[1] = rng.Next(55, 90);
            growths[2] = rng.Next(40, 70);
            growths[3] = rng.Next(40, 70);
            growths[4] = rng.Next(30, 50);
            growths[5] = rng.Next(30, 50);
            growths[6] = rng.Next(10, 30);
            growths[7] = rng.Next(10, 30);

            return growths;
        }
    }
}
