using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SettingModels
{
    public class SettingDetail
    {
        [Display(Name = "Name")]
        public string CustomSettingName { get; set; }
        public int SettingId { get; set; }
        [Display(Name = "Material")]
        public int MaterialId { get; set; }
        [Display(Name = "Printer")]
        public int PrinterId { get; set; }
        [Display(Name ="Printer Model")]
        public string PrinterModel { get; set; }
        [Display(Name = "Material")]
        public MaterialTypes MaterialTypes { get; set; }
        public string Color { get; set; }
        [Display(Name = "Extruder °C")]
        public double MaterialTemp { get; set; }
        [Display(Name = "Bed °C")]
        
        public double BedTemp { get; set; }
        [Display(Name = "Speed mm/s")]

        public double Speed { get; set; }

        public string Printer { get; set; }
        public string Material { get; set; }
    }
}
