using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CC.Plugins.Zoho.Models.ZohoModels;

namespace CC.Plugins.Zoho.Services
{
    public interface IZohoService
    {
          System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetInvoicesByStatus(string status);
        System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetInvoiceDetail_v2(string invoice_Id);
    }
}