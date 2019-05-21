using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.Cloudflow.Api.Data;
using Vertical.Cloudflow.Api.Http;

namespace Vertical.Cloudflow.Api {

    /// <summary>
    /// handles assets in cloudflow
    /// </summary>
    public class CloudflowAssetService : CloudflowService {

        /// <summary>
        /// creates a new <see cref="CloudflowAssetService"/>
        /// </summary>
        /// <param name="httpservice">http service used to access cloudflow</param>
        public CloudflowAssetService(IHttpService httpservice) 
            : base(httpservice) {
        }

        /// <summary>
        /// uploads a file to cloudflow
        /// </summary>
        /// <param name="sessionid">authenticated session to use for upload</param>
        /// <param name="folder">folder where to store file</param>
        /// <param name="filename">name of file</param>
        /// <param name="contents">file contents</param>
        /// <returns>path to uploaded file</returns>
        public async Task<string> UploadFile(string sessionid, string folder, string filename, string contents) {
            JObject response = await Http.PostFormData(
                new[] {new FormData(new StringContent(contents), "files[]", filename)},
                new Parameter("session", sessionid),
                new Parameter("asset", "upload_file"),
                new Parameter("url", folder));

            return response.Value<string>("files");
        }

        /// <summary>
        /// uploads a file to cloudflow
        /// </summary>
        /// <param name="sessionid">authenticated session to use for upload</param>
        /// <param name="folder">folder where to store file</param>
        /// <param name="filename">name of file</param>
        /// <param name="contents">file contents</param>
        /// <returns>path to uploaded file</returns>
        public async Task<string> UploadFile(string sessionid, string folder, string filename, byte[] contents)
        {
            JObject response = await Http.PostFormData(
                new[] { new FormData(new ByteArrayContent(contents), "files[]", filename) },
                new Parameter("session", sessionid),
                new Parameter("asset", "upload_file"),
                new Parameter("url", folder));

            return response.Value<string>("files");
        }

        /// <summary>
        /// uploads a file to cloudflow
        /// </summary>
        /// <param name="sessionid">authenticated session to use for upload</param>
        /// <param name="folder">folder where to store file</param>
        /// <param name="filename">name of file</param>
        /// <param name="contents">file contents</param>
        /// <returns>path to uploaded file</returns>
        public async Task<string> UploadFile(string sessionid, string folder, string filename, Stream contents)
        {
            JObject response = await Http.PostFormData(
                new[] { new FormData(new StreamContent(contents), "files[]", filename) },
                new Parameter("session", sessionid),
                new Parameter("asset", "upload_file"),
                new Parameter("url", folder));

            return response.Value<string>("files");
        }
    }
}