using Newtonsoft.Json.Linq;
using SmartStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using static CC.Plugins.Zoho.Models.ZohoModels;

namespace CC.Plugins.Zoho.Services
{
    public class ZohoService: IZohoService
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string refresh_token { get; set; }
        public string oauthtoken { get; set; }

        public string organization_id { get; set; }
        public ZohoService(ICommonServices _services)
        {
            var setting = _services.Settings.LoadSetting<ZohoSettings>(_services.StoreContext.CurrentStore.Id);
            this.client_id = setting.ClientId;
            this.client_secret = setting.ClientSecret;
            this.oauthtoken = setting.OauthToken;
            this.refresh_token = setting.RefreshToken;
            this.organization_id = setting.OrganizationId;
        }


        #region PUBLIC

        public ZohoAction GetZohoAction(int rootCode, bool rootIsSuccess, ZohoAction actionWhenNotFound)
        {
            ZohoAction action = ZohoAction.StopAndThrowErrors;

            switch (rootCode)
            {
                case (int)HttpStatusCode.OK:
                case (int)HttpStatusCode.Created:
                    action = ZohoAction.Success; break;
                case (int)HttpStatusCode.NotFound:
                    action = actionWhenNotFound;
                    break;
                case (int)HttpStatusCode.BadRequest:
                case (int)HttpStatusCode.Unauthorized:
                case 429:
                    action = ZohoAction.StopAndThrowErrors;
                    break;
                default:
                    action = ZohoAction.StopAndThrowErrors;
                    break;
            }

            if (action == ZohoAction.Success && rootIsSuccess == false)
                action = ZohoAction.SuccessButErroredParsing;

            return action;
        }

        public async Task<string> RefreshToken()
        {
            var token = await RefreshToken<ZohoRootObject<ZohoRefreshToken>>();

            return token.Zoho_Object.access_token;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<ZohoRefreshToken>> RefreshToken<T>()
        {
            ZohoRootObject<ZohoRefreshToken> result = new ZohoRootObject<ZohoRefreshToken>();

            string url = $"https://accounts.zoho.com/oauth/v2/token?client_id={client_id}&client_secret={client_secret}&grant_type=refresh_token&redirect_uri=https://books.zoho.com&refresh_token={refresh_token}";

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();

                    HttpResponseMessage response = await client.PostAsJsonAsync(url, "");

                    var content = await response.Content.ReadAsStringAsync();

                    result.code = (int)response.StatusCode;
                    result.Zoho_Object = Newtonsoft.Json.JsonConvert.DeserializeObject<ZohoRefreshToken>(content);
                    result.Success = true;

                    this.oauthtoken = result.Zoho_Object.access_token;
                }
            }
            catch (Exception er)
            {
                result.message = er.Message;
            }


            return result;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> CreatePayment(string zoho_InvoiceID, string zoho_InvoiceNumber, string zoho_CustomerID, double amount, DateTime date, string paymentDesctiption)
        {
            ZohoRootObject<JObject> result = new ZohoRootObject<JObject>();

            var payment = new
            {
                customer_id = zoho_CustomerID,
                payment_mode = "creditcard",
                amount = amount,
                reference_number = zoho_InvoiceNumber,
                description = paymentDesctiption,
                date = date.ToString("yyyy-MM-dd"),
                invoices = new[]
                {
                    new { invoice_id = zoho_InvoiceID, amount_applied = amount }
                }

            };

            var rootObj = await CreateZohoBaseObject(ZohoApiType.customerpayments.ToString(), payment);

            result.code = rootObj.code;
            result.message = rootObj.Zoho_Object.GetValue("message").ToString();
            result.Success = false;
            if (result.code == (int)HttpStatusCode.Created)
            {
                try
                {
                    string content = rootObj.Zoho_Object.GetValue(nameof(ZohoApiType.payment)).ToString();

                    result.Zoho_Object = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(content);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    //Exceptions.LogException(ex);
                    result.message = ex.Message;
                }
            }

            return result;
        }

        #region CONTACT

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetInvoiceCustomFields(Dictionary<string, string> parameters)
        {
            var customfields = await GetZohoObjects<JObject>($"{ZohoApiType.settings}/{ZohoApiType.customfields}/{ZohoApiType.invoice}", ZohoApiType.customfields, parameters);

            return customfields;
        }

        #endregion

        #region INVOICE

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetInvoiceDetail_v2(string invoice_Id)
        {
            ZohoRootObject<JObject> result = new ZohoRootObject<JObject>();

            var rootObj = await GetZohoBaseObjectDetail_v2(invoice_Id, ZohoApiType.invoices.ToString(), new Dictionary<string, string>() { });

            result.code = (int)rootObj.Zoho_Object.GetValue("code");
            result.message = rootObj.Zoho_Object.GetValue("message").ToString();
            result.Success = false;
            if (result.code == 0)
            {
                try
                {
                    string content = rootObj.Zoho_Object.GetValue(nameof(ZohoApiType.invoice)).ToString();

                    result.Zoho_Object = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(content);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    //Exceptions.LogException(ex);
                    result.message = ex.Message;
                }
            }

            return result;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetInvoicesByYearWeek(string yearweek)
        {
            var invoices = await GetZohoObjects<JObject>(ZohoApiType.invoices.ToString(), ZohoApiType.invoices, new Dictionary<string, string>()
            {
                { "invoice_number_startswith", yearweek}
            });

            return invoices;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetInvoicesByStatus(string status)
        {
            var invoices = await GetZohoObjects<JObject>(ZohoApiType.invoices.ToString(), ZohoApiType.invoices, new Dictionary<string, string>()
            {
                { "status", status}
            });

            return invoices;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> DeleteInvoice(string zoho_InvoiceId)
        {
            var root = await DeleteZohoObjects<JObject>(zoho_InvoiceId, ZohoApiType.invoices, ZohoApiType.invoice);

            return root;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> MarkInvoiceStatus(string zoho_InvoiceId, string status)
        {
            var invoice = await SetZohoObject<JObject>(zoho_InvoiceId, ZohoApiType.invoices, ZohoApiType.invoice, new Dictionary<string, string>()
            {
                { "status", $"{status}"}
            });

            return invoice;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> CreateInvoice_v2(Object inputInvoice)
        {
            var invoice = await CreateZohoObjects<JObject>(inputInvoice, ZohoApiType.invoices, ZohoApiType.invoice);

            return invoice;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> CreateContact_v2(Object inputContact)
        {
            var invoice = await CreateZohoObjects<JObject>(inputContact, ZohoApiType.contacts, ZohoApiType.contact);

            return invoice;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetContacts_v2(Dictionary<string, string> parameters)
        {
            var contact = await GetZohoObjects<JObject>(ZohoApiType.contacts.ToString(), ZohoApiType.contacts, parameters);

            return contact;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetContactCustomFields(Dictionary<string, string> parameters)
        {
            var customfields = await GetZohoObjects<JObject>($"{ZohoApiType.settings}/{ZohoApiType.customfields}/{ZohoApiType.contact}", ZohoApiType.customfields, parameters);

            return customfields;
        }



        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetContactByID(string contactId)
        {
            var contact = await GetZohoObjectDetail_v2<JObject>(contactId, ZohoApiType.contacts, ZohoApiType.contact, null);

            return contact;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetItemByName(string itemName)
        {
            var items = await GetZohoObjects<JObject>(ZohoApiType.items.ToString(), ZohoApiType.items, new Dictionary<string, string>()
            {
                { "name", itemName}
            });

            return items;
        }

        #endregion


        #region CRUD

        public async System.Threading.Tasks.Task<ZohoRootObject<T>> SetZohoObject<T>(string zoho_ObjectId, ZohoApiType type, ZohoApiType jsonProperty, Dictionary<string, string> parameters)
        {
            ZohoRootObject<T> result = new ZohoRootObject<T>();

            var rootObj = await SetZohoBaseObjectDetail(zoho_ObjectId, type.ToString(), parameters);

            result.code = rootObj.code;//(int)rootObj.Zoho_Object.GetValue("code");
            result.message = rootObj.Zoho_Object.GetValue("message").ToString();
            result.Success = false;
            if (result.code == (int)HttpStatusCode.OK)
            {
                try
                {
                    //string content = rootObj.Zoho_Object.GetValue(jsonProperty.ToString()).ToString();

                    //result.Zoho_Object = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    //Exceptions.LogException(ex);
                    result.message = ex.Message;
                }
            }

            return result;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<T>> UpdateZohoObjects<T>(string zoho_ObjectId, Object objectToUpdate, ZohoApiType type, ZohoApiType jsonProperty)
        {
            ZohoRootObject<T> result = new ZohoRootObject<T>();

            var rootObj = await UpdateZohoBaseObjectDetail(zoho_ObjectId, type.ToString(), objectToUpdate);

            result.code = rootObj.code;//(int)rootObj.Zoho_Object.GetValue("code");
            result.message = rootObj.Zoho_Object.GetValue("message").ToString();
            result.Success = false;
            if (result.code == (int)HttpStatusCode.OK)
            {
                try
                {
                    string content = rootObj.Zoho_Object.GetValue(jsonProperty.ToString()).ToString();

                    result.Zoho_Object = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    //Exceptions.LogException(ex);
                    result.message = ex.Message;
                }
            }

            return result;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<T>> CreateZohoObjects<T>(Object objectToCreate, ZohoApiType type, ZohoApiType jsonProperty)
        {
            ZohoRootObject<T> result = new ZohoRootObject<T>();

            var rootObj = await CreateZohoBaseObject(type.ToString(), objectToCreate);

            result.code = rootObj.code;//(int)rootObj.Zoho_Object.GetValue("code");
            result.message = rootObj.Zoho_Object.GetValue("message").ToString();
            result.Success = false;
            if (result.code == (int)HttpStatusCode.Created)
            {
                try
                {
                    string content = rootObj.Zoho_Object.GetValue(jsonProperty.ToString()).ToString();

                    result.Zoho_Object = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    //Exceptions.LogException(ex);
                    result.message = ex.Message;
                }
            }

            return result;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<T>> DeleteZohoObjects<T>(string zoho_ObjectId, ZohoApiType type, ZohoApiType jsonProperty)
        {
            ZohoRootObject<T> result = new ZohoRootObject<T>();

            var rootObj = await DeleteZohoBaseObject(zoho_ObjectId, type.ToString(), null);

            result.code = rootObj.code;//(int)rootObj.Zoho_Object.GetValue("code");
            result.message = rootObj.Zoho_Object.GetValue("message").ToString();
            result.Success = false;
            if (result.code == (int)HttpStatusCode.OK)
            {
                result.Success = true;
            }

            return result;
        }

        #endregion



        #endregion



        #region PRIVATE

        #region GET

        public async System.Threading.Tasks.Task<ZohoRootObject<T>> GetZohoObjects<T>(string CustomType, ZohoApiType jsonProperty, Dictionary<string, string> parameters, string customErrorMessage = null)
        {
            bool hasMorePage = false;

            ZohoRootObject<T> result = new ZohoRootObject<T>();
            result.Success = false;

            if (parameters == null || parameters.ContainsKey(nameof(Zoho_PageContext.page)) == false)
            {
                if (parameters == null)
                    parameters = new Dictionary<string, string>();

                parameters.Add(nameof(Zoho_PageContext.page), "1");

                hasMorePage = true;
            }

            try
            {
                do
                {
                    var rootObj = await GetZohoBaseObjects_v2(CustomType, parameters);

                    result.code = rootObj.code;//(int)rootObj.Zoho_Object.GetValue("code");
                    result.message = rootObj.Zoho_Object.GetValue("message").ToString();
                    result.Success = false;

                    var pageData = rootObj.Zoho_Object.GetValue("page_context");
                    if (pageData != null)
                    {
                        result.PageContext = pageData.ToObject<Zoho_PageContext>();
                    }
                    else
                    {
                        hasMorePage = false;
                    }

                    if (result.code == (int)HttpStatusCode.OK)
                    {
                        string content = rootObj.Zoho_Object.GetValue(jsonProperty.ToString()).ToString();

                        var tmp = Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(content);

                        if (result.Zoho_Objects == null)
                            result.Zoho_Objects = tmp;
                        else
                            result.Zoho_Objects = result.Zoho_Objects.Concat(tmp).ToArray<T>();
                    }

                    if (hasMorePage)
                    {
                        hasMorePage = (result.PageContext.has_more_page.HasValue && result.PageContext.has_more_page.Value == true) ? true : false;
                        parameters[nameof(Zoho_PageContext.page)] = (Convert.ToInt32(parameters[nameof(Zoho_PageContext.page)]) + 1).ToString();
                    }

                }
                while (hasMorePage);

                result.Success = true;
            }
            catch (Exception err)
            {
                result.Success = false;
                result.message = err.Message;
            }


            return result;
        }

        public async System.Threading.Tasks.Task<ZohoRootObject<T>> GetZohoObjectDetail_v2<T>(string zoho_ObjectId, ZohoApiType type, ZohoApiType jsonProperty, Dictionary<string, string> parameters, string customErrorMessage = null)
        {
            ZohoRootObject<T> result = new ZohoRootObject<T>();

            var rootObj = await GetZohoBaseObjectDetail_v2(zoho_ObjectId, type.ToString(), parameters);

            result.code = rootObj.code;//(int)rootObj.Zoho_Object.GetValue("code");
            result.message = rootObj.Zoho_Object.GetValue("message").ToString();
            result.Success = false;
            if (result.code == (int)HttpStatusCode.OK)
            {
                try
                {
                    string content = rootObj.Zoho_Object.GetValue(jsonProperty.ToString()).ToString();

                    result.Zoho_Object = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    //Exceptions.LogException(ex);
                    result.message = ex.Message;
                }
            }

            return result;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> Get_RESTful_ZohoBaseObject_v2(string url, Dictionary<string, string> parameters, int retry = 1)
        {
            if (retry < 0)
                return null;

            ZohoRootObject<JObject> zohoObj = new ZohoRootObject<JObject>();

            string parameterUrl = parameters == null || parameters.Count == 0 ? string.Empty : "&" + string.Join("&", parameters.Select(n => $"{n.Key}={n.Value}").ToArray());

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", oauthtoken);

                url = $"{url}{parameterUrl}";

                HttpResponseMessage response = await client.GetAsync(url);

                var content = await response.Content.ReadAsStringAsync();

                zohoObj.code = (int)response.StatusCode;
                zohoObj.Zoho_Object = JObject.Parse(content);

                if (zohoObj.code == 401 && (zohoObj.Zoho_Object["code"]?.ToString() == "57" || zohoObj.Zoho_Object["code"]?.ToString() == "14"))
                {
                    var tokenResult = await RefreshToken<ZohoRefreshToken>();
                    if (tokenResult.code == 200)
                    {
                        retry -= 1;
                        zohoObj = await Get_RESTful_ZohoBaseObject_v2(url, null, retry);
                    }


                }

            }

            return zohoObj;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetZohoBaseObjects_v2(string zoho_REsT_type, Dictionary<string, string> parameters)
        {
            string url = $"https://books.zoho.com/api/v3/{zoho_REsT_type}?organization_id={organization_id}";
            var zohoObj = await Get_RESTful_ZohoBaseObject_v2(url, parameters);

            return zohoObj;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> GetZohoBaseObjectDetail_v2(string zoho_ObjectId, string zoho_REsT_type, Dictionary<string, string> parameters)
        {
            string url = $"https://books.zoho.com/api/v3/{zoho_REsT_type}/{zoho_ObjectId}?organization_id={organization_id}";
            var zohoObj = await Get_RESTful_ZohoBaseObject_v2(url, parameters);

            return zohoObj;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> SetZohoBaseObjectDetail(string zoho_ObjectId, string zoho_REsT_type, Dictionary<string, string> parameters)
        {
            string fieldsToBeUpdated = string.Join("/", parameters.Select(n => $"{n.Key}/{n.Value}").ToArray());

            string url = $"https://books.zoho.com/api/v3/{zoho_REsT_type}/{zoho_ObjectId}/{fieldsToBeUpdated}?organization_id={organization_id}";

            var zohoObj = await Post_RESTful_ZohoBaseObject_v2(url, parameters);

            return zohoObj;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> UpdateZohoBaseObjectDetail(string zoho_ObjectId, string zoho_REsT_type, Object objectToBeUpdated)
        {
            string url = $"https://books.zoho.com/api/v3/{zoho_REsT_type}/{zoho_ObjectId}?organization_id={organization_id}";

            var zohoObj = await Put_RESTful_ZohoBaseObject(url, objectToBeUpdated);

            return zohoObj;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> CreateZohoBaseObject(string zoho_REsT_type, Object zohoObjectToBeCreated)
        {
            string url = $"https://books.zoho.com/api/v3/{zoho_REsT_type}?organization_id={organization_id}";
            var zohoObj = await Post_RESTful_ZohoBaseObject_v2(url, zohoObjectToBeCreated);

            return zohoObj;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> DeleteZohoBaseObject(string zoho_ObjectId, string zoho_REsT_type, Dictionary<string, string> parameters)
        {
            string url = $"https://books.zoho.com/api/v3/{zoho_REsT_type}/{zoho_ObjectId}?organization_id={organization_id}";
            var zohoObj = await Delete_RESTful_ZohoBaseObject(url, parameters);

            return zohoObj;
        }

        #endregion

        #region POST

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> Post_RESTful_ZohoBaseObject_v2(string url, Object zohoObjectToBeCreated, int retry = 1)
        {
            if (retry < 0)
                return null;

            ZohoRootObject<JObject> zohoObj = new ZohoRootObject<JObject>();

            string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(zohoObjectToBeCreated);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", oauthtoken);

                url = $"{url}&JSONString={jsonContent}";

                HttpResponseMessage response = await client.PostAsJsonAsync(url, "");

                var content = await response.Content.ReadAsStringAsync();

                zohoObj.code = (int)response.StatusCode;
                zohoObj.Zoho_Object = JObject.Parse(content);
                if (zohoObj.code == 401 && (zohoObj.Zoho_Object["code"]?.ToString() == "57" || zohoObj.Zoho_Object["code"]?.ToString() == "14"))
                {
                    var tokenResult = await RefreshToken<ZohoRefreshToken>();
                    if (tokenResult.code == 200)
                    {
                        retry -= 1;
                        zohoObj = await Post_RESTful_ZohoBaseObject_v2(url, zohoObjectToBeCreated, retry);
                    }
                }
            }

            return zohoObj;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> Post_RESTful_ZohoBaseObject_v2(string url, int retry = 1)
        {
            if (retry < 0)
                return null;

            ZohoRootObject<JObject> zohoObj = new ZohoRootObject<JObject>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", oauthtoken);

                url = $"{url}";

                HttpResponseMessage response = await client.PostAsJsonAsync(url, "");

                var content = await response.Content.ReadAsStringAsync();

                zohoObj.code = (int)response.StatusCode;
                zohoObj.Zoho_Object = JObject.Parse(content);
                if (zohoObj.code == 401 && (zohoObj.Zoho_Object["code"]?.ToString() == "57" || zohoObj.Zoho_Object["code"]?.ToString() == "14"))
                {
                    var tokenResult = await RefreshToken<ZohoRefreshToken>();
                    if (tokenResult.code == 200)
                    {
                        retry -= 1;
                        zohoObj = await Post_RESTful_ZohoBaseObject_v2(url, retry);
                    }
                }
            }

            return zohoObj;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> Put_RESTful_ZohoBaseObject(string url, Object propertiesToBeUpdated, int retry = 1)
        {
            if (retry < 0)
                return null;

            ZohoRootObject<JObject> zohoObj = new ZohoRootObject<JObject>();

            string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(propertiesToBeUpdated);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", oauthtoken);

                url = $"{url}&JSONString={jsonContent}";

                HttpResponseMessage response = await client.PutAsJsonAsync(url, "");

                var content = await response.Content.ReadAsStringAsync();

                zohoObj.code = (int)response.StatusCode;
                zohoObj.Zoho_Object = JObject.Parse(content);
                if (zohoObj.code == 401 && (zohoObj.Zoho_Object["code"]?.ToString() == "57" || zohoObj.Zoho_Object["code"]?.ToString() == "14"))
                {
                    var tokenResult = await RefreshToken<ZohoRefreshToken>();
                    if (tokenResult.code == 200)
                    {
                        retry -= 1;
                        zohoObj = await Put_RESTful_ZohoBaseObject(url, propertiesToBeUpdated, retry);
                    }
                }
            }

            return zohoObj;
        }

        private async System.Threading.Tasks.Task<ZohoRootObject<JObject>> Delete_RESTful_ZohoBaseObject(string url, Dictionary<string, string> parameters, int retry = 1)
        {
            if (retry < 0)
                return null;

            ZohoRootObject<JObject> zohoObj = new ZohoRootObject<JObject>();

            string parameterUrl = parameters == null || parameters.Count == 0 ? string.Empty : "&" + string.Join("&", parameters.Select(n => $"{n.Key}={n.Value}").ToArray());

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", oauthtoken);

                url = $"{url}{parameterUrl}";

                HttpResponseMessage response = await client.DeleteAsync(url);

                var content = await response.Content.ReadAsStringAsync();

                zohoObj.code = (int)response.StatusCode;
                zohoObj.Zoho_Object = JObject.Parse(content);
                if (zohoObj.code == 401 && (zohoObj.Zoho_Object["code"]?.ToString() == "57" || zohoObj.Zoho_Object["code"]?.ToString() == "14"))
                {
                    var tokenResult = await RefreshToken<ZohoRefreshToken>();
                    if (tokenResult.code == 200)
                    {
                        retry -= 1;
                        zohoObj = await Delete_RESTful_ZohoBaseObject(url, parameters, retry);
                    }
                }
            }

            return zohoObj;
        }

        #endregion

        #endregion

    }
}