using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// requires system.net.http nuget
namespace HttpPlay
{
    public class MyTraceHandler : DelegatingHandler
    {
        public MyTraceHandler() : this(new HttpClientHandler()) { }
        public MyTraceHandler(HttpMessageHandler inner) : base(inner) { }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("!!! MyTraceHandler >> request >> {0}", request);
            var response = await base.SendAsync(request, cancellationToken);
            Debug.WriteLine("!!! MyTraceHandler << response << {0}", response); return response;
        }
    }

}
