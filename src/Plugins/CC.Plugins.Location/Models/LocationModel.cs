using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Location.Models
{
    public class LocationModel
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public string LocationType { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}