using DnDCharDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharDB.Data.Services
{
    public class CharacterDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
    }
}
