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
            return this.View(this.db.AllCostCategories.ToList());
        }
        // GET: CostCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCategories costCategories = this.db.AllCostCategories.Find(id);
            if (costCategories == null)
            {
                return this.HttpNotFound();
            }
            return this.View(costCategories);
        }
        // GET: CostCategories/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: CostCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CostID,CostBracket")] CostCategories costCategories)
        {
            if (this.ModelState.IsValid)
            {
                this.db.AllCostCategories.Add(costCategories);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(costCategories);
        }
        // GET: CostCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCategories costCategories = this.db.AllCostCategories.Find(id);
            if (costCategories == null)
            {
                return this.HttpNotFound();
            }
            return this.View(costCategories);
        }
        // POST: CostCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Edit([Bind(Include = "CostID,CostBracket")] CostCategories costCategories)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(costCategories).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(costCategories);
        }
        // GET: CostCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCategories costCategories = this.db.AllCostCategories.Find(id);
            if (costCategories == null)
            {
                return this.HttpNotFound();
            }
            return this.View(costCategories);
        }
        // POST: CostCategories/Delete/5
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CostCategories costCategories = this.db.AllCostCategories.Find(id);
            this.db.AllCostCategories.Remove(costCategories);
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