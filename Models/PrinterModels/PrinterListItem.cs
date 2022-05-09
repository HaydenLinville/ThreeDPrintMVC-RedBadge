using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PrinterModels
{
    public class PrinterListItem
    {
        public int PrinterId { get; set; }
        [Display(Name = "Brand")]
        public string PrinterBrand { get; set; }
        [Display(Name = "Model")]
        public string PrinterModel { get; set; }
        public byte[] Image { get; set; }

    }
}
