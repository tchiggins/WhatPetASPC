using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhatPetASPC.DAL;
using WhatPetASPC.Models;
namespace WhatPetASPC.Controllers
{
    public class PetTypesController : Controller
    {
        private PetDB db = new PetDB();
        // Show single image
        public ActionResult ShowImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = this.db.AllPetTypes.Find(id);
            if (petType == null)
            {
                return this.HttpNotFound();
            }
            return this.View(petType);
        }
        // GET: PetTypes
        public ActionResult Index(int? SelectedSpecies)
        {
            // Add the All type to the species list for the pulldown menu
            var AllSpecies = new Species
            {
                SpeciesName = "All",
                SpeciesID = 0
            };
            List<Species> MyList = new List<Species>
            {
                AllSpecies
            };
            var MySpecies = this.db.AllSpecies.OrderBy(q => q.SpeciesName).ToList();
            MyList.AddRange(MySpecies);
            this.ViewBag.SelectedSpecies = new SelectList(MyList, "SpeciesID", "SpeciesName", SelectedSpecies);
            int speciesID = SelectedSpecies.GetValueOrDefault();
            // Select the list of items that match the pulldown or all if All is selected
            IQueryable<PetType> petTypeList = this.db.AllPetTypes
                .Where(c => !SelectedSpecies.HasValue || c.SpeciesID == speciesID || speciesID == 0)
                .OrderBy(d => d.SpeciesID);
            var sql = petTypeList.ToString();
            return this.View(petTypeList.ToList());
        }
        // GET: PetTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = this.db.AllPetTypes.Find(id);
            if (petType == null)
            {
                return this.HttpNotFound();
            }
            return this.View(petType);
        }
        // GET: PetTypes/Create
        [HttpGet]
        public ActionResult Create()
        {
            this.ViewBag.SpeciesID = new SelectList(this.db.AllSpecies, "SpeciesID", "SpeciesName");
            return this.View();
        }
        // POST: PetTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "PetTypeID,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetDietCost,PetImage,SpeciesID")] PetType petType)
        {
            if (this.ModelState.IsValid)
            {
                this.db.AllPetTypes.Add(petType);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            Contract.Requires(petType != null);
            this.ViewBag.SpeciesID = new SelectList(this.db.AllSpecies, "SpeciesID", "SpeciesName", petType.SpeciesID);
            return this.View(petType);
        }
        // GET: PetTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = this.db.AllPetTypes.Find(id);
            if (petType == null)
            {
                return this.HttpNotFound();
            }
            this.ViewBag.SpeciesID = new SelectList(this.db.AllSpecies, "SpeciesID", "SpeciesName", petType.SpeciesID);
            return this.View(petType);
        }
        // POST: PetTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Edit([Bind(Include = "PetTypeID,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetDietCost,PetImage,SpeciesID")] PetType petType)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(petType).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            Contract.Requires(petType != null);
            this.ViewBag.SpeciesID = new SelectList(this.db.AllSpecies, "SpeciesID", "SpeciesName", petType.SpeciesID);
            return this.View(petType);
        }
        // GET: PetTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = this.db.AllPetTypes.Find(id);
            if (petType == null)
            {
                return this.HttpNotFound();
            }
            return this.View(petType);
        }
        // POST: PetTypes/Delete/5
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PetType petType = this.db.AllPetTypes.Find(id);
            this.db.AllPetTypes.Remove(petType);
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