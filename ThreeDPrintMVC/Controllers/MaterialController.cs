using Microsoft.AspNet.Identity;
using Models.Material;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThreeDPrintMVC.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        // GET: Material
        public ActionResult Index()
        {
            var srv = MService();
            var model = srv.GetMaterials();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MaterialCreate model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var srv = MService();

            if(srv.CreateMaterial(model))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var srv = MService();
            var entity = srv.GetMaterialById(id);
            return View(entity);
        }

        public ActionResult Edit(int id)
        {
            var srv = MService();
            var entity = srv.GetMaterialById(id);
            return View(entity);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MaterialEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var svr = MService();

            if (svr.UpdateMaterial(model))
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var srv = MService();
            var model = srv.GetMaterialById(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMaterial(int id)
        {
            var srv = MService();
            srv.DeleteMaterial(id);
            return RedirectToAction("Index");
        }

        private MaterialService MService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var srv = new MaterialService(userId);
            return srv;

        }

    }
}