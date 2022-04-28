using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum MaterialTypes { PLA, ABS, PETG, PVA, Nylon}
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string MaterialBrand { get; set; }
        [Required]
        public MaterialTypes MaterialType { get; set; }
        [Required]
        public string Color { get; set; }
    
    }
}
