
using System.Numerics;
using MediatR;

namespace Application.StarterTasks.Queries
{
    public class IsNumberTwoPowerQuery : IRequest<bool>
    {
        public IsNumberTwoPowerQuery(int number)
        {
            Number = number;
        }

        public int Number { get; private set; }

        public class IsNumberTwoPowerQueryHandler : IRequestHandler<IsNumberTwoPowerQuery, bool>
        {
            public Task<bool> Handle(IsNumberTwoPowerQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult(BitOperations.IsPow2(request.Number));
            }
        }
    }
}
