using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.Cloudflow.Api.Data;
using Vertical.Cloudflow.Api.Http;

namespace Vertical.Cloudflow.Api {

    /// <summary>
    /// authentication methods to be used with cloudflow
    /// </summary>
    public class CloudflowAuth : CloudflowService {

        /// <summary>
        /// creates a new <see cref="CloudflowAuth"/>
        /// </summary>
        public CloudflowAuth(IHttpService httpservice)
        : base(httpservice)
        {
        }

        /// <summary>
        /// creates a session for cloudflow calls
        /// </summary>
        /// <param name="user">user to login</param>
        /// <param name="password">password of user</param>
        /// <returns>session id</returns>
        public async Task<string> CreateSession(string user, string password)
        {
            JObject response=await Http.Post(CreateMethodCall("auth.create_session", new Variable("user_name", user), new Variable("user_pass", password)));
            return response.Value<string>("session");
        }

    }
}