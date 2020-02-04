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
            return this.View(this.db.AllPetClasses.ToList());
        }
        // GET: PetClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetClass petClass = this.db.AllPetClasses.Find(id);
            if (petClass == null)
            {
                return this.HttpNotFound();
            }
            return this.View(petClass);
        }
        // GET: PetClasses/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }
        // POST: PetClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "PetClassID,ClassName")] PetClass petClass)
        {
            if (this.ModelState.IsValid)
            {
                this.db.AllPetClasses.Add(petClass);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(petClass);
        }
        // GET: PetClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetClass petClass = this.db.AllPetClasses.Find(id);
            if (petClass == null)
            {
                return this.HttpNotFound();
            }
            return this.View(petClass);
        }
        // POST: PetClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Edit([Bind(Include = "PetClassID,ClassName")] PetClass petClass)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(petClass).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(petClass);
        }
        // GET: PetClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetClass petClass = this.db.AllPetClasses.Find(id);
            if (petClass == null)
            {
                return this.HttpNotFound();
            }
            return this.View(petClass);
        }
        // POST: PetClasses/Delete/5
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PetClass petClass = this.db.AllPetClasses.Find(id);
            this.db.AllPetClasses.Remove(petClass);
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