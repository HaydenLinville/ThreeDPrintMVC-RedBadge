using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string CustomSettingName { get; set; }

        [ForeignKey("Material")]
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        [ForeignKey("Printer")]
        public int PrinterId { get; set; }
        public virtual Printer Printer { get; set; }

        [Required]
        public double MaterialTemp { get; set; }
        [Required]
        public double BedTemp { get; set; }
        [Required]
        public double Speed { get; set; }
    }
}
