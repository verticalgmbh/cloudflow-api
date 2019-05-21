using System;
using Vertical.Cloudflow.Api.Http;

namespace Vertical.Cloudflow.Api {

    /// <summary>
    /// api to be used to call cloudflow methods
    /// </summary>
    public class CloudflowApi {
        IHttpService httpservice;

        /// <summary>
        /// creates a new <see cref="CloudflowApi"/>
        /// </summary>
        /// <param name="server">address and port of cloudflow server</param>
        public CloudflowApi(string server)
        : this(new HttpService(new Uri($"http://{server}/portal.cgi")))
        {
        }

        /// <summary>
        /// creates a new <see cref="CloudflowApi"/>
        /// </summary>
        /// <param name="httpservice">httpservice to use to post requests</param>
        public CloudflowApi(IHttpService httpservice) {
            this.httpservice = httpservice;
            Auth = new CloudflowAuth(httpservice);
            Files = new CloudflowFileService(httpservice);
            Hub = new CloudflowHubService(httpservice);
            Assets = new CloudflowAssetService(httpservice);
            Meta = new CloudflowMetadataService(httpservice);
        }

        /// <summary>
        /// authentication methods
        /// </summary>
        public CloudflowAuth Auth { get; }

        /// <summary>
        /// file methods
        /// </summary>
        public CloudflowFileService Files { get; }

        /// <summary>
        /// hub methods
        /// </summary>
        public CloudflowHubService Hub { get; }

        /// <summary>
        /// asset management
        /// </summary>
        public CloudflowAssetService Assets { get; }

        /// <summary>
        /// access to document metadata
        /// </summary>
        public CloudflowMetadataService Meta { get; }
    }
}