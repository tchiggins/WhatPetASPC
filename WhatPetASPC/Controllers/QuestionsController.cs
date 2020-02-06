using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhatPetASPC.DAL;
using WhatPetASPC.Models;
namespace WhatPetASPC.Controllers
{
    public class QuestionsController : Controller
    {
        private PetDB db = new PetDB();
        // GET: Questions
        public ActionResult Index()
        {
            return this.View(this.db.AllQuestions.ToList());
        }
        public ActionResult Selector()
        {
            return this.View(this.db.AllQuestions.ToList());
        }
        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = this.db.AllQuestions.Find(id);
            if (questions == null)
            {
                return this.HttpNotFound();
            }
            return this.View(questions);
        }
        // GET: Questions/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }
        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "QuestionID,QuestionText")] Questions questions)
        {
            if (this.ModelState.IsValid)
            {
                this.db.AllQuestions.Add(questions);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(questions);
        }
        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = this.db.AllQuestions.Find(id);
            if (questions == null)
            {
                return this.HttpNotFound();
            }
            return this.View(questions);
        }
        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Edit([Bind(Include = "QuestionID,QuestionText")] Questions questions)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(questions).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(questions);
        }
        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = this.db.AllQuestions.Find(id);
            if (questions == null)
            {
                return this.HttpNotFound();
            }
            return this.View(questions);
        }
        // POST: Questions/Delete/5
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Questions questions = this.db.AllQuestions.Find(id);
            this.db.AllQuestions.Remove(questions);
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