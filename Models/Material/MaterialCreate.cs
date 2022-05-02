using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Material
{
    public class MaterialCreate
    {

        [Display(Name = "Brand")]
        public string MaterialBrand { get; set; }
        [Display(Name = "Material")]
        public MaterialTypes MaterialType { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}
