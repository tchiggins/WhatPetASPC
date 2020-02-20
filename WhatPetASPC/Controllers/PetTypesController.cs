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

        public ActionResult Results()
        {
            // Get the answers that have been put in the TempData area from the Questions Controller
            string Q0, Q1, Q2, Q3, Q3b, Q4, Q4b, Q5=null, Q6, Q7, Q8, Q9, Q9b, Q10, Q10b;
            if (this.TempData["Q0"] != null)
            {
                Q0 = this.TempData["Q0"].ToString();
            }
            if (this.TempData["Q1"] != null)
            {
                Q1 = this.TempData["Q1"].ToString();
            }
            if (this.TempData["Q2"] != null)
            {
                Q2 = this.TempData["Q2"].ToString();
            }
            if (this.TempData["Q3"] != null)
            {
                Q3 = this.TempData["Q3"].ToString();
            }
            if (this.TempData["Q3b"] != null)
            {
                Q3b = this.TempData["Q3b"].ToString();
            }
            if (this.TempData["Q4"] != null)
            {
                Q4 = this.TempData["Q4"].ToString();
            }
            if (this.TempData["Q4b"] != null)
            {
                Q4b = this.TempData["Q4b"].ToString();
            }
            if (this.TempData["Q5"] != null)
            {
                Q5 = this.TempData["Q5"].ToString();
            }
            if (this.TempData["Q6"] != null)
            {
                Q6 = this.TempData["Q6"].ToString();
            }
            if (this.TempData["Q7"] != null)
            {
                Q7 = this.TempData["Q7"].ToString();
            }
            if (this.TempData["Q8"] != null)
            {
                Q8 = this.TempData["Q8"].ToString();
            }
            if (this.TempData["Q9"] != null)
            {
                Q9 = this.TempData["Q9"].ToString();
            }
            if (this.TempData["Q9b"] != null)
            {
                Q9b = this.TempData["Q9b"].ToString();
            }
            if (this.TempData["Q10"] != null)
            {
                Q10 = this.TempData["Q10"].ToString();
            }
            if (this.TempData["Q10b"] != null)
            {
                Q10b = this.TempData["Q10b"].ToString();
            }

            // Select the list of items that match the pulldown or all if All is selected
            IQueryable<PetType> petTypeList = this.db.AllPetTypes
                                .Where(c => c.PetCost == Q5)
                                .OrderBy(d => d.SpeciesID);
            var sql = petTypeList.ToString();

            // Now run the algo to work out which pets fit the bill
            return this.View(petTypeList.ToList());
        }

        // Show the pet selector
        public ActionResult Selector()
        {
            // Add the All type to the species list for the pulldown menu
            var AllSpecies = new Species
            {
                SpeciesName = null,
                SpeciesID = 0
            };
            List<Species> MyList = new List<Species>
            {
                AllSpecies
            };
            var MySpecies = this.db.AllSpecies.OrderBy(q => q.SpeciesName).ToList();
            MyList.AddRange(MySpecies);
            this.ViewBag.SpeciesID = new SelectList(MyList, "SpeciesID", "SpeciesName");
            return this.View();
        }

        [HttpPost]
        public ActionResult SelectorResult(string Q0Val, string Q1Val, string Q2Val, string Q3Val, string Q3bVal, string Q4Val, string Q4bVal, string Q5Val, string Q6Val, string Q7Val, string Q8Val, string Q9Val, string Q9bVal, string Q10Val, string Q10bVal)
        {
            // Put the answers in the TempData area so they can be shared with the PetTypes controller
            this.TempData["Q0"] = Q0Val;
            this.TempData["Q1"] = Q1Val;
            this.TempData["Q2"] = Q2Val;
            this.TempData["Q3"] = Q3Val;
            this.TempData["Q3b"] = Q3bVal;
            this.TempData["Q4"] = Q4Val;
            this.TempData["Q4b"] = Q4bVal;
            this.TempData["Q5"] = Q5Val;
            this.TempData["Q6"] = Q6Val;
            this.TempData["Q7"] = Q7Val;
            this.TempData["Q8"] = Q8Val;
            this.TempData["Q9"] = Q9Val;
            this.TempData["Q9b"] = Q9bVal;
            this.TempData["Q10"] = Q9bVal;
            this.TempData["Q10b"] = Q10bVal;

            return this.RedirectToAction("Results", "PetTypes");
        }

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
        [HttpGet]
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
        [HttpPost]
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