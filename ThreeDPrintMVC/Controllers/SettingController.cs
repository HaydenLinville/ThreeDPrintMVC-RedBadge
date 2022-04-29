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
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SettingCreate model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var src = SService();

            if(src.CreateSetting(model))
            {
                return RedirectToAction("Detail", "PrinterController", new {id =model.PrinterId});
                //play around with later to figure out how to get to details with the int id. 
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var srv = SService();
            var setting = srv.GetSettingById(id);
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SettingEdit model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var srv = SService();
            if(srv.UpdateSetting(model))
            {
                return RedirectToAction("Detail", new {id = model.SettingId});
            }

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var srv = SService();
            var model = srv.GetSettingById(id);
            return View();

        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSetting(int id)
        {
            var srv = SService();
            srv.DeleteSetting(id);

            return RedirectToAction("Index", "PrinterController");
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
    }
}