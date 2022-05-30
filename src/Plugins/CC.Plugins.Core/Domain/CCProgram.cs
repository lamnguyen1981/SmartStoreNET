using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Domain
{
    public class CCProgram: CCBaseEntity
    {
        public string tbProgramCode { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }        

        public string ProgramType { get; set; }        

        public int? Sequence { get; set; }

        public int? LockOutDays { get; set; }        

        public int? Frequency { get; set; }        

        public int? AppliedTo { get; set; }        

        public bool? Deleted { get; set; }
        
        //public CCCustomerProfile CCCustomerProfile { get; set; }
    }
}