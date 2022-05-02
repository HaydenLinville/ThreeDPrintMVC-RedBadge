using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PrinterModels
{
    public class PrinterCreate
    {
        [Display(Name ="Brand")]
        public string PrinterBrand { get; set; }
        [Display(Name = "Model")]
        public string PrinterModel { get; set; }
        [Display(Name = "Dual Extruder")]
        public bool HasDualExtruder { get; set; }
        [Display(Name = "Auto Level")]
        public bool CanAutoLevel { get; set; }
        [Display(Name = "Heated Bed")]
        public bool HasHeatedBed { get; set; }

    }
}
