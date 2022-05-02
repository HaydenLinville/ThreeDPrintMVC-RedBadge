using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SettingModels
{
    public class SettingPrinterDisplay
    {
        //maybe add materialbrand
        [Display(Name = "Custom Setting Name")]
        public string CustomSettingName { get; set; }
        public int SettingId { get; set; }
        [Display(Name = "Material")]
        public MaterialTypes MaterialType { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
        [Display(Name = "Extruder °C")]
        public double MaterialTemp { get; set; }
        [Display(Name = "Bed °C")]
        public double BedTemp { get; set; }
        [Display(Name = "Speed mm/s")]
        public double Speed { get; set; }


    }
}
