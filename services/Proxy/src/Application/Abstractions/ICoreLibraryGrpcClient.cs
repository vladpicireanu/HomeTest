namespace Application.Abstractions
{
    public  interface ICoreLibraryGrpcClient
    {
        Task<Domain.Book> GetBookById(int bookId);
    }
}
