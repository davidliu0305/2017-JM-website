using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JMWebsite.DAL.JMEntities;
using JMWebsite.Models;
using JMWebsite.Infrastructure;

namespace JMWebsite.Controllers
{
    [CustomAuthorize]
    public class ContractsController : Controller
    {
        private JohnMEntities db = new JohnMEntities();

        // GET: Contracts
        public ActionResult Index()
        {
            var contracts = db.Contracts.Include(c => c._event);
            return View("Index", contracts.ToList());
        }

        // GET: Contracts/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contract contract = db.Contracts.Find(id);
        //    if (contract == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contract);
        //}

        // GET: Contracts/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "ID", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,ContractName")] Contract contract, HttpPostedFileBase contractPDF)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (contractPDF != null)
                    {
                        string name = contractPDF.FileName;
                        if (contractPDF.FileName.ToLower().Contains("pdf"))
                        {
                            contractPDF.SaveAs(HttpContext.Server.MapPath("~/ContractFiles/")
                                                              + contractPDF.FileName);
                            contract.ContractName = contractPDF.FileName;
                            db.Contracts.Add(contract);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["notice"] = "Only pdf file types are allowed to be uploaded for contracts!";
                        }
                    }
                    else
                    {
                        TempData["notice"] = "Contract file must be uploaded!";
                    }
                }
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("IX_"))
                {
                    ModelState.AddModelError("Name", "Unable to save changes. Remember, you cannot upload the same file twice or another file using the same file name.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            ViewBag.EventID = new SelectList(db.Events, "ID", "Name", contract.EventID);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contract contract = db.Contracts.Find(id);
        //    if (contract == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.EventID = new SelectList(db.Events, "ID", "Name", contract.EventID);
        //    return View(contract);
        //}

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "EventID,Approved,ContractName")] Contract contract)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(contract).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EventID = new SelectList(db.Events, "ID", "Name", contract.EventID);
        //    return View(contract);
        //}

        // GET: Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            string fullPath = Request.MapPath("~/ContractFiles/" + contract.ContractName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.Contracts.Remove(contract);
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
