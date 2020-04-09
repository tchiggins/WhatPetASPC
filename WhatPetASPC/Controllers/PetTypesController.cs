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
            string  QPetsAllowedVal, QGardenVal, QAllergiesVal, QAllergiesbVal, QPurchaseCostVal, QMonthlyCostVal,
                    QHoursNeededVal, QSizeVal, QSpeciesWantedVal, QSpeciesWantedbVal;

            QPetsAllowedVal =       this.TempData["QPetsAllowedVal"].ToString();
            QGardenVal =            this.TempData["QGardenVal"].ToString();
            QAllergiesVal =         this.TempData["QAllergiesVal"].ToString();
            QAllergiesbVal =        this.TempData["QAllergiesbVal"].ToString();
            QPurchaseCostVal =      this.TempData["QPurchaseCostVal"].ToString();
            QMonthlyCostVal =       this.TempData["QMonthlyCostVal"].ToString();
            QHoursNeededVal =       this.TempData["QHoursNeededVal"].ToString();
            QSizeVal =              this.TempData["QSizeVal"].ToString();
            QSpeciesWantedVal =     this.TempData["QSpeciesWantedVal"].ToString();
            QSpeciesWantedbVal =    this.TempData["QSpeciesWantedbVal"].ToString();

            // Should we issue a warning?
            if (QPetsAllowedVal == "No")
            {
                this.ViewBag.WarningMessage = "Please check with the property owner or agent if pets are allowed";
            }
            else
            {
                this.ViewBag.WarningMessage = string.Empty;
            }

            // Select the list of items that match the requests
            IQueryable<PetType> petTypeList = this.db.AllPetTypes
                                .Where(c =>

                                // If the pet needs outdoor space do we have that available?
                                ((c.Outdoors == "TRUE" && QGardenVal == "Yes") || (c.Outdoors == "FALSE")) &&

                                // Do we have any allergies to this species of pet?
                                (((QAllergiesVal == "Yes") && (c.SpeciesID.ToString() != QAllergiesbVal)) || (QAllergiesVal == "No")) &&

                                // Is the purchase cost in our range?
                                ((c.PurchaseCost == QPurchaseCostVal) || (QPurchaseCostVal == "Any")) &&

                                // Is the monthly cost in our range?
                                ((c.MonthlyCost == QMonthlyCostVal) || (QMonthlyCostVal == "Any")) &&

                                // Are the hours needed in our range?
                                ((c.HoursNeeded == QHoursNeededVal) || (QHoursNeededVal == "Any")) &&

                                // Does the pet fit our size critera?
                                ((c.Size == QSizeVal) || (QSizeVal  == "Any")) &&

                                // Have we specified that we want this particular species of pet?
                                (((QSpeciesWantedVal == "Yes") && (c.SpeciesID.ToString() == QSpeciesWantedbVal)) || (QSpeciesWantedVal == "No"))

                                )

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
        public ActionResult SelectorResult(string QPetsAllowedVal, string QGardenVal, string QAllergiesVal,
                                            string QAllergiesbVal, string QPurchaseCostVal, string QMonthlyCostVal,
                                            string QHoursNeededVal, string QSizeVal, string QSpeciesWantedVal,
                                            string QSpeciesWantedbVal)
        {
            // Put the answers in the TempData area so they can be shared with the PetTypes controller
            this.TempData["QPetsAllowedVal"] = QPetsAllowedVal;
            this.TempData["QGardenVal"] = QGardenVal;
            this.TempData["QAllergiesVal"] = QAllergiesVal;
            this.TempData["QAllergiesbVal"] = QAllergiesbVal;
            this.TempData["QPurchaseCostVal"] = QPurchaseCostVal;
            this.TempData["QMonthlyCostVal"] = QMonthlyCostVal;
            this.TempData["QHoursNeededVal"] = QHoursNeededVal;
            this.TempData["QSizeVal"] = QSizeVal;
            this.TempData["QSpeciesWantedVal"] = QSpeciesWantedVal;
            this.TempData["QSpeciesWantedbVal"] = QSpeciesWantedbVal;

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
        public ActionResult Create([Bind(Include = "PetTypeID,TypeName,Size,Outdoors,PurchaseCost,MonthlyCost,HoursNeeded,PetImage,SpeciesID")] PetType petType)
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
        public ActionResult Edit([Bind(Include = "PetTypeID,TypeName,Size,Outdoors,PurchaseCost,MonthlyCost,HoursNeeded,PetImage,SpeciesID")] PetType petType)
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
        [HttpGet]
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