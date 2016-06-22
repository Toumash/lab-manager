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
    public class ExamController : Controller
    {
        private readonly LabContext db;

        public ExamController()
        {
            this.db = new LabContext();
        }

        public ExamController(LabContext context)
        {
            this.db = context;
        }

        // GET: Badania
        public ActionResult Index()
        {
            return View(db.Badania.ToList());
        }

        // GET: Badania/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam badanie = db.Badania.Include(b => b.Patient).FirstOrDefault(p => p.Id == id);
            if (badanie == null)
            {
                return HttpNotFound();
            }
            return View(badanie);
        }

        // GET: Badania/Create
        public ActionResult Create(int? UserId)
        {
            if (UserId != null)
            {
                var pesel = db.Pacjenci.FirstOrDefault(p => p.ID == UserId.Value)?.PESEL;
                if (pesel != null)
                {
                    return View(new ExamCreateViewModel() {PESEL = pesel});
                }
            }
            return View();
        }

        // POST: Badania/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamCreateViewModel badanie)
        {
            if (ModelState.IsValid)
            {
                var patient = db.Pacjenci.FirstOrDefault(p => p.PESEL == badanie.PESEL);
                if (patient == null)
                {
                    badanie.IsExistingPesel = false;
                    return View(badanie);
                }
                var exam = new Exam()
                {
                    Name = badanie.Name,
                    Patient = patient,
                    Result = new ExamResult() {Complete = false},
                    Issued = DateTime.Now
                };
                db.Badania.Add(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(badanie);
        }

        // GET: Badania/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam badanie = db.Badania.Find(id);
            if (badanie == null)
            {
                return HttpNotFound();
            }
            return View(badanie);
        }

        // POST: Badania/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nazwa,Czas,Wyniki")] Exam badanie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(badanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(badanie);
        }

        // GET: Badania/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam badanie = db.Badania.Find(id);
            if (badanie == null)
            {
                return HttpNotFound();
            }
            return View(badanie);
        }

        // POST: Badania/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exam badanie = db.Badania.Find(id);
            db.Badania.Remove(badanie);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddResult(ExamResultAddViewModel wyniki)
        {
            if (ModelState.IsValid)
            {
                var exam = db.Badania.FirstOrDefault(b => b.Id == wyniki.ExamId);
                if (exam == null)
                {
                    return new HttpNotFoundResult("Brak badania z takim ID");
                }
                exam.Result = new ExamResult()
                {
                    Complete = wyniki.Complete,
                    Details = wyniki.Details,
                    ReadyTime = DateTime.Now
                };
                db.Entry(exam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wyniki);
        }

        public ActionResult AddResult(int? ExamId)
        {
            if (ExamId.HasValue)
            {
                var data = new ExamResultAddViewModel()
                {
                    Complete = false,
                    ExamId = ExamId.Value,
                };
                return View(data);
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }
    }
}