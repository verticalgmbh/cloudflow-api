using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Vertical.Cloudflow.Api.Data;

namespace Vertical.Cloudflow.Api.Http {

    /// <summary>
    /// interface for http service used by cloudflow
    /// </summary>
    public interface IHttpService {

        /// <summary>
        /// posts data to cloudflow
        /// </summary>
        /// <param name="data"></param>
        /// <returns>response of request</returns>
        Task<JObject> Post(JObject data);

        /// <summary>
        /// posts a request to a url
        /// </summary>
        /// <param name="contents">contents for response to contain</param>
        /// <param name="parameters">query parameters</param>
        /// <returns>server response</returns>
        Task<JObject> PostFormData(FormData[] contents, params Parameter[] parameters);
    }
}