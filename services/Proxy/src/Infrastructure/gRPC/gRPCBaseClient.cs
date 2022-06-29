using Grpc.Core;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.gRPC
{
    public class GRPCBaseClient
    {
        private readonly IHttpContextAccessor contextAccessor;
        private const string correlationidkey = "x-correlation-id";

        public GRPCBaseClient(IHttpContextAccessor httpContextAccessor)
        {
            contextAccessor = httpContextAccessor;
        }

        private string GetCorrelationId()
        {
            var header = string.Empty;

            if (contextAccessor.HttpContext.Request.Headers.TryGetValue(correlationidkey, out var values))
            {
                header = values.FirstOrDefault();
            }

            var correlationId = string.IsNullOrEmpty(header) ? Guid.NewGuid().ToString() : header;
            return correlationId;
        }

        protected Metadata GetCorrelationMetaData()
        {
            var correlationMetaData = new Metadata() {
                new Metadata.Entry(correlationidkey,GetCorrelationId())
            };
            return correlationMetaData;
        }
    }
}
