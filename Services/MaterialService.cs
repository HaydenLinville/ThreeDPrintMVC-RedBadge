using Data;
using Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MaterialService
    {
        private readonly Guid _userId;

        public MaterialService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMaterial(MaterialCreate model)
        {
            var entity = new Material()
            {
                UserId = _userId,
                MaterialType = model.MaterialType,
                Color = model.Color,
                MaterialBrand = model.MaterialBrand,

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Materials.Add(entity);

                return ctx.SaveChanges() == 1;
            }


        }

        public MaterialDetail GetMaterialById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var material = ctx.Materials.Single(e => e.UserId == _userId && e.MaterialId == id);

                return new MaterialDetail { MaterialId = material.MaterialId, MaterialBrand = material.MaterialBrand, Color = material.Color, MaterialType = material.MaterialType };
            }
        }

        public IEnumerable<MaterialListItem> GetMaterials()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var quary = ctx.Materials.Where(e => e.UserId == _userId).Select(e => new MaterialListItem { MaterialBrand = e.MaterialBrand, MaterialId = e.MaterialId, Color = e.Color, MaterialType = e.MaterialType });

                return quary.ToArray();
            }
        }


        

        public bool UpdateMaterial(MaterialEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Materials.Single(e => e.UserId == _userId && e.MaterialId == model.MaterialId);

                entity.Color = model.Color;
                entity.MaterialBrand = model.MaterialBrand;
                entity.MaterialType = model.MaterialType;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMaterial (int id)
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
