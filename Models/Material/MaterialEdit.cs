using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Material
{
    public class MaterialEdit
    {
        public int MaterialId { get; set; }
        public string Brand { get; set; }
        public MaterialTypes MaterialType { get; set; }
        public string Color { get; set; }

    }
}
