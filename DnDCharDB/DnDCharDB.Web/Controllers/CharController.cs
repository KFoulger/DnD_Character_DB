using DnDCharDB.Data.Models;
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

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = db.GetModel(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.GetModel(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Level(int id)
        {
            var model = db.GetModel(id);
            db.Level(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Random()
        {
            int charId = db.Random();
            return RedirectToAction("Details", new { id = charId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Character character)
        {
            db.Create(character);

            return RedirectToAction("Details", new { id = character.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Character character)
        {
            if (ModelState.IsValid)
            {
                db.Update(character);
                return RedirectToAction("Details", new { id = character.Id });
            }
            return View(character);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Character character)
        {
            if (ModelState.IsValid)
            {
                db.Level(character);
                return RedirectToAction("Details", new { id = character.Id });
            }
            return View(character);
        }
    }
}