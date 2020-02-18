using DnDCharDB.Data.Models;
using DnDCharDB.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DnDCharDB.Web.Api
{
    public class CharController : ApiController
    {
        private readonly ICharData db;

        public CharController(ICharData db)
        {
            this.db = db;
        }

        public IEnumerable<Character> get()
        {
            var model = db.GetAll();
            return model;
        }
    }
}
