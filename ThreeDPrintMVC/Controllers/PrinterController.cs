using Microsoft.AspNet.Identity;
using Models.PrinterModels;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThreeDPrintMVC.Controllers
{
    [Authorize]
    public class PrinterController : Controller
    {
        // GET: Printer
        public ActionResult Index()
        {
            var service = CreatePrinterService();
            var model = service.GetPrinters();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrinterCreate model)
        {
            var service = CreatePrinterService();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
              
            if(service.CreatePrinter(model))
            {
                return RedirectToAction("Index");
            }


            return View(model);

        }



        public ActionResult Detail(int id)
        {
            var service = CreatePrinterService();

            var entity = service.GetPrinterById(id);

            return View(entity);
        }


        public ActionResult Edit(int id)
        {
            var srv = CreatePrinterService();

            var detailModel = srv.GetPrinterById(id);
            var editModel = new PrinterEdit { PrinterId = detailModel.PrinterId, CanAutoLevel = detailModel.CanAutoLevel, PrinterModel = detailModel.PrinterModel, HasDualExtruder = detailModel.HasDualExtruder, HasHeatedBed = detailModel.HasHeatedBed, PrinterBand = detailModel.PrinterBrand };

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PrinterEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var srv = CreatePrinterService();

            if (srv.UpdatePrinter(model))
            {
                return RedirectToAction("Index");
            }

            return View(model);

        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var srv = CreatePrinterService();

            var model = srv.GetPrinterById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePrinter(int id)
        {
            var srv = CreatePrinterService();

            srv.DeletePrinter(id);

            return RedirectToAction("Index");
        }


        private PrinterService CreatePrinterService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PrinterService(userId);
            return service;
        }

    }
}