using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhatPetASPC.DAL;
using WhatPetASPC.Models;
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
            var MyPetClasses = this.db.AllPetClasses.OrderBy(q => q.ClassName).ToList();
            MyList.AddRange(MyPetClasses);
            this.ViewBag.SelectedClasses = new SelectList(MyList, "PetClassID", "ClassName", SelectedClasses);
            int PetClassID = SelectedClasses.GetValueOrDefault();
            // Select the list of items that match the pulldown or all if All is selected
            IQueryable<Species> speciesList = this.db.AllSpecies
                .Where(c => !SelectedClasses.HasValue || c.PetClassID == PetClassID || PetClassID == 0)
                .OrderBy(d => d.SpeciesID);
            var sql = speciesList.ToString();
            return this.View(speciesList.ToList());
        }
        // GET: Species/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = this.db.AllSpecies.Find(id);
            if (species == null)
            {
                return this.HttpNotFound();
            }
            return this.View(species);
        }
        // GET: Species/Create
        public ActionResult Create()
        {
            this.ViewBag.PetClassID = new SelectList(this.db.AllPetClasses, "PetClassID", "ClassName");
            return this.View();
        }
        // POST: Species/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Create([Bind(Include = "SpeciesID,SpeciesName,PetClassID")] Species species)
        {
            if (this.ModelState.IsValid)
            {
                this.db.AllSpecies.Add(species);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            Contract.Requires(species != null);
            this.ViewBag.PetClassID = new SelectList(this.db.AllPetClasses, "PetClassID", "ClassName", species.PetClassID);
            return this.View(species);
        }
        // GET: Species/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = this.db.AllSpecies.Find(id);
            if (species == null)
            {
                return this.HttpNotFound();
            }
            this.ViewBag.PetClassID = new SelectList(this.db.AllPetClasses, "PetClassID", "ClassName", species.PetClassID);
            return this.View(species);
        }
        // POST: Species/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Edit([Bind(Include = "SpeciesID,SpeciesName,PetClassID")] Species species)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(species).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            Contract.Requires(species != null);
            this.ViewBag.PetClassID = new SelectList(this.db.AllPetClasses, "PetClassID", "ClassName", species.PetClassID);
            return this.View(species);
        }
        // GET: Species/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Species species = this.db.AllSpecies.Find(id);
            if (species == null)
            {
                return this.HttpNotFound();
            }
            return this.View(species);
        }
        // POST: Species/Delete/5
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Species species = this.db.AllSpecies.Find(id);
            this.db.AllSpecies.Remove(species);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}