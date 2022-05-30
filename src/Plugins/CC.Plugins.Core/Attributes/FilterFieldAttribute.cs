using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CC.Plugins.Core.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field |
                       System.AttributeTargets.Property,
                       AllowMultiple = true) 
    ]
    public class FilterFieldAttribute : System.Attribute
    {
        public string FilterOperator { get; set; }
    }

    
}