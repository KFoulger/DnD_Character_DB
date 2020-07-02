using DnDCharDB.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharDB.Data.Services
{
    public interface ICharData
    {
        IEnumerable<Character> GetAll();
        Character GetModel(int id);
        void Update(Character character);
        void Create(Character character);
        void Level(Character character);
    }
}
