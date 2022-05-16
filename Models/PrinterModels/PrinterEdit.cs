using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PrinterModels
{
    public class PrinterEdit
    {
        public int PrinterId { get; set; }
        [Display(Name= "Brand")]
        public string PrinterBand { get; set; }
        [Display(Name= "Model")]
        public string PrinterModel { get; set; }
        [Display(Name= "Dual Extruder")]
        public bool HasDualExtruder { get; set; }
        [Display(Name= "Auto Level")]
        public bool CanAutoLevel { get; set; }
        [Display(Name= "Heated Bed")]
        public bool HasHeatedBed { get; set; }
        
        [Display(Name = "Wifi")]
        public bool HasWifi { get; set; }
        [Display(Name = "Upgrade")]
        public bool CanUpgrade { get; set; }
        [Display(Name = "Camera")]
        public bool HasCamera { get; set; }
        public byte[] Image { get; set; }
    }
}
