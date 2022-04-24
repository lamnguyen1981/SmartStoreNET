using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Zoho.Models
{
    public class InvoiceModel
    {
        public string InvoiveId { get; set; }

        public string InvoiveNumber { get; set; }

        public DateTime InvoiveDate { get; set; }

        public string InvoiveDateStringFormat
        {
            get { return InvoiveDate.ToString("MM/dd/yyyy"); }
            
        }

        public string CustomerName { get; set; }

        public string Status { get; set; }

        public Decimal Total { get; set; }

        public string CurrencySymbol { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
