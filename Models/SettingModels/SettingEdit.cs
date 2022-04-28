using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SettingModels
{
    public class SettingEdit
    {
        public int SettingId { get; set; }
        public int MaterialId { get; set; }
        public int PrinterId { get; set; }

        public double MaterialTemp { get; set; }
        public double BedTemp { get; set; }
        public double Speed { get; set; }
    }
}
