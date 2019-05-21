using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.Cloudflow.Api.Data;
using Vertical.Cloudflow.Api.Http;

namespace Vertical.Cloudflow.Api {

    /// <summary>
    /// service handling whitepapers in cloudflow
    /// </summary>
    public class CloudflowHubService : CloudflowService {

        /// <summary>
        /// creates a new <see cref="CloudflowHubService"/>
        /// </summary>
        /// <param name="httpservice">service used to access cloudflow</param>
        public CloudflowHubService(IHttpService httpservice) : base(httpservice) {
        }

        /// <summary>
        /// starts a workflow in cloudflow
        /// </summary>
        /// <param name="sessionid">authenticated session id</param>
        /// <param name="whitepaper">name of workflow to start</param>
        /// <param name="inputname">name of node in workflow to start</param>
        /// <param name="variables">variables to send</param>
        /// <returns></returns>
        public async Task<JObject> StartFromWhitepaper(string sessionid, string whitepaper, string inputname, JObject variables = null)
        {
            if (variables!=null)
                return await StartFromWhitepaper(sessionid, whitepaper, inputname, null, variables);

            return await Http.Post(CreateMethodCall("hub.start_from_whitepaper",
                new Variable("session", sessionid),
                new Variable("whitepaper_name", whitepaper),
                new Variable("input_name", inputname)));
        }

        /// <summary>
        /// starts a workflow in cloudflow
        /// </summary>
        /// <param name="sessionid">authenticated session id</param>
        /// <param name="workflow">name of workflow to start</param>
        /// <param name="node">name of node in workflow to start</param>
        /// <param name="files">file parameters</param>
        /// <param name="parameters">input variables</param>
        public async Task<JObject> StartFromWhitepaper(string sessionid, string workflow, string node, string[] files, JObject parameters = null) {
            JObject method = CreateMethodCall("hub.start_from_whitepaper_with_files_and_variables",
                new Variable("session", sessionid),
                new Variable("whitepaper_name", workflow),
                new Variable("input_name", node));
            if (files != null)
                method["files"] = new JArray(files.Cast<object>().ToArray());
            if (parameters != null)
                method["variables"] = parameters;

            return await Http.Post(method);
        }
    }
}