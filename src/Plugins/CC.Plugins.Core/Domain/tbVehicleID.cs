using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Domain
{
    public class tbVehicleID: BaseEntity
    {
        public string Vehicle { get; set; }

        public bool IsJourney { get; set; }
    }
}