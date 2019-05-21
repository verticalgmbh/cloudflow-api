using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.Cloudflow.Api.Data;

namespace Vertical.Cloudflow.Api.Http {

    /// <summary>
    /// http service implementation using <see cref="HttpClient"/>
    /// </summary>
    public class HttpService : IHttpService, IDisposable {
        readonly HttpClient httpclient = new HttpClient();
        readonly Random rng = new Random();

        /// <summary>
        /// creates a new <see cref="HttpService"/>
        /// </summary>
        /// <param name="baseuri"></param>
        public HttpService(Uri baseuri) {
            httpclient.BaseAddress = baseuri;
        }

        async Task<JObject> HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) {
                JToken responsetoken = JToken.Parse(await response.Content.ReadAsStringAsync());
                if (!(responsetoken is JObject responsedata))
                    throw new InvalidOperationException($"Response not of expected type {nameof(JObject)}: " + responsetoken);

                if (responsedata.ContainsKey("error") && responsedata.ContainsKey("error_code"))
                    throw new InvalidOperationException($"{responsedata.Value<string>("error_code")}: {responsedata.Value<string>("error")}");

                return responsedata;
            }

            // TODO: throw better exception with cloudflow error code
            throw new InvalidOperationException($"Error {response.StatusCode} in post to cloudflow: {response.ReasonPhrase}");
        }

        /// <inheritdoc />
        public async Task<JObject> Post(JObject data)
        {
            HttpResponseMessage response = await httpclient.PostAsync("", new StringContent(data.ToString(), Encoding.UTF8, "application/json"));
            return await HandleResponse(response);
        }

        /// <summary>
        /// posts a request to a url
        /// </summary>
        /// <param name="contents">contents for response to contain</param>
        /// <param name="parameters">query parameters</param>
        /// <returns>server response</returns>
        public async Task<JObject> PostFormData(FormData[] contents, params Parameter[] parameters)
        {
            string targeturl;
            if (parameters.Length > 0)
                targeturl = "?" + string.Join("&", parameters.Select(p => p.Name + "=" + WebUtility.UrlEncode(p.Value)));
            else targeturl = "";

            httpclient.DefaultRequestHeaders.ExpectContinue = true;
            using (MultipartFormDataContent content = new MultipartFormDataContent("------------------------" + rng.Next().ToString("x8") + rng.Next().ToString("x8")))
            {
                // disable quotes in boundary
                NameValueHeaderValue boundary = content.Headers.ContentType.Parameters.First(p => p.Name == "boundary");
                boundary.Value = boundary.Value.Replace("\"", string.Empty);

                foreach (FormData data in contents)
                    content.Add(data.Content);

                HttpResponseMessage response = await httpclient.PostAsync(targeturl, content);
                return await HandleResponse(response);
            }
        }

        /// <inheritdoc />
        public void Dispose() {
            httpclient?.Dispose();
        }
    }
}