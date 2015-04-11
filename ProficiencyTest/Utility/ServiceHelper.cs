using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ProficiencyTest.Utility
{
    public class ServiceHelper
    {
        public virtual async Task<TResult> GetServiceAsync<TResult>(HttpVerbs method, string relativeUrl, object input = null) where TResult : new()
        {
            TResult result = default(TResult);

            string serviceUrl = GetFullServiceUrl(relativeUrl); 

            var requestHandler = new WebRequestHandler
            {
                UseDefaultCredentials = true,
                ContinueTimeout = new TimeSpan(0, 1, 0),
                ReadWriteTimeout = 60000,
            };

            using (HttpClient http = new HttpClient(requestHandler) { Timeout = new TimeSpan(0, 1, 0) })
            {
                Task<HttpResponseMessage> httpTask = null;

                switch (method)
                {
                    case HttpVerbs.Delete:
                        httpTask = http.DeleteAsync(serviceUrl);
                        break;
                    case HttpVerbs.Get:
                        httpTask = http.GetAsync(serviceUrl);
                        break;
                    case HttpVerbs.Post:
                        httpTask = http.PostAsJsonAsync(serviceUrl, input);
                        break;
                    case HttpVerbs.Put:
                        httpTask = http.PutAsJsonAsync(serviceUrl, input);
                        break;
                }

                var response = await httpTask.ConfigureAwait(continueOnCapturedContext: false);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(content) && content != "null")
                {
                    result = JsonConvert.DeserializeObject<TResult>(content);
                }
            }

            return result;
        }

        private string GetFullServiceUrl(string relativeUrl)
        {
            string serviceUrl = WebConfigurationManager.AppSettings["PeopleServiceUrl"].Trim('/') + "/" + relativeUrl.Trim('/');
            Uri test;
            if (!Uri.TryCreate(serviceUrl, UriKind.Absolute, out test) && HttpContext.Current != null)
            {
                //Try to get base url from request
                var uri = HttpContext.Current.Request.Url;
                serviceUrl = string.Format("{0}://{1}/{2}", uri.Scheme, uri.Authority, serviceUrl);
            }
            return serviceUrl;
        }
    }
}