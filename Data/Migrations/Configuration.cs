namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.ApplicationDbContext context)
        {

            context.Printers.AddOrUpdate(x => x.PrinterId,
                new Printer()
                {
                    PrinterId = 1,
                    PrinterBrand = "MakerBot",
                    PrinterModel = "Replicator Plus",
                    CanAutoLevel = true,
                    CanUpgrade = true,
                    HasCamera = false,
                    HasDualExtruder = false,
                    HasHeatedBed = true,
                    HasWifi = true,
                    Image = ReadFile("../../Img/MakerBot.png"),
                },
                new Printer()
                {
                    PrinterId = 2,
                    PrinterBrand = "Flashforge",
                    PrinterModel = "Creator Pro 2",
                    CanAutoLevel = false,
                    HasCamera = true,
                    HasDualExtruder = true,
                    HasHeatedBed = true,
                    HasWifi = true,
                    Image = ReadFile("../../Img/flashforge.jpg")
                },
                new Printer()
                {
                    PrinterId = 3,
                    PrinterBrand = "AirWolf",
                    PrinterModel = "EVO R 3D",
                    CanAutoLevel = true,
                    HasCamera = false,
                    HasDualExtruder = true,
                    HasHeatedBed = true,
                    HasWifi = false,
                    Image = ReadFile("../../Img/Evo.jpg")

                });

            context.Materials.AddOrUpdate(x => x.MaterialId,
                new Material()
                {
                    MaterialId = 1,
                    Color = "Yellow",
                    MaterialBrand = "Hatchbox",
                    MaterialType = MaterialTypes.PLA,
                    Image = ReadFile("../../Img/Yellow.png")
                },
                new Material()
                {
                    MaterialId = 2,
                    Color = "Blue",
                    MaterialBrand = "Esun",
                    MaterialType = MaterialTypes.PETG,
                    Image = ReadFile("../../Img/blue.jpg")
                },
                new Material()
                {
                    MaterialId = 3,
                    Color = "White",
                    MaterialBrand = "Bridge",
                    MaterialType = MaterialTypes.Nylon,
                    Image = ReadFile("../../Img/white.jpg")
                }

                );

            context.Settings.AddOrUpdate(x => x.SettingId,
                new Setting()
                {
                    SettingId = 1,
                    PrinterId = 1,
                    MaterialId = 1,
                    CustomSettingName = "Profile 1",
                    Speed = 50,
                    BedTemp = 60,
                    MaterialTemp = 190,
                },
                new Setting()
                {
                    SettingId = 2,
                    PrinterId = 1,
                    MaterialId = 2,
                    CustomSettingName = "Profile 2",
                    Speed = 50,
                    BedTemp = 80,
                    MaterialTemp = 230,
                },
                new Setting()
                {
                    SettingId = 3,
                    PrinterId = 1,
                    MaterialId = 3,
                    CustomSettingName = "Profile 3",
                    Speed = 40,
                    BedTemp = 110,
                    MaterialTemp = 250,
                },
                new Setting()
                {
                    SettingId = 4,
                    PrinterId = 1,
                    MaterialId = 1,
                    CustomSettingName = "Profile 4",
                    Speed = 60,
                    BedTemp = 70,
                    MaterialTemp = 200,
                },
                new Setting()
                {
                    SettingId = 5,
                    PrinterId = 2,
                    MaterialId = 1,
                    CustomSettingName = "Profile 1",
                    Speed = 53,
                    BedTemp = 72,
                    MaterialTemp = 201,
                },
                new Setting()
                {
                    SettingId = 6,
                    PrinterId = 2,
                    MaterialId = 1,
                    CustomSettingName = "Profile 2",
                    Speed = 45,
                    BedTemp = 82,
                    MaterialTemp = 193,
                },
                new Setting()
                {
                    SettingId = 7,
                    PrinterId = 2,
                    MaterialId = 1,
                    CustomSettingName = "Profile 3",
                    Speed = 55,
                    BedTemp = 0,
                    MaterialTemp = 165,
                }
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
        public byte[] ReadFile(string sPath)
        {
            byte[] data = null;

            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);

            return data;


        }
    }
}
