using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Domain
{
    public class CCVehicle: CCBaseEntity
    {
        public int tbVehicleId { get; set; }
        
        public int ProgramId { get; set; }

        public string Name { get; set; }
        
        public int StartYW { get; set; }
        
        public int EndYW { get; set; }
        
        public int NumberOfLevels { get; set; }
        
        public decimal SellUnitPrice { get; set; }
        
        public bool? Deleted { get; set; }

        public CCProgram CCProgram { get; set; }
    }
}