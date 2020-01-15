using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhatPetASPC.DAL;
using WhatPetASPC.Models;
using System.Collections.Generic;

namespace WhatPetASPC.Controllers
{
    public class SpeciesController : Controller
    {
        private PetDB db = new PetDB();

        public ActionResult Index(int? SelectedClasses)
        {
            // Add the All type to the species list for the pulldown menu
            var AllClasses = new PetClass
            {
                ClassName = "All",
                PetClassID = 0
            };
            List<PetClass> MyList = new List<PetClass>
            {
                AllClasses
            };

            var MyPetClasses = db.AllPetClasses.OrderBy(q => q.ClassName).ToList();
            MyList.AddRange(MyPetClasses);

            ViewBag.SelectedClasses = new SelectList(MyList, "PetClassID", "ClassName", SelectedClasses);
            int PetClassID = SelectedClasses.GetValueOrDefault();

            // Select the list of items that match the pulldown or all if All is selected
            IQueryable<Species> speciesList = db.AllSpecies
                .Where(c => !SelectedClasses.HasValue || c.PetClassID == PetClassID || PetClassID == 0)
                .OrderBy(d => d.SpeciesID);
            var sql = speciesList.ToString();
            return View(speciesList.ToList());
        }
        // GET: Species/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.AllSpecies.Find(id);
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }
        // GET: Species/Create
        public ActionResult Create()
        {
            ViewBag.PetClassID = new SelectList(db.AllPetClasses, "PetClassID", "ClassName");
            return View();
        }
        // POST: Species/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpeciesID,SpeciesName,PetClassID")] Species species)
        {
            if (ModelState.IsValid)
            {
                db.AllSpecies.Add(species);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PetClassID = new SelectList(db.AllPetClasses, "PetClassID", "ClassName", species.PetClassID);
            return View(species);
        }
        // GET: Species/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.AllSpecies.Find(id);
            if (species == null)
            {
                return HttpNotFound();
            }
            ViewBag.PetClassID = new SelectList(db.AllPetClasses, "PetClassID", "ClassName", species.PetClassID);
            return View(species);
        }
        // POST: Species/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpeciesID,SpeciesName,PetClassID")] Species species)
        {
            if (ModelState.IsValid)
            {
                db.Entry(species).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PetClassID = new SelectList(db.AllPetClasses, "PetClassID", "ClassName", species.PetClassID);
            return View(species);
        }
        // GET: Species/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = db.AllSpecies.Find(id);
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }
        // POST: Species/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Species species = db.AllSpecies.Find(id);
            db.AllSpecies.Remove(species);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}