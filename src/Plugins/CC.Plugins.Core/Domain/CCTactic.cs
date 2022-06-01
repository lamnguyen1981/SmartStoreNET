using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Domain
{
    public class CCTactic: CCBaseEntity
    {        
        public int tbTacticId { get; set; }
        
        public int VehicleId { get; set; }            

        public int StartYW { get; set; }

        public int EndYW { get; set; }

        public string TacticCode { get; set; }

        public string TacticType { get; set; }

        public string TacticDescription { get; set; }          

        public decimal TacticAmount { get; set; }    
                   
        public bool? Deleted { get; set; }
        
        public CCVehicle CCVehicle { get; set; }
    }
}