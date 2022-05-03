using Data;
using Models.PrinterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PrinterService
    {
        private readonly Guid _userId;

        public PrinterService(Guid userId)
        {
              _userId = userId;
        }

        public bool CreatePrinter(PrinterCreate model)
        {
            var entity = new Printer() { UserId = _userId, PrinterBrand = model.PrinterBrand, PrinterModel = model.PrinterModel, CanAutoLevel = model.CanAutoLevel, HasDualExtruder = model.HasDualExtruder, HasHeatedBed = model.HasHeatedBed, };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Printers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public PrinterDetail GetPrinterById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Printers.Single(e => e.PrinterId == id && e.UserId == _userId);

                return new PrinterDetail
                {
                    PrinterId = entity.PrinterId,
                    PrinterBrand = entity.PrinterBrand,
                    PrinterModel = entity.PrinterModel,
                    CanAutoLevel = entity.CanAutoLevel,
                    HasDualExtruder = entity.HasDualExtruder,
                    HasHeatedBed = entity.HasHeatedBed,
                    Settings = entity.Settings.Select(i => new Models.SettingModels.SettingPrinterDisplay
                    {
                       
                        CustomSettingName = i.CustomSettingName,
                        SettingId = i.SettingId,
                        BedTemp = i.BedTemp,
                        MaterialTemp = i.MaterialTemp,
                        Speed = i.Speed,
                        MaterialType = i.Material.MaterialType,
                        Color = i.Material.Color,

                    }).ToList(),
                };

            }
        }

        public IEnumerable<PrinterListItem> GetPrinters()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var quary = ctx.Printers.Where(e => e.UserId == _userId).Select(e=>new PrinterListItem
                {
                    PrinterId = e.PrinterId,
                    PrinterBrand = e.PrinterBrand,
                    PrinterModel = e.PrinterModel,
                });

                return quary.ToArray();
            }
        }
        
        public bool UpdatePrinter(PrinterEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Printers.Single(e => e.UserId == _userId && e.PrinterId == model.PrinterId);

                model.PrinterBand = entity.PrinterBrand;
                model.PrinterModel = entity.PrinterModel;
                model.HasHeatedBed = entity.HasHeatedBed;
                model.HasDualExtruder = entity.HasDualExtruder;
                model.CanAutoLevel = entity.CanAutoLevel;

                //check 

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePrinter(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Printers.Single(e => e.UserId == _userId && e.PrinterId == id);

                ctx.Printers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }

        }

    }
}
