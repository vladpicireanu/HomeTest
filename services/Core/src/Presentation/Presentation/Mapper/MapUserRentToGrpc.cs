using Mapster;

namespace Presentation.Mapper
{
    public class MapUserRentToGrpc : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Application.Models.UserRent, UserRent>()
                .Map(dest => dest.StartDate, src => src.StartDate.ToString())
                .Map(dest => dest.ReturnDate, src => src.ReturnDate.ToString());
        }
    }
}
