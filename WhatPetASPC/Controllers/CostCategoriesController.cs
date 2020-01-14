using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhatPetASPC.DAL;
using WhatPetASPC.Models;
namespace WhatPetASPC.Controllers
{
    public class CostCategoriesController : Controller
    {
        private PetDB db = new PetDB();
        // GET: CostCategories
        public ActionResult Index()
        {
            return View(db.CostCategories.ToList());
        }
        // GET: CostCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCategories costCategories = db.CostCategories.Find(id);
            if (costCategories == null)
            {
                return HttpNotFound();
            }
            return View(costCategories);
        }
        // GET: CostCategories/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: CostCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CostID,CostBracket")] CostCategories costCategories)
        {
            if (ModelState.IsValid)
            {
                db.CostCategories.Add(costCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(costCategories);
        }
        // GET: CostCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCategories costCategories = db.CostCategories.Find(id);
            if (costCategories == null)
            {
                return HttpNotFound();
            }
            return View(costCategories);
        }
        // POST: CostCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CostID,CostBracket")] CostCategories costCategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(costCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(costCategories);
        }
        // GET: CostCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCategories costCategories = db.CostCategories.Find(id);
            if (costCategories == null)
            {
                return HttpNotFound();
            }
            return View(costCategories);
        }
        // POST: CostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CostCategories costCategories = db.CostCategories.Find(id);
            db.CostCategories.Remove(costCategories);
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