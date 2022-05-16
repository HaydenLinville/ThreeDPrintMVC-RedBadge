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
        [Range(90,300, ErrorMessage ="Must be set betweent {1} and {2}.")]
        [Display(Name= "Extruder °C")]
        public double MaterialTemp { get; set; }
        [Range(40, 200, ErrorMessage ="Must be set between {1} and {2}.")]
        [Display(Name= "Bed °C")]
        public double BedTemp { get; set; }

        [Range(10, 150, ErrorMessage ="Must be set between {1} and {2},")]
        [Display(Name ="Speed mm/s")]
        public double Speed { get; set; }

    }
}
