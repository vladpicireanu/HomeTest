using MediatR;

namespace Application.StarterTasks.Queries
{
    public class GetOddNumbersQuery : IRequest<int[]>
    {
        public GetOddNumbersQuery(int startNumber, int stopNumber)
        {
            StartNumber = startNumber;
            StopNumber = stopNumber;
        }

        public int StartNumber { get; private set; }

        public int StopNumber { get; private set; }

        public class GetOddNumbersQueryHandler : IRequestHandler<GetOddNumbersQuery, int[]>
        {
            public Task<int[]> Handle(GetOddNumbersQuery request, CancellationToken cancellationToken)
            {
                int arrayIndex = 0;
                int[] array = new int[GetArrayLength(request)];

                for (var number = request.StartNumber; number <= request.StopNumber; number++)
                {
                    if (number % 2 != 0)
                    {
                        array[arrayIndex] = number;
                        arrayIndex++;
                    }
                }

                return Task.FromResult(array);
            }

            private int GetArrayLength(GetOddNumbersQuery request)
            {
                if (request.StopNumber % 2 == 0 && request.StartNumber % 2 == 0)
                    return ((request.StopNumber - request.StartNumber) / 2);

                return ((request.StopNumber - request.StartNumber) / 2) + 1;
            }
        }
    }
}
