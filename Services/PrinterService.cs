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

        public PrinterService()
        {

        }

        public PrinterService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePrinter(HttpPostedFileBase file, PrinterCreate model)
        {
            model.Image = ConvertToBytes(file);

            var entity = new Printer() { UserId = _userId, PrinterBrand = model.PrinterBrand, PrinterModel = model.PrinterModel, CanAutoLevel = model.CanAutoLevel, HasDualExtruder = model.HasDualExtruder, HasHeatedBed = model.HasHeatedBed, HasWifi = model.HasWifi, HasCamera = model.HasCamera, CanUpgrade = model.CanUpgrade, Image = model.Image };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Printers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public byte[] GetImageFromDataBase(int Id)
        {
            using (var ctx = new ApplicationDbContext())
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

        

        public IEnumerable<PrinterListSettingItem> PrinterSelectList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var printer = ctx.Printers.Where(e=> e.UserId == _userId).ToList();
                IEnumerable<PrinterListSettingItem> pList = from s in printer select new PrinterListSettingItem { PrinterId = s.PrinterId, PrinterInfo = s.PrinterBrand + " " + s.PrinterModel};

                return pList;
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
                    CanUpgrade = entity.CanUpgrade,
                    HasCamera = entity.HasCamera,
                    HasWifi = entity.HasWifi,
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
                        MaterialId= i.MaterialId

                    }).ToList(),
                };

            }
        }

        public PrinterDetail GetPrinterByIdSeed(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                byte[] bytes = new byte[16];
                BitConverter.GetBytes(00000000 - 0000 - 0000 - 0000 - 000000000000).CopyTo(bytes, 0);
                var nope = new Guid(bytes);


                var entity = ctx.Printers.Single(e => e.PrinterId == id && e.UserId == nope);

                return new PrinterDetail
                {
                    PrinterId = entity.PrinterId,
                    PrinterBrand = entity.PrinterBrand,
                    PrinterModel = entity.PrinterModel,
                    CanAutoLevel = entity.CanAutoLevel,
                    HasDualExtruder = entity.HasDualExtruder,
                    HasHeatedBed = entity.HasHeatedBed,
                    CanUpgrade = entity.CanUpgrade,
                    HasCamera = entity.HasCamera,
                    HasWifi = entity.HasWifi,
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
                        MaterialId = i.MaterialId

                    }).ToList(),
                };

            }
        }



        public IEnumerable<PrinterListItem> GetPrintersSeed()
        {
            using (var ctx = new ApplicationDbContext())
            {
                byte[] bytes = new byte[16];
                BitConverter.GetBytes(00000000 - 0000 - 0000 - 0000 - 000000000000).CopyTo(bytes, 0);
                var nope = new Guid(bytes);

                var quary = ctx.Printers.Where(e => e.UserId == nope).Select(e => new PrinterListItem
                {
                    PrinterId = e.PrinterId,
                    PrinterBrand = e.PrinterBrand,
                    PrinterModel = e.PrinterModel,
                    Image = e.Image,
                });

                return quary.ToArray();
            }
        }

        public IEnumerable<PrinterListItem> GetPrinters()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var quary = ctx.Printers.Where(e => e.UserId == _userId).Select(e => new PrinterListItem
                {
                    PrinterId = e.PrinterId,
                    PrinterBrand = e.PrinterBrand,
                    PrinterModel = e.PrinterModel,
                    Image = e.Image,
                });

                return quary.ToArray();
            }
        }

        public bool UpdatePrinter(HttpPostedFileBase file, PrinterEdit model)
        {
            model.Image = ConvertToBytes(file);
            using (var ctx = new ApplicationDbContext())
            {

                var entity = ctx.Printers.Single(e => e.UserId == _userId && e.PrinterId == model.PrinterId);

                entity.PrinterBrand = model.PrinterBand;
                entity.PrinterModel = model.PrinterModel;
                entity.HasHeatedBed = model.HasHeatedBed;
                entity.HasDualExtruder = model.HasDualExtruder;
                entity.CanAutoLevel = model.CanAutoLevel;
                entity.CanUpgrade = model.CanUpgrade;
                entity.HasWifi = model.HasWifi;
                entity.HasCamera = model.HasCamera;
                entity.Image = model.Image;



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
