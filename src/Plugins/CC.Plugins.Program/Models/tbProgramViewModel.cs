using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CC.Plugins.Program.Models
{
   
    public class tbProgramViewModel
    {
        public string ProgramCode { get; set; }

        public string ProgramName { get; set; }
    }
}