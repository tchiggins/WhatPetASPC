using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhatPetASPC.DAL;
using WhatPetASPC.Models;

namespace WhatPetASPC.Controllers
{
    public class PetTypesController : Controller
    {
        private PetDB db = new PetDB();

        // GET: PetTypes
        public ActionResult Index()
        {
            return View(db.AllPetTypes.ToList());
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
            return View();
        }

        // POST: PetTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetTypeID,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetImage")] PetType petType)
        {
            if (ModelState.IsValid)
            {
                db.AllPetTypes.Add(petType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(petType);
        }

        // POST: PetTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetTypeID,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetImage")] PetType petType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
