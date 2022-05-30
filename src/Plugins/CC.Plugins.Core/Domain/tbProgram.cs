using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Domain
{
    [Table("tbProgram")]
    public class tbProgram: BaseEntity
    {
        public string ProgramCode { get; set; }

        public string ProgramName { get; set; }
    }
}