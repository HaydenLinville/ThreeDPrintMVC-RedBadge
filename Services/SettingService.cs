using Data;
using Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SettingService
    {
        private readonly Guid _userId;



        public SettingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSettingWPrint(SettingCreate model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var printer = ctx.Printers.Single(e => e.PrinterId == id);
                if (printer.HasHeatedBed == false)
                {

                    var entityNoBed = new Setting()
                    {
                        UserId = _userId,
                        CustomSettingName = model.CustomSettingName,
                        MaterialId = model.MaterialId,
                        PrinterId = id,
                        BedTemp = 0,
                        MaterialTemp = model.MaterialTemp,
                        Speed = model.Speed,
                    };
                    ctx.Settings.Add(entityNoBed);
                    return ctx.SaveChanges() >= 1;
                }
                else
                {
                    var entity = new Setting()
                    {
                        UserId = _userId,
                        CustomSettingName = model.CustomSettingName,
                        MaterialId = model.MaterialId,
                        PrinterId = id,
                        BedTemp = model.BedTemp,
                        MaterialTemp = model.MaterialTemp,
                        Speed = model.Speed,
                    };
                    ctx.Settings.Add(entity);
                    return ctx.SaveChanges() >= 1;
                }
            }
        }


        public bool CreateSetting(SettingCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var printer = ctx.Printers.Single(e => e.PrinterId == model.PrinterId);
                if (printer.HasHeatedBed == false)
                {
                    var entityNoBed = new Setting()
                    {
                        UserId = _userId,
                        CustomSettingName = model.CustomSettingName,
                        MaterialId = model.MaterialId,
                        PrinterId = model.PrinterId,
                        BedTemp = 0,
                        MaterialTemp = model.MaterialTemp,
                        Speed = model.Speed,
                    };
                    ctx.Settings.Add(entityNoBed);
                    return ctx.SaveChanges() == 1;
                }
                else
                {
                    var entity = new Setting()
                    {
                        UserId = _userId,
                        CustomSettingName = model.CustomSettingName,
                        MaterialId = model.MaterialId,
                        PrinterId = model.PrinterId,
                        BedTemp = model.BedTemp,
                        MaterialTemp = model.MaterialTemp,
                        Speed = model.Speed,
                    };
                    ctx.Settings.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }
        }





        public bool UpdateSetting(SettingEdit model, int printerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Settings.Single(e => e.SettingId == model.SettingId && e.UserId == _userId);

                entity.CustomSettingName = model.CustomSettingName;
                entity.MaterialId = model.MaterialId;
                entity.PrinterId = printerId;
                entity.MaterialTemp = model.MaterialTemp;
                entity.BedTemp = model.BedTemp;
                entity.Speed = model.Speed;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSetting(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var dsetting = ctx.Settings.Single(e => e.SettingId == id && e.UserId == _userId);

                ctx.Settings.Remove(dsetting);

                return ctx.SaveChanges() == 1;
            }
        }

        public SettingDetail GetSettingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var setDe = ctx.Settings.Single(e => e.SettingId == id && e.UserId == _userId);

                return new SettingDetail
                {
                    PrinterId = setDe.PrinterId,
                    CustomSettingName = setDe.CustomSettingName,
                    SettingId = setDe.SettingId,
                    PrinterModel = setDe.Printer.PrinterModel,
                    MaterialTypes = setDe.Material.MaterialType,
                    Color = setDe.Material.Color,
                    MaterialTemp = setDe.MaterialTemp,
                    BedTemp = setDe.BedTemp,
                    Speed = setDe.Speed,
                    Printer = setDe.Printer.PrinterBrand + " " + setDe.Printer.PrinterModel,
                    Material = setDe.Material.MaterialBrand + " " + setDe.Material.MaterialType.ToString() + " " + setDe.Material.Color
                };
            }
        }

    }
}
