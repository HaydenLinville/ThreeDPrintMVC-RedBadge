using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SettingModels
{
    public class SettingPrinterDisplay
    {
        //maybe add materialbrand
        public MaterialTypes MaterialType { get; set; }
        public string Color { get; set; }

        public double MaterialTemp { get; set; }
        public double BedTemp { get; set; }
        public double Speed { get; set; }


    }
}
