using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.Cloudflow.Api.Data;
using Vertical.Cloudflow.Api.Http;

namespace Vertical.Cloudflow.Api {

    /// <summary>
    /// service handling file operations in cloudflow
    /// </summary>
    public class CloudflowFileService : CloudflowService {

        /// <summary>
        /// creates a new <see cref="CloudflowFileService"/>
        /// </summary>
        /// <param name="httpservice">service used to access cloudflow</param>
        public CloudflowFileService(IHttpService httpservice) 
            : base(httpservice) {
        }

        /// <summary>
        /// creates a folder in cloudflow
        /// </summary>
        /// <param name="sessionid">session id for authentication</param>
        /// <param name="parent">parent folder where to create new folder</param>
        /// <param name="name">name of folder to create</param>
        /// <returns>full path to new folder</returns>
        public async Task<string> CreateFolder(string sessionid, string parent, string name) {
            JObject response = await Http.Post(CreateMethodCall("file.create_folder",
                new Variable("session", sessionid),
                new Variable("inside_folder", parent),
                new Variable("folder_to_create", name)));

            return response.Value<string>("created_folder");
        }
    }
}