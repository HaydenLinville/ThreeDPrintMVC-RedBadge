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

            HttpPostedFileBase file = Request.Files["ImageData"];
            var srv = MService();

            if(srv.CreateMaterial(file,model))
            {
                TempData["SaveResult"] = $"{model.MaterialBrand} Created!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult RetrieveMImage(int id)
        {
            var mService = MService();
            byte[] cover = mService.GetImageFromDataBase(id);
            
            if(cover!= null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }

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
            var edit = srv.GetMaterialById(id);
            var model = new MaterialEdit { MaterialId = edit.MaterialId, Color = edit.Color, MaterialBrand = edit.MaterialBrand, MaterialType = edit.MaterialType };
            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MaterialEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(id != model.MaterialId)
            {
                ModelState.AddModelError("", "Id Mismatch");
                    return View(model); }

            var svr = MService();

            if (svr.UpdateMaterial(model))
            {
                TempData["SaveResult"] = $"{model.MaterialBrand} Updated!";
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
            var model = srv.GetMaterialById(id);
            var name = model.MaterialBrand;
            srv.DeleteMaterial(id);
            TempData["SaveResult"] = $"{name} Deleted!";
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