using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ContainerStar.API.ActionResults
{
    class StreamActionResult : IHttpActionResult
    {
        public StreamActionResult(Stream stream)
        {
            Stream = stream;
        }

        public Stream Stream { get; private set; }
        public string ContentType { get; set; }
        public ContentDispositionHeaderValue ContentDisposition { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            response.Content = new StreamContent(Stream);
            response.Content.Headers.ContentDisposition = ContentDisposition;

            if (string.IsNullOrWhiteSpace(ContentType))
                response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
            else
                response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentType);

            return Task.FromResult(response);
        }
    }
}
