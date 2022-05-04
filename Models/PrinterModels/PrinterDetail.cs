using Models.SettingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PrinterModels
{
    public class PrinterDetail
    {
        public int PrinterId { get; set; }
        [Display(Name = "Brand")]
        public string PrinterBrand { get; set; }
        [Display(Name = "Model")]
        public string PrinterModel { get; set; }
        [Display(Name = "Has Dual Extruder")]
        public bool HasDualExtruder { get; set; }
        [Display(Name = "Can Level Bed")]
        public bool CanAutoLevel { get; set; }
        [Display(Name = "Has Heated Bed")]
        public bool HasHeatedBed { get; set; }
        public byte[] Image { get; set; }
        public List<SettingPrinterDisplay> Settings { get; set; }
        //might not make that a list 
    }
}
