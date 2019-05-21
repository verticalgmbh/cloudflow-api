using Newtonsoft.Json.Linq;
using Vertical.Cloudflow.Api.Data;
using Vertical.Cloudflow.Api.Http;

namespace Vertical.Cloudflow.Api {

    /// <summary>
    /// service for method calls to cloudflow
    /// </summary>
    public abstract class CloudflowService {
        readonly IHttpService httpservice;

        /// <summary>
        /// creates a new <see cref="CloudflowService"/>
        /// </summary>
        /// <param name="httpservice">service used to access cloudflow</param>
        protected CloudflowService(IHttpService httpservice) {
            this.httpservice = httpservice;
        }

        /// <summary>
        /// service used to send http request
        /// </summary>
        protected IHttpService Http => httpservice;

        /// <summary>
        /// creates a method call to be used with cloudflow
        /// </summary>
        /// <param name="method">method to call</param>
        /// <param name="parameters">method parameters</param>
        /// <returns>object containing method call to be sent to cloudflow</returns>
        protected JObject CreateMethodCall(string method, params Variable[] parameters)
        {
            JObject json = new JObject
            {
                ["method"] = method
            };

            foreach (Variable parameter in parameters)
                json[parameter.Name] = new JValue(parameter.Value);

            return json;
        }

    }
}