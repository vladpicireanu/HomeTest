namespace Application.Models
{
    public class GenericResponse<T>
    {
        public T? Response { get; set; }

        public Error Error { get; set; } = null!;

        public GenericResponse(T response)
        {
            Response = response;
        }

        public GenericResponse(Error error)
        {
            Error = error;
        }

        public bool HasError()
        {
            return Error != null;
        }
    }
}
