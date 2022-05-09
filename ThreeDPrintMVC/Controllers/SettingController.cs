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

        public ActionResult CreateWPrint(int id)
        {
            var ps = CreatePrinterService();
            var ms = MService();
            var mSelectList = ms.MaterialSelectList();

            if(ms.GetMaterials().Count() == 0)
            {
                TempData["NoMaterial"] = "You need to have at least one material created before adding Custom Settings";
                return RedirectToAction("Create", "Material"); 
            }

            var n = ps.GetPrinterById(id);
            ViewBag.PrinterBrand = n.PrinterBrand;
            ViewBag.PrinterId = n.PrinterId;
            ViewBag.MaterialId = new SelectList(mSelectList, "MaterialId", "MaterialInfo");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWPrint(SettingCreate model, int id)
        {
            var ps = CreatePrinterService();
            var ms = MService();
            var mSelectList = ms.MaterialSelectList();


            var printer = ps.GetPrinterById(id);
            var pId = printer.PrinterId;
            ViewBag.PrinterId = printer.PrinterId;
            ViewBag.MaterialId = new SelectList(mSelectList, "MaterialId", "MaterialInfo");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var src = SService();

            if (src.CreateSettingWPrint(model, pId))
            {
                TempData["SettingSave"] = $"{model.CustomSettingName} was created!";
                return RedirectToAction("Detail", "Printer", new { id = pId });

            }
            ModelState.AddModelError("", "Setting could not be created");

            return View(model);
        }

        public ActionResult Create()
        {
            var ps = CreatePrinterService();
            var ms = MService();
            var mSelectList = ms.MaterialSelectList();
            var pSelectList = ps.PrinterSelectList();
            if(ps.GetPrinters().Count()==0)
            {
                TempData["NoPrinter"] = "You need to have at least one printer to create Custom Settings";
                return RedirectToAction("Create", "Printer");
            }

            if (ms.GetMaterials().Count() == 0)
            {
                TempData["NoMaterial"] = "You need to have at least one material created before adding Custom Settings";
                return RedirectToAction("Create", "Material");
            }


            ViewBag.MaterialId = new SelectList(mSelectList, "MaterialId", "MaterialInfo");
            ViewBag.PrinterId = new SelectList(pSelectList, "PrinterId", "PrinterInfo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SettingCreate model)
        {
            var ps = CreatePrinterService();
            var ms = MService();
            var mSelectList = ms.MaterialSelectList();
            var pSelectList = ps.PrinterSelectList();
            ViewBag.MaterialId = new SelectList(mSelectList, "MaterialId", "MaterialInfo");
            ViewBag.PrinterId = new SelectList(pSelectList, "PrinterId", "PrinterInfo");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var src = SService();

            if (src.CreateSetting(model))
            {
                return RedirectToAction("Detail", "Printer", new { id = model.PrinterId });
                
            }
            return View(model);
        }

        public ActionResult Edit(int settingId, int printerId)
        {
            var ps = CreatePrinterService();
            var ms = MService();
            var mSelectList = ms.MaterialSelectList();
            var pSelectList = ps.PrinterSelectList();
            ViewBag.PrinterId = new SelectList(pSelectList, "PrinterId", "PrinterInfo");
            ViewBag.MaterialId = new SelectList(mSelectList, "MaterialId", "MaterialInfo");

            var srv = SService();
            var setting = srv.GetSettingById(settingId);
            var model = new SettingEdit 
            { 
                CustomSettingName = setting.CustomSettingName, 
                BedTemp = setting.BedTemp, 
                MaterialId = setting.MaterialId, 
                PrinterId = setting.PrinterId, 
                MaterialTemp = setting.MaterialTemp, 
                SettingId = setting.SettingId, 
                Speed = setting.Speed
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int settingId, int printerId, SettingEdit model)
        {
            var ps = CreatePrinterService();
            var ms = MService();
            var pSelectList = ps.PrinterSelectList();
            var mSelectList = ms.MaterialSelectList();
            ViewBag.PrinterId = new SelectList(pSelectList, "PrinterId", "PrinterInfo");
            ViewBag.MaterialId = new SelectList(mSelectList, "MaterialId", "MaterialInfo");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (settingId != model.SettingId)
            {
                ModelState.AddModelError("", "Id does not match");
                return View(model);
            }

            var srv = SService();
            if (srv.UpdateSetting(model, printerId))
            {
                TempData["SettingUpdate"] = $"You updated {model.CustomSettingName}";
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
            return RedirectToAction("Detail", "Printer", new { id = printerId });
        }

        public ActionResult Detail(int id)
        {
            var srv = SService();
            var setting = srv.GetSettingById(id);
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