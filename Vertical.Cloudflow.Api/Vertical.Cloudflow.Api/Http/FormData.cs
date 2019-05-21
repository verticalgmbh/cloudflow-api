using System.Net.Http;
using System.Net.Http.Headers;

namespace Vertical.Cloudflow.Api.Http {
    public class FormData {

        public FormData(HttpContent content, string field=null, string file=null, string contenttype=null) {
            Field = field;
            File = file;
            Content = content;
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            if(!string.IsNullOrEmpty(field))
                content.Headers.ContentDisposition.Name = field;
            if(!string.IsNullOrEmpty(file))
                content.Headers.ContentDisposition.FileName = $"\"{file}\"";
            if (!string.IsNullOrEmpty(contenttype))
                content.Headers.ContentType = new MediaTypeHeaderValue(contenttype);
        }

        /// <summary>
        /// name of field in form
        /// </summary>
        public string Field { get; }

        /// <summary>
        /// filename for file uploads
        /// </summary>
        public string File { get; }

        /// <summary>
        /// content data
        /// </summary>
        public HttpContent Content { get; }
    }
}