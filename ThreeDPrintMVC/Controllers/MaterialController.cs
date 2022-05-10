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
    
    public class MaterialController : Controller
    {
        // GET: Material
        [Authorize]
        public ActionResult Index()
        {
            var srv = MService();
            var model = srv.GetMaterials();
            return View(model);
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Default()
        {
            var ms = new MaterialService();
            var model = ms.GetMaterialsSeed();
            return View(model);
        }

        public ActionResult DefaultDetail(int id)
        {
            var ms = new MaterialService();
            var model = ms.GetMaterialByIdSeed(id);

            return View(model);
        }



        [Authorize]
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

        public ActionResult Info()
        {
            return View();
        }

        
        public ActionResult RetrieveMImage(int id)
        {
            
            var mService = new MaterialService();
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

        [Authorize]
        public ActionResult Detail(int id)
        {
            var srv = MService();
            var entity = srv.GetMaterialById(id);
            return View(entity);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var srv = MService();
            var edit = srv.GetMaterialById(id);
            var model = new MaterialEdit { MaterialId = edit.MaterialId, Color = edit.Color, MaterialBrand = edit.MaterialBrand, MaterialType = edit.MaterialType, Image = edit.Image };
            return View(model);

        }

        [Authorize]
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

            HttpPostedFileBase file = Request.Files["ImageData"];
            var svr = MService();

            if (svr.UpdateMaterial(file,model))
            {
                TempData["SaveResult"] = $"{model.MaterialBrand} Updated!";
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var srv = MService();
            var model = srv.GetMaterialById(id);
            return View(model);
        }
        [Authorize]
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