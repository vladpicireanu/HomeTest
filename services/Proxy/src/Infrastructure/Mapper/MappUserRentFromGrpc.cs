using Mapster;
using Presentation;

namespace Infrastructure.Mapper
{
    internal class MappUserRentFromGrpc : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserRent, Application.Models.UserRent>()
                .Map(dest => dest.StartDate, src => DateTimeOffset.Parse(src.StartDate))
                .Map(dest => dest.ReturnDate, src => DateTimeOffset.Parse(src.ReturnDate));
        }
    }
}
