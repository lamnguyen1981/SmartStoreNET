using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Domain
{
    public class CCBaseEntity : BaseEntity
    {
        public int CreatedByUser{ get; set; }

        public DateTime CreatedOnUtc{ get; set; }

        public int UpdatedByUser { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }
    }
}



