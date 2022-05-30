using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CC.Plugins.Tactic.Models
{
   
    public class tbTacticViewModel
    {
        public string TacticCode { get; set; }

        public string TacticName { get; set; }
    }
}