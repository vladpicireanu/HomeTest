using MediatR;

namespace Application.StarterTasks.Queries
{
    public class GetReverseStringQuery : IRequest<string>
    {
        public GetReverseStringQuery(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }

        public class GetReverseStringQueryHandler : IRequestHandler<GetReverseStringQuery, string>
        {
            public Task<string> Handle(GetReverseStringQuery request, CancellationToken cancellationToken)
            {
                var result = string.Create(request.Text.Length, request.Text, (chars, state) =>
                {
                    state.AsSpan().CopyTo(chars);
                    chars.Reverse();
                });

                //Another method
                //var charArray = request.Text.ToCharArray();
                //Array.Reverse(charArray);
                //var result = new string(charArray);

                return Task.FromResult(result);
            }
        }
    }
}
