using Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PrinterModels
{
    public class PrinterDetail
    {
        public int PrinterId { get; set; }
        public string PrinterBrand { get; set; }
        public string PrinterModel { get; set; }
        public bool HasDualExtruder { get; set; }
        public bool CanAutoLevel { get; set; }
        public bool HasHeatedBed { get; set; }
        public List<SettingPrinterDisplay> Settings { get; set; }
        //might not make that a list 
    }
}
