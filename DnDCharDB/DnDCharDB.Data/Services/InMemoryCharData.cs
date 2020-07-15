using DnDCharDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDCharDB.Data.Services
{
    public class InMemoryCharData : ICharData
    {
        List<Character> characters;

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
                Random rng = new Random();
                for (int i = 0; i < cha.Stats.Length; i++)
                {
                    if (rng.Next(0, 100) <= cha.Growths[i])
                    {
                        cha.Stats[i]++;
                    }
                }

            }
        }
    }
}
