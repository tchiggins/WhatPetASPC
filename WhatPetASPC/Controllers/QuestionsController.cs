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
            this.ViewBag.SpeciesID = new SelectList(this.db.AllSpecies, "SpeciesID", "SpeciesName");
            return this.View(this.db.AllQuestions.ToList());
        }

        [HttpPost]
        public ActionResult SelectorResult( string Q0Val, string Q1Val, string Q2Val, string Q3Val, string Q4Val, string Q5Val, string Q6Val, string Q7Val, string Q8Val, string Q9Val, string Q10Val, string Q10ValPet, string Q11Val, string Q11ValPet)
        {
            this.TempData["Q0"] = Q0Val;
            this.TempData["Q1"] = Q1Val;
            this.TempData["Q2"] = Q2Val;
            this.TempData["Q3"] = Q3Val;
            this.TempData["Q4"] = Q4Val;
            this.TempData["Q5"] = Q5Val;
            this.TempData["Q6"] = Q6Val;
            this.TempData["Q7"] = Q7Val;
            this.TempData["Q8"] = Q8Val;
            this.TempData["Q9"] = Q9Val;
            this.TempData["Q10"] = Q10Val;
            this.TempData["Q10Value"] = Q10ValPet;
            this.TempData["Q11"] = Q11Val;
            this.TempData["Q11Value"] = Q11ValPet;

            return this.RedirectToAction("Results", "PetTypes");
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