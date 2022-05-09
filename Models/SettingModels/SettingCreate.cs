using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SettingModels
{
    public class SettingCreate
    {
        [Display(Name ="Your Setting Name")]
        public string CustomSettingName { get; set; }
        [Display(Name= "Material")]
        public int MaterialId { get; set; }

        [Display(Name= "Printer")]
        public int PrinterId { get; set; }

        [Display(Name= "Extruder °C")]
        public double MaterialTemp { get; set; }
        [Display(Name= "Bed °C")]
        public double BedTemp { get; set; }
        [Display(Name ="Speed mm/s")]
        public double Speed { get; set; }

    }
}
