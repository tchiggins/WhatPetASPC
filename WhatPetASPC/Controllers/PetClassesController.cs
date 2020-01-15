using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhatPetASPC.DAL;
using WhatPetASPC.Models;
namespace WhatPetASPC.Controllers
{
    public class PetClassesController : Controller
    {
        private PetDB db = new PetDB();
        // GET: PetClasses
        public ActionResult Index()
        {
            return View(db.AllPetClasses.ToList());
        }
        // GET: PetClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetClass petClass = db.AllPetClasses.Find(id);
            if (petClass == null)
            {
                return HttpNotFound();
            }
            return View(petClass);
        }
        // GET: PetClasses/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: PetClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetClassID,ClassName")] PetClass petClass)
        {
            if (ModelState.IsValid)
            {
                db.AllPetClasses.Add(petClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petClass);
        }
        // GET: PetClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetClass petClass = db.AllPetClasses.Find(id);
            if (petClass == null)
            {
                return HttpNotFound();
            }
            return View(petClass);
        }
        // POST: PetClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetClassID,ClassName")] PetClass petClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petClass);
        }
        // GET: PetClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetClass petClass = db.AllPetClasses.Find(id);
            if (petClass == null)
            {
                return HttpNotFound();
            }
            return View(petClass);
        }
        // POST: PetClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetClass petClass = db.AllPetClasses.Find(id);
            db.AllPetClasses.Remove(petClass);
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