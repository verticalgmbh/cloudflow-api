using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.Cloudflow.Api.Data;
using Vertical.Cloudflow.Api.Http;

namespace Vertical.Cloudflow.Api {

    /// <summary>
    /// service used to access metadata in cloudflow
    /// </summary>
    public class CloudflowMetadataService : CloudflowService {

        /// <summary>
        /// creates a new <see cref="CloudflowMetadataService"/>
        /// </summary>
        /// <param name="httpservice">http access to cloudflow</param>
        public CloudflowMetadataService(IHttpService httpservice) 
            : base(httpservice) {
        }

        /// <summary>
        /// Get all metadata from the specified asset
        /// </summary>
        /// <param name="sessionid">authenticated session id</param>
        /// <param name="file">specifies the asset/file to get the meta data from</param>
        /// <param name="options">options values</param>
        /// <returns>Returns all the requested meta data</returns>
        public async Task<JObject> GetFromAsset(string sessionid, string file, params Parameter[] options) {
            if (options.Length > 0) {
                JObject optionobject=new JObject();
                foreach (Parameter option in options)
                    optionobject[option.Name] = option.Value;

                JObject call = CreateMethodCall("metadata.get_from_asset_with_options",
                    new Variable("session", sessionid),
                    new Variable("file", file));
                call["options"] = optionobject;
                return await Http.Post(call);
            }
            return await Http.Post(CreateMethodCall("metadata.get_from_asset", new Variable("session", sessionid), new Variable("file", file)));
        }

        /// <summary>
        /// Get all metadata from the specified file
        /// </summary>
        /// <param name="sessionid">authenticated session id</param>
        /// <param name="file">specifies the file to get the meta data from</param>
        /// <param name="options">options values</param>
        /// <returns>Returns all the requested meta data</returns>
        public async Task<JObject> GetFromFile(string sessionid, string file, params Parameter[] options)
        {
            if (options.Length > 0)
            {
                JObject optionobject = new JObject();
                foreach (Parameter option in options)
                    optionobject[option.Name] = option.Value;

                JObject call = CreateMethodCall("metadata.get_from_file_with_options",
                    new Variable("session", sessionid),
                    new Variable("file", file));
                call["options"] = optionobject;
                return await Http.Post(call);
            }
            return await Http.Post(CreateMethodCall("metadata.get_from_file", new Variable("session", sessionid), new Variable("file", file)));
        }

        /// <summary>
        /// Returns a preview of a file
        /// </summary>
        /// <param name="sessionid">authenticated session id</param>
        /// <param name="file">The file to get the preview data from</param>
        /// <param name="page">The page in the document</param>
        /// <param name="size">The size of the preview in pixels</param>
        /// <returns>Returns the preview as an Base64 encoded RGB jpeg image</returns>
        public async Task<byte[]> GetPreview(string sessionid, string file, int page, int size) {
            JObject response = await Http.Post(CreateMethodCall("metadata.get_preview",
                new Variable("session", sessionid),
                new Variable("file", file),
                new Variable("page", page),
                new Variable("size", size)));

            return Convert.FromBase64String(response.Value<string>("data").Split(';')[2]);
        }

        /// <summary>
        /// Returns a preview of a file
        /// </summary>
        /// <param name="sessionid">authenticated session id</param>
        /// <param name="file">The file to get the preview data from</param>
        /// <param name="options">options for preview generation</param>
        /// <returns>Returns the preview as an Base64 encoded RGB image</returns>
        public async Task<byte[]> GetPreview(string sessionid, string file, params Variable[] options) {
            JObject call = CreateMethodCall("metadata.get_preview_with_options",
                new Variable("session", sessionid),
                new Variable("file", file));
            JObject optionobject=new JObject();
            foreach (Variable option in options)
                optionobject[option.Name] = new JValue(option.Value);
            call["options"] = optionobject;
            JObject response = await Http.Post(call);
            return Convert.FromBase64String(response.Value<string>("data").Split(';')[2]);
        }
    }
}