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
                new Character { Id = 1, AbilityScore = new int[]{14,19,12,11,6,22}, Age = 369,
                                Alignment =  AlignmentEnum.LN, ArmourClass = 17, Class = ClassEnum.Bard,
                                Health = 56, Level = 6, Name = "Viro", Race = RaceEnum.Elf},
                new Character { Id = 2, AbilityScore = new int[]{18,12,20,8,17,14}, Age = 20,
                                Alignment =  AlignmentEnum.LE, ArmourClass = 22, Class = ClassEnum.Barbarian,
                                Health = 70, Level = 5, Name = "a guy", Race = RaceEnum.Human}
            };
        }

        public void Create(Character character)
        {
            characters.Add(character);
            character.Id = characters.Max(c => c.Id) + 1;
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
            if(cha != null)
            {
                cha.Name = character.Name;
                cha.Class = character.Class;
                cha.Race = character.Race;
                cha.Age = character.Age;
                cha.ArmourClass = character.ArmourClass;
                cha.Health = character.Health;
                cha.Level = character.Level;
            }
        }
    }
}
