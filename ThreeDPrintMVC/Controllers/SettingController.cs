using Microsoft.AspNet.Identity;
using Models.SettingModels;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThreeDPrintMVC.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var ps = CreatePrinterService();
            var ms = MService();
            ViewBag.PrinterId = new SelectList(ps.GetPrinters(), "PrinterId", "PrinterBrand");
            ViewBag.MaterialId = new SelectList(ms.GetMaterials(), "MaterialId", "MaterialType");
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SettingCreate model)
        {
            var ps = CreatePrinterService();
            var ms = MService();
            ViewBag.PrinterId = new SelectList(ps.GetPrinters(), "PrinterId", "PrinterBrand");
            ViewBag.MaterialId = new SelectList(ms.GetMaterials(), "MaterialId", "MaterialType");

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var src = SService();

            if(src.CreateSetting(model))
            {
                TempData["SettingSave"] = $"{model.CustomSettingName} was created!";
                return RedirectToAction("Detail", "Printer", new {id =model.PrinterId});
                
            }
            ModelState.AddModelError("", "Setting could not be created");

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var ps = CreatePrinterService();
            var ms = MService();
            ViewBag.PrinterId = new SelectList(ps.GetPrinters(), "PrinterId", "PrinterBrand");
            ViewBag.MaterialId = new SelectList(ms.GetMaterials(), "MaterialId", "MaterialType");

            var srv = SService();
            var setting = srv.GetSettingById(id);
            var model = new SettingEdit { CustomSettingName=setting.CustomSettingName, BedTemp = setting.BedTemp, MaterialId = setting.MaterialId, PrinterId = setting.PrinterId, MaterialTemp = setting.MaterialTemp, SettingId = setting.SettingId, Speed = setting.Speed };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SettingEdit model)
        {
            var ps = CreatePrinterService();
            var ms = MService();
            ViewBag.PrinterId = new SelectList(ps.GetPrinters(), "PrinterId", "PrinterBrand");
            ViewBag.MaterialId = new SelectList(ms.GetMaterials(), "MaterialId", "MaterialType");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(id != model.SettingId)
            {
                ModelState.AddModelError("", "Id does not match");
                return View(model);
            }

            var srv = SService();
            if(srv.UpdateSetting(model))
            {
                return RedirectToAction("Detail", "Printer", new { id = model.PrinterId });
            }
            ModelState.AddModelError("", "Setting could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var srv = SService();
            var model = srv.GetSettingById(id);
            return View(model);

        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSetting(int id)
        {
            var srv = SService();
            var model = srv.GetSettingById(id);
            var name = model.CustomSettingName;
            var printerId = model.PrinterId;
            srv.DeleteSetting(id);
            TempData["SettingSave"] = $"{name} Deleted!";
            return RedirectToAction("Detail", "Printer", new {id = printerId});
        }

        public ActionResult Detail(int id)
        {
            var srv = SService();
            var setting =srv.GetSettingById(id);
            return View(setting);
            //return RedirectToAction("Index", "PrinterController");
        }

        public SettingService SService()
        {
            var user = Guid.Parse(User.Identity.GetUserId());
            var srv = new SettingService(user);
            return srv;
        }

        private PrinterService CreatePrinterService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PrinterService(userId);
            return service;
        }
        private MaterialService MService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var srv = new MaterialService(userId);
            return srv;

        }

    }
}