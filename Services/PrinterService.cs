using Data;
using Models.PrinterModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class PrinterService
    {
        private readonly Guid _userId;

        public PrinterService(Guid userId)
        {
              _userId = userId;
        }

        public bool CreatePrinter(HttpPostedFileBase file, PrinterCreate model)
        {
            model.Image = ConvertToBytes(file);
            var entity = new Printer() { UserId = _userId, PrinterBrand = model.PrinterBrand, PrinterModel = model.PrinterModel, CanAutoLevel = model.CanAutoLevel, HasDualExtruder = model.HasDualExtruder, HasHeatedBed = model.HasHeatedBed, Image = model.Image };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Printers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public byte[] GetImageFromDataBase(int Id)
        {
            using(var ctx = new ApplicationDbContext())
            {

            var q = from temp in ctx.Printers where temp.PrinterId == Id select temp.Image;
            byte[] cover = q.First();
                return cover;
            }
        }
        //public int UploadImageInDataBase(HttpPostedFileBase file, PrinterCreate model )
        //{
        //    model.Image = ConvertToBytes(file);
        //    var Printer = new Printer { }
        //}

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
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
                    Image = entity.Image,
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

                entity.PrinterBrand = model.PrinterBand;
                entity.PrinterModel = model.PrinterModel;
                entity.HasHeatedBed = model.HasHeatedBed;
                entity.HasDualExtruder = model.HasDualExtruder;
                entity.CanAutoLevel = model.CanAutoLevel;

                

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
