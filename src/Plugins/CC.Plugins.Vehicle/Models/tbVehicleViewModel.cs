using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CC.Plugins.Vehicle.Models
{
   
    public class tbVehicleViewModel
    {
        public int VehicleId { get; set; }

        public string Vehicle { get; set; }

        public bool IsJourney { get; set; }
    }
}