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
    
    public class PrinterController : Controller
    {
        // GET: Printer
        [Authorize]
        public ActionResult Index()
        {
            var service = CreatePrinterService();
            var model = service.GetPrinters();
            return View(model);
        }

        public ActionResult Info()
        {
            return View();
        }


        public ActionResult Default()
        {
            var ps = new PrinterService();
            var model = ps.GetPrintersSeed();
            return View(model);
        }

        public ActionResult DefaultDetail(int id)
        {
            var ps = new PrinterService();
            var model = ps.GetPrinterByIdSeed(id);

            return View(model);
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrinterCreate model)
        {
           

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HttpPostedFileBase file = Request.Files["ImageData"];
            var service = CreatePrinterService();
            
              
            if(service.CreatePrinter(file,model))
            {
                TempData["SaveResult"] = $"{model.PrinterModel} was created!";
                
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Printer could not be created.");

            return View(model);

        }

        public ActionResult RetrieveImage(int id)
        {
            var s = new PrinterService();
            byte[] cover = s.GetImageFromDataBase(id);
            if(cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        public ActionResult Detail(int id)
        {
            var service = CreatePrinterService();

            var entity = service.GetPrinterById(id);

            return View(entity);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var srv = CreatePrinterService();

            var detailModel = srv.GetPrinterById(id);
            var editModel = new PrinterEdit { PrinterId = detailModel.PrinterId, CanAutoLevel = detailModel.CanAutoLevel, PrinterModel = detailModel.PrinterModel, HasDualExtruder = detailModel.HasDualExtruder, HasHeatedBed = detailModel.HasHeatedBed, PrinterBand = detailModel.PrinterBrand, CanUpgrade = detailModel.CanUpgrade, HasCamera = detailModel.HasCamera, HasWifi = detailModel.HasWifi, Image = detailModel.Image };
            //has not added img 
            return View(editModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PrinterEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(id!=model.PrinterId)
            {
                ModelState.AddModelError("", "Id does not match");
                return View(model);
            }
            HttpPostedFileBase file = Request.Files["ImageData"];
            var srv = CreatePrinterService();

            if (srv.UpdatePrinter(file,model))
            {
                TempData["SaveResult"] = $"{model.PrinterModel} was Updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Nothing has been changed. Change something and save or cancel.");

            return View(model);

        }
        [Authorize]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var srv = CreatePrinterService();

            var model = srv.GetPrinterById(id);

            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePrinter(int id)
        {
            var srv = CreatePrinterService();

            srv.DeletePrinter(id);
            TempData["SaveResult"] = "Printer Deleted!";
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