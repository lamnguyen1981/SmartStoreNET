using SmartStore.Core;
using SmartStore.Web.Framework.Modelling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CC.Plugins.Tactic.Models
{
   
    public class tbTacticViewModel: EntityModelBase
    {
        public int TacticID { get; set; }

        public string Tactic { get; set; }

        public string ProgramCode { get; set; }

        public bool IsJourney { get; set; }
    }
}