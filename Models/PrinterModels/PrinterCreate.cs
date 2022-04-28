﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PrinterModels
{
    public class PrinterCreate
    {
        public string PrinterBrand { get; set; }
        public string PrinterModel { get; set; }
        public bool HasDualExtruder { get; set; }
        public bool CanAutoLevel { get; set; }
        public bool HasHeatedBed { get; set; }

    }
}
