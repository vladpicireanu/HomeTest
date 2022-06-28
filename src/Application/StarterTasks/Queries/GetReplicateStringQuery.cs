using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StarterTasks.Queries
{
    public class GetReplicateStringQuery : IRequest<string>
    {
        public GetReplicateStringQuery(string text, int replicas)
        {
            Text = text;
            Replicas = replicas;
        }

        public string Text { get; private set; }

        public int Replicas { get; private set; }

        public class GetReplicateStringQueryHandler : IRequestHandler<GetReplicateStringQuery, string>
        {
            public Task<string> Handle(GetReplicateStringQuery request, CancellationToken cancellationToken)
            {
                var result = String.Concat(Enumerable.Repeat(request.Text, request.Replicas));

                return Task.FromResult(result);
            }
        }
    }
}
