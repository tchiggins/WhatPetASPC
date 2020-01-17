using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhatPetASPC.DAL;
using WhatPetASPC.Models;
using System.Collections.Generic;
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
            PetType petType = db.AllPetTypes.Find(id);
            if (petType == null)
            {
                return HttpNotFound();
            }
            return View(petType);
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
            var MySpecies = db.AllSpecies.OrderBy(q => q.SpeciesName).ToList();
            MyList.AddRange(MySpecies);
            ViewBag.SelectedSpecies = new SelectList(MyList, "SpeciesID", "SpeciesName", SelectedSpecies);
            int speciesID = SelectedSpecies.GetValueOrDefault();
            // Select the list of items that match the pulldown or all if All is selected
            IQueryable<PetType> petTypeList = db.AllPetTypes
                .Where(c => !SelectedSpecies.HasValue || c.SpeciesID == speciesID || speciesID == 0)
                .OrderBy(d => d.SpeciesID);
            var sql = petTypeList.ToString();
            return View(petTypeList.ToList());
        }
        // GET: PetTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = db.AllPetTypes.Find(id);
            if (petType == null)
            {
                return HttpNotFound();
            }
            return View(petType);
        }
        // GET: PetTypes/Create
        public ActionResult Create()
        {
            ViewBag.SpeciesID = new SelectList(db.AllSpecies, "SpeciesID", "SpeciesName");
            return View();
        }
        // POST: PetTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetTypeID,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetDietCost,PetImage,SpeciesID")] PetType petType)
        {
            if (ModelState.IsValid)
            {
                db.AllPetTypes.Add(petType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpeciesID = new SelectList(db.AllSpecies, "SpeciesID", "SpeciesName", petType.SpeciesID);
            return View(petType);
        }
        // GET: PetTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = db.AllPetTypes.Find(id);
            if (petType == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpeciesID = new SelectList(db.AllSpecies, "SpeciesID", "SpeciesName", petType.SpeciesID);
            return View(petType);
        }
        // POST: PetTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetTypeID,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetDietCost,PetImage,SpeciesID")] PetType petType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpeciesID = new SelectList(db.AllSpecies, "SpeciesID", "SpeciesName", petType.SpeciesID);
            return View(petType);
        }
        // GET: PetTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = db.AllPetTypes.Find(id);
            if (petType == null)
            {
                return HttpNotFound();
            }
            return View(petType);
        }
        // POST: PetTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetType petType = db.AllPetTypes.Find(id);
            db.AllPetTypes.Remove(petType);
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