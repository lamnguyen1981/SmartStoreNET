using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Zoho.Models
{
    public class ZohoModels
    {
        #region Zoho
        public class Zoho_PageContext
        {
            public int page { get; set; }
            public int per_page { get; set; }
            public bool? has_more_page { get; set; }
            public string report_name { get; set; }
            public string applied_filter { get; set; }
            public List<object> custom_fields { get; set; }
            public string sort_column { get; set; }
            public string sort_order { get; set; }
        }

        public enum ZohoApiType
        {
            invoice,
            invoices,
            contact,
            contacts,
            item,
            items,
            customfields,
            settings,
            customerpayments,
            payment
        }

        public enum ZohoAction
        {
            Success = 0,
            SuccessButErroredParsing = 1,
            SkipErrorsAndContinue = 2,
            SkipErrorsAndGoToNextLoop = 3,
            StopAndThrowErrors = 4
        }

        public class ZohoRootObject<T>
        {
            public int code { get; set; }
            public string message { get; set; }
            public T Zoho_Object { get; set; }
            public T[] Zoho_Objects { get; set; }
            public bool Success { get; set; }

            public Zoho_PageContext PageContext { get; set; }
        }

        public class ZohoRefreshToken
        {
            public string access_token { get; set; }
            public string api_domain { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
        }

        #endregion
    }
}