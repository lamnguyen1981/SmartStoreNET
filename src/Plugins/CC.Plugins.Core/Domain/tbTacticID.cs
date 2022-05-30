using SmartStore.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Domain
{
    [Table("tbTacticID")]
    public class tbTacticID : BaseEntity
    {
        public string Tactic { get; set; }

        public string ProgramCode { get; set; }

        public bool IsJourney { get; set; }
    }
}