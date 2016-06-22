using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "mod,admin")]
    public class PatientController : Controller
    {
        private readonly LabContext db;

        public PatientController(LabContext db)
        {
            this.db = db;
        }

        public PatientController()
        {
            this.db = new LabContext();
        }

        // GET: Pacjents
        public ActionResult Index()
        {
            return View(db.Pacjenci.ToList());
        }

        // GET: Pacjents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pacjent = db.Pacjenci.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }


        // GET: Pacjents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pacjents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientCreateViewModel patientData)
        {
            if (ModelState.IsValid)
            {
                var dbPatient = db.Pacjenci.FirstOrDefault(p => p.PESEL == patientData.PESEL.Trim());
                if (dbPatient != null)
                {
                    patientData.AlreadyExists = true;
                    patientData.ActualPatient = dbPatient;
                    return View(patientData);
                }
                var newPatient = new Patient()
                {
                    LastName = patientData.LastName,
                    Name = patientData.Name,
                    PESEL = patientData.PESEL
                };
                db.Pacjenci.Add(newPatient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientData);
        }

        // GET: Pacjents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pacjent = db.Pacjenci.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            var data = new PatientEditViewModel()
            {
                ID = pacjent.ID,
                LastName = pacjent.LastName,
                Name = pacjent.Name
            };
            return View(data);
        }

        // POST: Pacjents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientEditViewModel pacjent)
        {
            if (ModelState.IsValid)
            {
                var newData = new Patient()
                {
                    ID = pacjent.ID,
                    LastName = pacjent.LastName,
                    Name = pacjent.Name
                };
                db.Entry(newData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pacjent);
        }

        // GET: Pacjents/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient pacjent = db.Pacjenci.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }

        // POST: Pacjents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient pacjent = db.Pacjenci.Find(id);
            db.Pacjenci.Remove(pacjent);
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