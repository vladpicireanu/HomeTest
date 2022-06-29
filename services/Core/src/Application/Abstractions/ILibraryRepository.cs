using Domain;

namespace Application.Abstractions
{
    public interface ILibraryRepository
    {
        bool SaveChanges();

        Book GetBookById(int Id);

        void CreateBook(Book book);
    }
}
