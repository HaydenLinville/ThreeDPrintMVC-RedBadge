using Data;
using Models.Material;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class MaterialService
    {
        private readonly Guid _userId;

        public MaterialService()
        {

        }

        public MaterialService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMaterial(HttpPostedFileBase file, MaterialCreate model)
        {
            model.Image = ConvertToBytes(file);

            var entity = new Material()
            {
                UserId = _userId,
                MaterialType = model.MaterialType,
                Color = model.Color,
                MaterialBrand = model.MaterialBrand,
                Image = model.Image

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Materials.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MaterialListSettingItem> MaterialSelectList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var material = ctx.Materials.ToList();
                IEnumerable<MaterialListSettingItem> mList = from s in material select new MaterialListSettingItem { MaterialId = s.MaterialId, MaterialInfo = s.MaterialBrand + " " + s.MaterialType.ToString() + " " + s.Color  };

                return mList;
            }
        }

        public byte[] GetImageFromDataBase(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var q = from temp in ctx.Materials where temp.MaterialId == id select temp.Image;
                byte[] cover = q.First();
                return cover;
            }
        }


        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }


        public MaterialDetail GetMaterialById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var material = ctx.Materials.Single(e => e.UserId == _userId && e.MaterialId == id);

                return new MaterialDetail { MaterialId = material.MaterialId, MaterialBrand = material.MaterialBrand, Color = material.Color, MaterialType = material.MaterialType, Image = material.Image };
            }
        }

        public MaterialDetail GetMaterialByIdSeed(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                byte[] bytes = new byte[16];
                BitConverter.GetBytes(00000000 - 0000 - 0000 - 0000 - 000000000000).CopyTo(bytes, 0);
                var nope = new Guid(bytes);



                var material = ctx.Materials.Single(e => e.UserId == nope && e.MaterialId == id);

                return new MaterialDetail { MaterialId = material.MaterialId, MaterialBrand = material.MaterialBrand, Color = material.Color, MaterialType = material.MaterialType, Image = material.Image };
            }
        }

        public IEnumerable<MaterialListItem> GetMaterialsSeed()
        {
            using (var ctx = new ApplicationDbContext())
            {
                byte[] bytes = new byte[16];
                BitConverter.GetBytes(00000000 - 0000 - 0000 - 0000 - 000000000000).CopyTo(bytes, 0);
                var nope = new Guid(bytes);

                var quary = ctx.Materials.Where(e => e.UserId == nope).Select(e => new MaterialListItem { MaterialBrand = e.MaterialBrand, MaterialId = e.MaterialId, Color = e.Color, MaterialType = e.MaterialType, Image = e.Image });

                return quary.ToArray();
            }
        }



        public IEnumerable<MaterialListItem> GetMaterials()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var quary = ctx.Materials.Where(e => e.UserId == _userId).Select(e => new MaterialListItem { MaterialBrand = e.MaterialBrand, MaterialId = e.MaterialId, Color = e.Color, MaterialType = e.MaterialType, Image = e.Image });

                return quary.ToArray();
            }
        }




        public bool UpdateMaterial(HttpPostedFileBase file, MaterialEdit model)
        {
            model.Image = ConvertToBytes(file);
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Materials.Single(e => e.UserId == _userId && e.MaterialId == model.MaterialId);

                entity.Color = model.Color;
                entity.MaterialBrand = model.MaterialBrand;
                entity.MaterialType = model.MaterialType;
                entity.Image = model.Image;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMaterial(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Materials.Single(e => e.MaterialId == id && e.UserId == _userId);
                ctx.Materials.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
