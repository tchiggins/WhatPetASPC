﻿using System;
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
    public class SpeciesController : Controller
    {
        private PetDB db = new PetDB();

        // GET: Species
        public ActionResult Index()
        {
            var allSpecies = db.AllSpecies.Include(s => s.PetClass);
            return View(allSpecies.ToList());
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
            ViewBag.PetClassID = new SelectList(db.AllPetClasses, "PetClassID", "ClasssName");
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

            ViewBag.PetClassID = new SelectList(db.AllPetClasses, "PetClassID", "ClasssName", species.PetClassID);
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
            ViewBag.PetClassID = new SelectList(db.AllPetClasses, "PetClassID", "ClasssName", species.PetClassID);
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
            ViewBag.PetClassID = new SelectList(db.AllPetClasses, "PetClassID", "ClasssName", species.PetClassID);
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
