using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Printer
    {
        [Key]
        public int PrinterId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string PrinterBrand { get; set; }
        [Required]
        public string PrinterModel { get; set; }
        [Required]
        public bool HasDualExtruder { get; set; }
        [Required]
        public bool CanAutoLevel { get; set; }
        [Required]
        public bool HasHeatedBed { get; set; }

        public virtual List<Setting> Settings { get; set; }
    }
}
