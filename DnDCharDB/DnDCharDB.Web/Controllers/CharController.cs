using DnDCharDB.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DnDCharDB.Web.Controllers
{
    public class CharController : Controller
    {
        private readonly ICharData db;

        public CharController(ICharData db)
        {
            this.db = db;
        }
        // GET: Char
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }
    }
}